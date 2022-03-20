using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.ProgramElements;

namespace PBScript.Interpretation;

public static class PbsInterpreter
{
    private const string SingleLineComment = "//";
    
    public static bool Log = false;
    /// <summary>
    /// Interprets programText to executable form
    /// </summary>
    /// <exception cref="PbsException">Thrown for all different kinds of interpretation issues, including all relevant information to track it down</exception>
    public static PbsInterpretationResults InterpretProgram(string programText)
    {
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        Thread.CurrentThread.CurrentCulture = customCulture;
        
        var interpretationResults = new PbsInterpretationResults
        {
            TotalLines = Regex.Matches(programText, "\n").Count
        };

        var lines = new List<string>(programText.Split("\n"));

        var blockStack = new Stack<IPbsBlockStart>();
        var lineList = new List<IPbsElement>();
        
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i].Trim();
            if (line.StartsWith(SingleLineComment))
                continue;

            if (line.Contains(SingleLineComment))
            {
                line = line.Split(SingleLineComment)[0];
            }

            if (line.Length <= 0)
            {
                continue;
            }
            
            // i == sourcecode line count

            var element = LineToElement(line, lineList.Count, i);
            
            lineList.Add(element);

            if (element is IPbsBlockEnd be)
            {
                if (blockStack.Count <= 0)
                {
                    throw new UnexpectedBlockEndException(element.Token, element.SourceCodeLineNumber);
                }

                var blockStart = blockStack.Pop();
                blockStart.RegisterBlockEnd(be);
                
                if(element is IPbsBlockEnd end)
                    end.RegisterBlockStart(blockStart);
            }

            if (element is IPbsBlockStart start) 
                blockStack.Push(start);

        }

        if (blockStack.Count > 0)
        {
            var missingBlock = blockStack.Pop();
            throw new UnclosedBlockException(missingBlock.Token, missingBlock.SourceCodeLineNumber);
        }

        CheckAll(lineList);

        interpretationResults.Elements = lineList;
        interpretationResults.LinesOfCode = lineList.Count;
        
        return interpretationResults;
    }

    private static void CheckAll(List<IPbsElement> elements)
    {
        foreach (var element in elements)
        {
            if (!element.CheckValid())
            {
                throw new InvalidLineException(element.LineText, element.SourceCodeLineNumber);
            }
        }
    }

    private delegate IPbsElement CreateElementDelegate();

    private static readonly CreateElementDelegate DefaultDelegate = () => new ActionElement(); 

    private static readonly Dictionary<string, CreateElementDelegate> Tokens =
        new Dictionary<string, CreateElementDelegate>()
        {
            {"request", () => new RequestElement()},
            {"if", () => new IfElement()},
            {"else", () => new ElseElement()},
            {"elseif", () => new ElseIfElement()},
            {"while", () => new WhileElement()},
            {"end", () => new EndElement()},
            {"var", () => new VariableElement()},
        };

    public const string TokenRegex = @"[a-zA-Z\d_]+";
    private static IPbsElement LineToElement(string line, int lineIndex, int sourceCodeLineNumber)
    {
        line = line.Trim();

        if (line.StartsWith("$"))
            line = line.Split("$", 2)[1];
        
        IPbsElement? element = null;
        // Separate first token + brackets
        var found = false;
        var newLine = Regex.Replace(line, TokenRegex, match =>
        {
            if (found)
                return match.Value;
            
            // the first token HAS to start at index 0
            if (match.Index > 0)
                throw new InvalidLineException(line, sourceCodeLineNumber);
            

            found = true;

            return match.Value + " ";
        })
            // to allow for "if(x==y)" if one wants to use it.
            .Replace("(", " ( ").Replace(")", " ) ");

        if (!found)
        {
            throw new InvalidLineException(line, sourceCodeLineNumber);
        }

        var token = newLine.Split(" ")[0];

        if (Tokens.ContainsKey(token))
        {
            element = Tokens[token]();
        }
        
        element ??= DefaultDelegate();
        
        element.ParseLine(newLine, lineIndex, sourceCodeLineNumber);
        return element;
    }
}