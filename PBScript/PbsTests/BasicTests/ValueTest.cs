using NUnit.Framework;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PbsTexts.BasicTests;

public class ValueTest
{
    [Test]
    public void Test_LockedValues()
    {
        PbsInterpreter.Log = true;

        PbsValue value = new PbsValue("Value").Lock();
        Assert.AreEqual("Value", value.StringValue);
        value.SetObjectValue(true);
        Assert.AreEqual("Value", value.StringValue);
        value.SetObjectValue("true");
        Assert.AreEqual("Value", value.StringValue);
        
        
        PbsValue value2 = new PbsValue(250).Lock();
        Assert.AreEqual(250d, value2.NumberValue);
        value2.SetObjectValue(90);
        Assert.AreEqual(250d, value2.NumberValue);
        
        
        Assert.IsNull(new PbsValue().ObjectValue);
    }
    
    [Test]
    public void Test_UnlockedValues()
    {
        PbsInterpreter.Log = true;

        PbsValue value = new PbsValue("Value");
        Assert.AreEqual("Value", value.StringValue);
        value.SetObjectValue(true);
        Assert.AreEqual(null, value.StringValue);
        Assert.AreEqual(true, value.BooleanValue);
        
        
        PbsValue value2 = new PbsValue(250);
        Assert.AreEqual(250d, value2.NumberValue);
        value2.SetObjectValue(90);
        Assert.AreEqual(90d, value2.NumberValue);
        
        value2.SetObjectValue("Nice");
        Assert.AreEqual(null, value.NumberValue);
        Assert.AreEqual("Nice", value2.StringValue);
        
        
        
    }
}