namespace PBScript.Environment.Collections;

public class PbsCollectionsRepository: RepositoryBase
{
    public PbsCollectionsRepository() : base("pbs")
    {
        Register("dict", () => new CreatorObject("dict", s => new DictionaryObject(s)));
        Register("list", () => new CreatorObject("list", s => new ListObject(s)));
    }
}