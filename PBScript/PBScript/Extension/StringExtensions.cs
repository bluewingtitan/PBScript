using System.Text.RegularExpressions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Extension;

internal static class StringExtensions
{
    private const string VariableRegex = "\\$" + PbsInterpreter.TokenRegex;
    public static string EnrichString(this string s, IPbsEnvironment env)
    {
        return Regex.Replace(s, VariableRegex, match =>
        {
            var objectName = match.Value.Replace("$", "");

            var obj = env.GetObject(objectName);
            
            return obj?.GetStringValue() ?? PbsValue.Null.AsString();
        });
    }
}