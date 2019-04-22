// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using System;
using NodaTime;
using Serilog.Core.Enrichers;

namespace Serilog.NodaTime.Example
{
    public static class Examples
    {
        public static void LoggingExamples(bool withNodaTimeDeconstruction)
        {
            var lc = new LoggerConfiguration();

            if (withNodaTimeDeconstruction)
                lc = lc.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            var logger = lc
                .WriteTo.Console(outputTemplate: "{Instant} {Message:lj}{NewLine}")
                .CreateLogger();

            var localDateTime = LocalDateTime.FromDateTime(DateTime.Now);
            
            logger.WithCurrentInstant().Information("DateTimeZone {@DateTimeZone}", DateTimeZoneProviders.Tzdb["Europe/London"]);
            logger.WithCurrentInstant().Information("Duration {@Duration}", Duration.FromMinutes(69));
            logger.WithCurrentInstant().Information("Interval {@Interval}", new Interval(start: Instant.FromJulianDate(0), end: Instant.FromUnixTimeMilliseconds(0)));
            logger.WithCurrentInstant().Information("LocalDate {@LocalDate}", localDateTime.Date);
            logger.WithCurrentInstant().Information("LocalDateTime {@LocalDateTime}", localDateTime);
            logger.WithCurrentInstant().Information("LocalTime {@LocalTime}", localDateTime.TimeOfDay);
            logger.WithCurrentInstant().Information("OffsetDateTime {@OffsetDateTime}", new OffsetDateTime(localDateTime, Offset.FromHours(10)));
            logger.WithCurrentInstant().Information("Period {@Period}", Period.FromNanoseconds(1234567890));
            logger.WithCurrentInstant().Information("ZonedDateTime {@ZonedDateTime}", localDateTime.InZoneLeniently(DateTimeZoneProviders.Tzdb["Australia/Canberra"]));

            if (withNodaTimeDeconstruction)
                Console.WriteLine();

            // Note that for the following types Serilog.NodaTime overrides the deconstruction setting by declaring that the type should always be considered a Scalar, as the extra information within these structures is not useful in a log nor necessary to round-trip back to a NodaTime type:
            logger.WithCurrentInstant().Information("CalendarSystem {@CalendarSystem}", CalendarSystem.Julian);
            logger.WithCurrentInstant().Information("Offset {@Offset}", Offset.FromHoursAndMinutes(1, 2));

            logger.Dispose();
        }

        public static ILogger WithCurrentInstant(this ILogger logger) => logger.ForContext(new PropertyEnricher("Instant", SystemClock.Instance.GetCurrentInstant()));
    }
}
