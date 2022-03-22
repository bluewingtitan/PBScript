using PBScript.Environment.Default;
using PBScript.Interfaces;

namespace PBScript.Environment.Random;

public class PbsRandomRepository: RepositoryBase
{
    public PbsRandomRepository() : base("pbs")
    {
        Register("random", () => new RandomObject());
    }
}