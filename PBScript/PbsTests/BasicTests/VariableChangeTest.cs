using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.BasicTests;

public class VariableChangeTest: TestBase
{
    private const string DoubleVarName = "integerVariable";
    private const double IntVarInitValue = 5d;
    
    private const string ByZeroVarName = "byzero";
    
    private const string TruePlusTrueVarName = "tbt";

    private const string StringVarName = "string_variable";
    private const string StringVarInit = "\"This is a string!\"";
    private const string StringAppend = "\" And this is appended!\"";
    private const string StringAppendResult = @"This is a string! And this is appended!";
    
    protected override string Code => $@"var {DoubleVarName} = {IntVarInitValue}
    {DoubleVarName} += {IntVarInitValue} // => 10
{DoubleVarName} *= 2 // => 20
{DoubleVarName} /= 2 // => 10
{DoubleVarName} -= -10 // => 20

var {ByZeroVarName} = 10
{ByZeroVarName} /= 0

var {StringVarName} = {StringVarInit}
{StringVarName} += {StringAppend}
{StringVarName} += 20


var {TruePlusTrueVarName} = true
{TruePlusTrueVarName} += true


// following should not change the value of {StringVarName} at all!
{StringVarName} *= ""Hi""
{StringVarName} *= 100
{StringVarName} /= ""2""
{StringVarName} /= 20
{StringVarName} -= ""Hi""
{StringVarName} -= 250
{StringVarName} --
{StringVarName} ++
";

    [Test]
    public void Test_DoubleVariableChanges()
    {
        var doubleVar = Environment.GetObject(DoubleVarName) as VariableObject;
        Assert.NotNull(doubleVar);
        Assert.AreEqual(VariableType.Number, doubleVar.ValueType);
        Assert.NotNull(doubleVar.Value.NumberValue);
        Assert.True(Math.Abs((double)doubleVar.Value.NumberValue - 20d) < 0.01);
    }

    [Test]
    public void Test_TruePlusTrueIsUnchanged()
    {
        var doubleVar = Environment.GetObject(TruePlusTrueVarName) as VariableObject;
        Assert.NotNull(doubleVar);
        Assert.AreEqual(VariableType.Boolean, doubleVar.ValueType);
        Assert.NotNull(doubleVar.Value.BooleanValue);
        Assert.True(doubleVar.Value.BooleanValue);
    }

    [Test]
    public void Test_ByZeroIsBigAf()
    {
        var byZero = Environment.GetObject(ByZeroVarName) as VariableObject;
        Assert.NotNull(byZero);
        Assert.AreEqual(VariableType.Number, byZero.ValueType);
        Assert.NotNull(byZero.Value.NumberValue);
        Assert.True(Math.Abs((double)byZero.Value.NumberValue) > 10/0.0000001d);
    }

    [Test]
    public void Test_StringAppend()
    {
        var stringVar = Environment.GetObject(StringVarName) as VariableObject;
        Assert.NotNull(stringVar);
        Assert.AreEqual(VariableType.String, stringVar.ValueType);
        Assert.False(string.IsNullOrEmpty(stringVar.Value.StringValue));
        Assert.AreEqual(StringAppendResult, (string) stringVar.Value.StringValue);
    }
    
}