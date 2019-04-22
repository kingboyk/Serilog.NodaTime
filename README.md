[![Build status](https://ci.appveyor.com/api/projects/status/7hn6lvwv1ikxsxrn/branch/master?svg=true)](https://ci.appveyor.com/project/kingboyk/serilog-nodatime/branch/master) [![Nuget](https://img.shields.io/nuget/v/Serilog.NodaTime.svg)](https://www.nuget.org/packages/Serilog.NodaTime/)

# Serilog.NodaTime
Adds support for logging [NodaTime](https://github.com/nodatime/nodatime) types with [Serilog](https://github.com/serilog/serilog).

# Configuration
Configure Serilog to support NodaTime types by calling `.ConfigureForNodaTime` on the `LoggerConfiguration` instance:

`.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)`

For example:

    var logger = new LoggerConfiguration()
                    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                    .WriteTo.Console(outputTemplate: "{Instant} {Message:lj}")
                    .CreateLogger();

# Usage
Please refer to the [example program](https://github.com/kingboyk/Serilog.NodaTime/blob/master/Serilog.NodaTime.Example/Examples.cs).
