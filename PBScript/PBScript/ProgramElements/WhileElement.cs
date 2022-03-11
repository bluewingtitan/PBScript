using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class WhileElement: ConditionalBlockStart
{
    public override string Token { get; protected set; } = "while";
}