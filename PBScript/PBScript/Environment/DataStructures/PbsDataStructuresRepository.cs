namespace PBScript.Environment.DataStructures;

public class PbsDataStructuresRepository: RepositoryBase
{
    public PbsDataStructuresRepository() : base("pbs")
    {
        Register("stack", () => new CreatorObject("stack", s => new StackObject(s)));
        Register("queue", () => new CreatorObject("queue", s => new QueueObject(s)));
    }
}