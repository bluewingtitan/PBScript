# Time Repository

> Creation: v0.6
> 
> Last Update: v0.6

Objects can be requested unter pbs/time/utc/... for utc time, pbs/time/local/... for local time or pbs/time/... to use the default. 

If `pbs/time`, `pbs/time/utc` or `pbs/time/local` are requested, all following objects will get imported (all in the set time-zone).

## Objects `... = pbs/time(/utc;/local)`

### second `request .../second`

Represents the second part of the current time.

### minute `request .../minute`

Represents the minute part of the current time.

### hour `request .../hour`

Represents the hour part of the current time.

### day `request .../day`

Represents the day part of the current date (between 1 and 31).

### weekday `request .../weekday`

Represents the weekday part of the current date (1 for monday, 7 for sunday).

### dayOfYear `request .../dayOfYear`

Represents the dayOfYear part of the current date (between 1 and 366).

### month `request .../month`

Represents the month part of the current date (between 1 and 12).

### year `request .../second`

Represents the year part of the current date.
