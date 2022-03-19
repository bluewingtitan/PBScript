namespace PBScript;

/// <summary>
/// Contains basic information about the version of PBScript used
/// </summary>
public static class PbsInfo
{
    public static string VersionCode = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion} patch_{PatchVersion}";
    
    public const int MajorVersion = 0;
    public const int MinorVersion = 2;
    public const int SubMinorVersion = 2;
    public const int PatchVersion = 220314;
}