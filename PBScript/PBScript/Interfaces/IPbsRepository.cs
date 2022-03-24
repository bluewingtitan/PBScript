namespace PBScript.Interfaces;

public interface IPbsRepository
{
    public delegate IPbsObject[] ObjectsCreatorDelegate();

    public Dictionary<string, ObjectsCreatorDelegate> GetCreators();
}
