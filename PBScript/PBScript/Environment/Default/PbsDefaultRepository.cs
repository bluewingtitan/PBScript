using PBScript.Interfaces;

namespace PBScript.Environment.Default;

public class PbsDefaultRepository: IPbsRepository
{
    public Dictionary<string, IPbsRepository.ObjectCreatorDelegate> GetCreators()
    {
        return new Dictionary<string, IPbsRepository.ObjectCreatorDelegate>()
        {
            {"pbs/debug", () => new IPbsObject[] {new DebugObject()}},
            {"pbs/random", () => new IPbsObject[] {new RandomObject()}},
        };
    }
}