using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class GotoElement:ElementBase
{ 
    public string LabelName { get; private set; } = "";
    public int TargetLine { get; private set; } = -1;
    public override string Token { get; protected set; } = "goto";

    public void SetTargetLine(int line)
    {
        TargetLine = line;
    }
    
    public override int Execute(IPbsEnvironment env)
    {
        
        if (TargetLine != -1)
            return TargetLine;
        
        return LineIndex + 1;
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

    public override bool CheckValid()
    {
        return true;
    }
}
