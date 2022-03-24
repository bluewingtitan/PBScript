namespace PBScript.Exception;

public class NotProperlyInitializedException: PbsException
{
    public NotProperlyInitializedException(string token) : base("Element wasn't properly initialized. Don't create PbsElements outside of the interpreter if you don't know what you are doing.", token, -1)
    {
    }
}