
using PBScript.Interfaces;

namespace PBScript.Environment;

public struct PbsEnvironmentConfig
{
    public IPbsRepository[] Repositories;
    public bool AttachDefaultRepo;
}
    
public class PbsEnvironment: IPbsEnvironment
{
    private static readonly IPbsRepository DefaultRepo = new PbsDefaultRepository();
        
    private Dictionary<string, IPbsObject> _objects = new Dictionary<string, IPbsObject>();

    private readonly Dictionary<string, IPbsRepository.ObjectCreatorDelegate> _creatorDelegates =
        new Dictionary<string, IPbsRepository.ObjectCreatorDelegate>();

    public PbsEnvironment(IPbsRepository[] repositories = null, bool useDefaultRepo = true)
    {
        if (repositories != null)
        {
            foreach (var repository in repositories)
            {
                foreach (var (key, creator) in repository.GetCreators())
                {
                    _creatorDelegates[key] = creator;
                }
            }
        }

        if (useDefaultRepo)
        {
            foreach (var (key, creator) in DefaultRepo.GetCreators())
            {
                _creatorDelegates[key] = creator;
            }
        }
    }

    public void Log(string elementName, string message)
    {
        Console.WriteLine($"[{elementName}] {message}");
    }

    public IPbsObject? GetObject(string key)
    {
        key = key.Trim();

        if (!_objects.ContainsKey(key))
            return null;
            
        return _objects[key];
    }

    public void RegisterObject(string key, IPbsObject pbsObject, bool @override = false)
    {
        if (_objects.ContainsKey(key) && !@override)
        {
            return;
        }
            
        _objects[key] = pbsObject;
    }

    public void DeregisterObject(IPbsObject pbsObject)
    {
        if (_objects.ContainsValue(pbsObject))
        {
            var k = "";
            foreach (var (key, obj) in _objects)
            {
                if (obj == pbsObject)
                {
                    k = key;
                    break;
                }
            }
                
            if(string.IsNullOrEmpty(k))
                return;

            _objects.Remove(k);
        }
    }

    public void Request(string requestedObject)
    {
        // only set once!
        if(_objects.ContainsKey(requestedObject))
            return;
            
        if (_creatorDelegates.ContainsKey(requestedObject))
        {
            _objects[requestedObject] = _creatorDelegates[requestedObject]();
        }
    }
}