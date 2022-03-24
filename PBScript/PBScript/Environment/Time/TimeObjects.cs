namespace PBScript.Environment.Time;

public class SecondObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Second;
    public SecondObject(bool useUtc) : base(useUtc, "second") {}
}

public class MinuteObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Minute;
    public MinuteObject(bool useUtc) : base(useUtc, "minute") {}
}

public class HourObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Hour;
    public HourObject(bool useUtc) : base(useUtc, "hour") {}
}

public class DayObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Day;
    public DayObject(bool useUtc) : base(useUtc, "day") {}
}

public class WeekdayObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.DayOfWeek==DayOfWeek.Sunday? 7: (int) dt.DayOfWeek;
    public WeekdayObject(bool useUtc) : base(useUtc, "weekday") {}
}

public class DayOfYearObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.DayOfYear;
    public DayOfYearObject(bool useUtc) : base(useUtc, "dayOfYear") {}
}

public class MonthObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Month;
    public MonthObject(bool useUtc) : base(useUtc, "month") {}
}

public class YearObject: TimeObjectBase
{
    protected override int GetValueFromDateTime(DateTime dt) => dt.Year;
    public YearObject(bool useUtc) : base(useUtc, "year") {}
}