
using System.Collections.Immutable;
using PBScript.Environment.DataStructures;
using PBScript.Environment.Debug;
using PBScript.Environment.Default;
using PBScript.Environment.Random;
using PBScript.Environment.Time;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Environment;

public enum DefaultRepository
{
    /// <summary>
    /// Allows usage of a debug object, used to print into the execution-console.
    /// </summary>
    Debug,
    
    /// <summary>
    /// Allows usage of time objects (second, minute, ...)
    /// pbs/time/utc and pbs/time/local are available. pbs/time will use utc time.
    /// </summary>
    Time_UtcDefault,
    
    /// <summary>
    /// Allows usage of time objects (second, minute, ...) Only either use Time_UtcDefault or Time_LocalDefault, the last one in the list will override the previous.
    /// pbs/time/utc and pbs/time/local are available. pbs/time will use local time.
    /// </summary>
    Time_LocalDefault,
    
    /// <summary>
    /// Allows usage of a random number generator (can generate random bool and random numbers)
    /// </summary>
    Random,
    
    /// <summary>
    /// Allows usage of a basic implementation of a stack and a queue, available under pbs/queue and pbs/stack (use as 'stack create $name' to create a new stack)
    /// </summary>
    DataStructures,
}

public class PbsEnvironment: IPbsEnvironment
{
    private static readonly ImmutableDictionary<DefaultRepository, IPbsRepository> DefaultRepos = new Dictionary<DefaultRepository, IPbsRepository>()
    {
        {DefaultRepository.Debug, new PbsDebugRepository()},
        {DefaultRepository.Time_UtcDefault, new PbsTimeRepository(true)},
        {DefaultRepository.Time_LocalDefault, new PbsTimeRepository(false)},
        {DefaultRepository.Random, new PbsRandomRepository()},
        {DefaultRepository.DataStructures, new PbsDataStructuresRepository()},
    }.ToImmutableDictionary();
        
    private Dictionary<string, IPbsObject> _objects = new Dictionary<string, IPbsObject>();

    private readonly Dictionary<string, IPbsRepository.ObjectsCreatorDelegate> _creatorDelegates =
        new Dictionary<string, IPbsRepository.ObjectsCreatorDelegate>();

    public PbsEnvironment(IPbsRepository[]? repositories = null, DefaultRepository[]? defaultRepositories = null)
    {
        if (repositories != null)
        {
            foreach (var repository in repositories)
            {
                foreach (var (key, creator) in repository.GetCreators())
                {
                    if (PbsInterpreter.Log)
                        Log("env", "add custom requestable " + key);

                    _creatorDelegates[key] = creator;
                }
            }
        }

        if (defaultRepositories != null)
        {
            foreach (var key in defaultRepositories)
            {
                foreach (var (creatorKey, creator) in DefaultRepos[key].GetCreators())
                {
                    if (PbsInterpreter.Log)
                        Log("env", "add built-in requestable " + creatorKey);
                    
                    _creatorDelegates[creatorKey] = creator;
                }
            }
        }
        
        RegisterObject(new StaticValueObject(PbsValue.True, "true"), true);
        RegisterObject(new StaticValueObject(PbsValue.False, "false"), true);
        RegisterObject(new StaticValueObject(PbsValue.Null, "null"), true);
    }

    public void Log(string elementName, string message)
    {
        Console.WriteLine($"[{elementName}] {message}");
    }

    public IPbsObject? GetObject(string key)
    {
        if (PbsInterpreter.Log)
            Log("Env", $"Get '{key}'");
        key = key.Trim();

        if (!_objects.ContainsKey(key))
            return null;
            
        return _objects[key];
    }

    public void RegisterObject(IPbsObject pbsObject, bool @override = false)
    {
        
        var key = pbsObject.ObjectName;
        if (_objects.ContainsKey(key) && !@override)
        {
            if (PbsInterpreter.Log)
                Log("Env", $"Didn't register '{pbsObject.ObjectName}': already registered");
            return;
        }
        if (PbsInterpreter.Log)
            Log("Env", $"Registered '{pbsObject.ObjectName}'");

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
        if (PbsInterpreter.Log)
            Log("Env", $"Requested '{requestedObject}'");
            
        if (_creatorDelegates.ContainsKey(requestedObject))
        {
            var obj = _creatorDelegates[requestedObject]();

            foreach (var pbsObject in obj)
            {
                if(_objects.ContainsKey(pbsObject.ObjectName))
                    continue;
                
                _objects[pbsObject.ObjectName] = pbsObject;
            }
            
        }
    }


    #region Static Creators

    
    public static PbsEnvironment WithAllDefaultRepositories(IPbsRepository[]? repositories = null, bool utcTime = true)
    {
        return new PbsEnvironment(repositories, new []
        {
            DefaultRepository.Debug,
            utcTime?DefaultRepository.Time_UtcDefault:DefaultRepository.Time_LocalDefault,
            DefaultRepository.Random,
            DefaultRepository.DataStructures,
        });
    }
    
    /// <summary>
    /// Same as WithAllDefaultRepositories, but excluding the debug-repository.
    /// </summary>
    public static PbsEnvironment ProductionReady(IPbsRepository[]? repositories = null, bool utcTime = true)
    {
        return new PbsEnvironment(repositories, new []
        {
            utcTime?DefaultRepository.Time_UtcDefault:DefaultRepository.Time_LocalDefault,
            DefaultRepository.Random,
            DefaultRepository.DataStructures,
        });
    }
    

    #endregion
    
    
    
}