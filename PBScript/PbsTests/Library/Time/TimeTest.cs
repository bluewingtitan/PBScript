using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.Library.Time;

public class TimeTest: TestBase
{
    private const string KeyMinute = "minute";
    private const string KeyHour = "hour";
    private const string KeyDay = "day";
    private const string KeyWeekday = "weekday";
    private const string KeyDayOfYear = "dayOfYear";
    private const string KeyMonth = "month";
    private const string KeyYear = "year";

    private int Weekday => DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) DateTime.UtcNow.DayOfWeek;
    
    protected override string Code => $@"
// will be utc.
request pbs/time/utc

assert true minute = {DateTime.UtcNow.Minute}
assert save ""{KeyMinute}""

assert true hour = {DateTime.UtcNow.Hour}
assert save ""{KeyHour}""

assert true day = {DateTime.UtcNow.Day}
assert save ""{KeyDay}""

assert true weekday = {Weekday}
assert save ""{KeyWeekday}""

assert true dayOfYear = {DateTime.UtcNow.DayOfYear}
assert save ""{KeyDayOfYear}""

assert true month = {DateTime.UtcNow.Month}
assert save ""{KeyMonth}""

assert true year = {DateTime.UtcNow.Year}
assert save ""{KeyYear}""
";

    [Test]
    public void Test_CorrectTimeValues()
    {
        
        Assert.True(Math.Abs(
            (Environment.GetObject("second")?
                .ExecuteAction("","", Environment)
                .NumberValue??1000) - DateTime.UtcNow.Second
            ) < 1);
        
        Assert.True(AssertObject.Results[KeyMinute]);
        Assert.True(AssertObject.Results[KeyHour]);
        Assert.True(AssertObject.Results[KeyDay]);
        Assert.True(AssertObject.Results[KeyWeekday]);
        Assert.True(AssertObject.Results[KeyDayOfYear]);
        Assert.True(AssertObject.Results[KeyMonth]);
        Assert.True(AssertObject.Results[KeyYear]);
    }


    [Test]
    public void Test_CorrectValueTypes()
    {
        Assert.True(Environment.GetObject("second")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("minute")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("hour")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("day")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("weekday")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("dayOfYear")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("month")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
        
        Assert.True(Environment.GetObject("year")?
            .ExecuteAction("","", Environment)
            .ReturnType == VariableType.Number);
    }
    
}