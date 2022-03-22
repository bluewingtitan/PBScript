namespace PBScript;

/// <summary>
/// Contains basic information about the version of PBScript used
/// </summary>
public static class PbsInfo
{
    public static string VersionCode = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion}";
    public static string VersionCodeDetailed = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion} patch_{PatchVersion}";
    
    public const int MajorVersion = 0;
    public const int MinorVersion = 5;
    public const int SubMinorVersion = 1;
    public const string PatchVersion = "220322a";
}