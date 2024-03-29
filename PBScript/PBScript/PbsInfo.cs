namespace PBScript;

/// <summary>
/// Contains basic information about the version of PBScript used
/// </summary>
public static class PbsInfo
{
    public static string VersionCode = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion}";
    public static string VersionCodeDetailed = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion} patch_{PatchVersion}";
    
    public const int MajorVersion = 0;
    public const int MinorVersion = 8;
    public const int SubMinorVersion = 0;
    public const string PatchVersion = "230714a";

    /// <summary>
    /// Feel free to print this somewhere in the project you are using PBScript in to give credit.
    /// </summary>
    public static string Credits = 
@$"Powered by Plant-Based-Script (PBScript) v{VersionCode} by Dominik Mezler/bluewingtitan
Crafted with love, care and plants.

ko-fi.com/bluewingtitan";
}