namespace PBScript.Interfaces;

public interface IPbsRepository
{
    public delegate IPbsObject ObjectCreatorDelegate();

    public Dictionary<string, ObjectCreatorDelegate> GetCreators();
}
