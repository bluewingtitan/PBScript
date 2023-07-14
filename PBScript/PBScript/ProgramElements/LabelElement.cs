using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class LabelElement: ElementBase
{
    public string LabelName { get; private set; } = "";
    public override string Token { get; } = "label";
    public override int Execute(IPbsEnvironment env)
    {
        return LineIndex + 1;
    }

    public override void ThrowIfNotValid()
    {
        
    }

    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);

        var labelDefinition = code.Replace(Token, "").Trim().Split(" ",2)[0].Trim();
        
        // get label name
        if (!Regex.IsMatch(labelDefinition,"^" + PbsInterpreter.TokenRegex))
        {
            throw new InvalidLabelDefinition(LineText, SourceCodeLineNumber);
        }

        LabelName = labelDefinition;
    }
}