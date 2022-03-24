using System.Text.RegularExpressions;
using NUnit.Framework;
using PBScript;
using PBScript.Interpretation;

namespace PbsTexts;

public class PbsInfoTest
{
    [Test]
    public void Test_Format()
    {
        Assert.True(Regex.IsMatch(PbsInfo.VersionCode, "[\\d]+\\.[\\d]+\\.[\\d]+"));
        Assert.True(Regex.IsMatch(PbsInfo.VersionCodeDetailed, "[\\d]+\\.[\\d]+\\.[\\d]+ patch_[\\da-z]+"));
        Assert.True(PbsInfo.Credits.Contains(PbsInfo.VersionCode));
    }
}