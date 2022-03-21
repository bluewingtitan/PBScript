using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.LanguageFeatures;

public class VariableChangeTest: TestBase
{
    private const string DoubleVarName = "integerVariable";
    private const double IntVarInitValue = 5d;

    private const string StringVarName = "string_variable";
    private const string StringVarInit = "\"This is a string!\"";
    private const string StringAppend = "\" And this is appended!\"";
    private const string StringAppendResult = @"This is a string! And this is appended!";
    
    protected override string Code => $@"var {DoubleVarName} = {IntVarInitValue}
    {DoubleVarName}+={IntVarInitValue} // => 10
{DoubleVarName}*=2 // => 20
var {StringVarName} = {StringVarInit}
{StringVarName} += {StringAppend}
";

    [Test]
    public void Test_DoubleVariableChanges()
    {
        var doubleVar = _environment.GetObject(DoubleVarName) as VariableObject;
        Assert.NotNull(doubleVar);
        Assert.AreEqual(VariableType.Number, doubleVar.ValueType);
        Assert.NotNull(doubleVar.Value.NumberValue);
        Assert.True(Math.Abs((double)doubleVar.Value.NumberValue - 20d) < 0.01);
    }

    [Test]
    public void Test_StringAppend()
    {
        var stringVar = _environment.GetObject(StringVarName) as VariableObject;
        Assert.NotNull(stringVar);
        Assert.AreEqual(VariableType.String, stringVar.ValueType);
        Assert.False(string.IsNullOrEmpty(stringVar.Value.StringValue));
        Assert.AreEqual(StringAppendResult, (string) stringVar.Value.StringValue);
    }
    
}