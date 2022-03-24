using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.Library.Time;

public class TimeTest : TestBase
{
    private const string KeyMinute = "minute";
    private const string KeyHour = "hour";
    private const string KeyDay = "day";
    private const string KeyWeekday = "weekday";
    private const string KeyDayOfYear = "dayOfYear";
    private const string KeyMonth = "month";
    private const string KeyYear = "year";
    private const string KeyYearIsYear = "yearisyear";
    private const string KeyRawYearIsYear = "rawyearisyear";
    
    private DateTime Now => DateTime.UtcNow;

    private int Weekday => Now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) DateTime.UtcNow.DayOfWeek;

    protected override string Code => $@"
request pbs/time

assert true minute = {Now.Minute}
assert save ""{KeyMinute}""

assert true hour = {Now.Hour}
assert save ""{KeyHour}""

assert true day = {Now.Day}
assert save ""{KeyDay}""

assert true weekday = {Weekday}
assert save ""{KeyWeekday}""

assert true dayOfYear = {Now.DayOfYear}
assert save ""{KeyDayOfYear}""

assert true month = {Now.Month}
assert save ""{KeyMonth}""

assert true year = {Now.Year}
assert save ""{KeyYear}""


// Part of the subclass only, so just tested with one.
assert true year is year and year isnot second
assert save ""{KeyYearIsYear}""

assert true $year = year
assert save ""{KeyRawYearIsYear}""
";

    [Test]
    public void Test_CorrectTimeValues()
    {

        Assert.True(Math.Abs(
            (Environment.GetObject("second")?
                .ExecuteAction("", "", Environment)
                .NumberValue ?? 1000) - DateTime.UtcNow.Second
        ) < 1);

        Assert.True(AssertObject.Results[KeyMinute]);
        Assert.True(AssertObject.Results[KeyHour]);
        Assert.True(AssertObject.Results[KeyDay]);
        Assert.True(AssertObject.Results[KeyWeekday]);
        Assert.True(AssertObject.Results[KeyDayOfYear]);
        Assert.True(AssertObject.Results[KeyMonth]);
        Assert.True(AssertObject.Results[KeyYear]);
        Assert.True(AssertObject.Results[KeyYearIsYear]);
        Assert.True(AssertObject.Results[KeyRawYearIsYear]);
    }


    [Test]
    public void Test_CorrectValueTypes()
    {
        Assert.True(Environment.GetObject("second")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("minute")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("hour")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("day")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("weekday")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("dayOfYear")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("month")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);

        Assert.True(Environment.GetObject("year")?
            .ExecuteAction("", "", Environment)
            .ReturnType == VariableType.Number);
    }

    [Test]
    public void Test_Documentation()
    {
        Assert.NotNull(Environment.GetObject("second")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("minute")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("hour")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("day")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("weekday")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("dayOfYear")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("month")?
            .GetDocumentation());

        Assert.NotNull(Environment.GetObject("year")?
            .GetDocumentation());
    }
    
}