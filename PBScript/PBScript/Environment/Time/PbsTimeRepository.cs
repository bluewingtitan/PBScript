using PBScript.Interfaces;

namespace PBScript.Environment.Time;

public class PbsTimeRepository: RepositoryBase
{
    public PbsTimeRepository(bool defaultToUtcTime = true) : base("pbs")
    {
        Register("time/utc", () => CreateAll(true));
        Register("time/local", () => CreateAll(false));
        Register("time", () => CreateAll(defaultToUtcTime));

        // example: local second will be pbs/time/second or pbs/time/local/second if defaultToUtcTime = false
        RegisterAsSingles(false, false);
        RegisterAsSingles(true, false);
        
        RegisterAsSingles(defaultToUtcTime, true);
    }

    private void RegisterAsSingles(bool utc, bool noPre)
    {
        var name = "time/" + ( noPre?"":utc ? "utc/" :"local/");
        
        Register(name + "second", () => new SecondObject(utc));
        Register(name + "minute", () => new MinuteObject(utc));
        Register(name + "hour", () => new HourObject(utc));
        Register(name + "day", () => new DayObject(utc));
        Register(name + "weekday", () => new WeekdayObject(utc));
        Register(name + "dayOfYear", () => new DayOfYearObject(utc));
        Register(name + "month", () => new MonthObject(utc));
        Register(name + "year", () => new YearObject(utc));
    }
    
    private IPbsObject[] CreateAll(bool utc)
    {
        return new IPbsObject[]
        {
            new SecondObject(utc),
            new MinuteObject(utc),
            new HourObject(utc),
            new DayObject(utc),
            new WeekdayObject(utc),
            new DayOfYearObject(utc),
            new MonthObject(utc),
            new YearObject(utc)
        };
    }
    
}