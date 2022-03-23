using PBScript.Environment.Default;
using PBScript.Interfaces;

namespace PBScript.Environment.Debug;

public class PbsDebugRepository: RepositoryBase
{
    public PbsDebugRepository() : base("pbs")
    {
        Register("debug", () => new DebugObject());
        Register("assert", () => new AssertObject());
    }
}