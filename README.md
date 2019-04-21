[![Build status](https://ci.appveyor.com/api/projects/status/7hn6lvwv1ikxsxrn/branch/master?svg=true)](https://ci.appveyor.com/project/kingboyk/serilog-nodatime/branch/master)

# Serilog.NodaTime
Adds support to Serilog for destructuring NodaTime types

# Usage
Configure Serilog to support NodaTime types by calling `.ConfigureForNodaTime` on the `LoggerConfiguration` instance:

`.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)`

E.g.:

    var logger = new LoggerConfiguration()
                    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                    .WriteTo.Console(outputTemplate: "{Instant} {Message:lj}")
                    .CreateLogger();
