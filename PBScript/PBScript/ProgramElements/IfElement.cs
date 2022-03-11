using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class IfElement: ConditionalBlockStart
{
    public override string Token { get; protected set; } = "if";
}