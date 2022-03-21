using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.LanguageFeatures;

public class VariableAssignmentTest: TestBase
{
    private const string StringWithNumbers = "\"This is a string with 123 numbers.\"";
    private const string StringWithNumbersName = "stringWith_numbers";
    private const string String1 = "\"Hi! This one is a string!\"";
    private const string String1Name = "string1";
    private const double X1 = 27;
    private const string X1Name = "x1";
    
    protected override string Code => $@"var {X1Name} = {X1}
var {String1Name} = {String1}
var {StringWithNumbersName} = {StringWithNumbers}";

    [Test]
    public void Test_VariablesAreCorrectlyAssigned()
    {
        var x1 = _environment.GetObject(X1Name) as VariableObject;
        Assert.NotNull(x1);
        Assert.AreEqual(VariableType.Number, x1.ValueType);
        Assert.NotNull(x1.Value.NumberValue);
        Assert.True(Math.Abs((double)x1.Value.NumberValue - X1) < 0.01);

        var string1 = _environment.GetObject(String1Name) as VariableObject;
        Assert.NotNull(string1);
        Assert.AreEqual(VariableType.String, string1.ValueType);
        Assert.False(string.IsNullOrEmpty(string1.Value.StringValue));
        Assert.AreEqual(String1.Replace("\"",""), (string)string1.Value.StringValue);

        var stringWithNumbers = _environment.GetObject(StringWithNumbersName) as VariableObject;
        Assert.NotNull(stringWithNumbers);
        Assert.AreEqual(VariableType.String, stringWithNumbers.ValueType);
        Assert.False(string.IsNullOrEmpty(stringWithNumbers.Value.StringValue));
        Assert.AreEqual(StringWithNumbers.Replace("\"",""), (string)stringWithNumbers.Value.StringValue);
    }
}