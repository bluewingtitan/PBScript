using PBScript.Interfaces;

namespace PBScript.Environment;

public class PbsDefaultRepository: IPbsRepository
{
    public Dictionary<string, IPbsRepository.ObjectCreatorDelegate> GetCreators()
    {
        return new Dictionary<string, IPbsRepository.ObjectCreatorDelegate>()
        {
            {"debug", () => new DebugObject()},
            {"random", () => new RandomObject()},
        };
    }
}