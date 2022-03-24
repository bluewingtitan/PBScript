using PBScript.Interfaces;

namespace PBScript.Environment;

public abstract class RepositoryBase: IPbsRepository
{
    protected readonly string Space = "";
    
    private readonly Dictionary<string, IPbsRepository.ObjectsCreatorDelegate> _creatorDelegates = new();
    public delegate IPbsObject SingleObjectCreatorDelegate();

    protected RepositoryBase(string space)
    {
        if (!string.IsNullOrEmpty(space) && !string.IsNullOrWhiteSpace(space))
        {
            Space = space + "/";
        }
    }

    protected void Register(string name, IPbsRepository.ObjectsCreatorDelegate creator, bool addSpace = true)
    {
        var creatorName = (addSpace ? Space : "") + name;
        _creatorDelegates[creatorName] = creator;
    }
    
    protected void Register(string name, SingleObjectCreatorDelegate creator, bool addSpace = true)
    {
        var creatorName = (addSpace ? Space : "") + name;
        _creatorDelegates[creatorName] = () => new [] {creator()};
    }

    public Dictionary<string, IPbsRepository.ObjectsCreatorDelegate> GetCreators() => _creatorDelegates;
}