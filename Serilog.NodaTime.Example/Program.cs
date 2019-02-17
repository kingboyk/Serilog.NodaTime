// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using System;
using NodaTime;
using Serilog.Core.Enrichers;

namespace Serilog.NodaTime.Example
{
    class Program
    {
        private static void Main(string[] _)
        {
            var logger = new LoggerConfiguration()
                .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                .WriteTo.Console(outputTemplate: "{Instant} {Duration} {Message:lj}")
                .CreateLogger();

                logger.ForContext(new[] {
                    new PropertyEnricher("Instant", SystemClock.Instance.GetCurrentInstant()),
                    new PropertyEnricher("Duration", Duration.FromMinutes(69))
                }).Information("Test");

            logger.Dispose();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
