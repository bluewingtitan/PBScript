namespace PBScript;

/// <summary>
/// Contains basic information about the version of PBScript used
/// </summary>
public static class PbsInfo
{
    public static string VersionCode = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion}";
    public static string VersionCodeDetailed = $"{MajorVersion}.{MinorVersion}.{SubMinorVersion} patch_{PatchVersion}";
    
    public const int MajorVersion = 0;
    public const int MinorVersion = 7;
    public const int SubMinorVersion = 0;
    public const string PatchVersion = "220330";

    /// <summary>
    /// Feel free to print this somewhere in the project you are using PBScript in to give credit.
    /// </summary>
    public static string Credits = 
@$"Powered by Plant-Based-Script (PBScript) v{VersionCode} by Dominik Mezler/bluewingtitan
Crafted with love, care and plants.

Usage of ko-fi.com/bluewingtitan is definitely appreciated if you want to support!";
}