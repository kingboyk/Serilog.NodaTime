// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using System.IO;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using NUnit.Framework;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Compact.Reader;
using Serilog.Sinks.TestCorrelator;

namespace Serilog.NodaTime.Tests
{
    [TestFixture]
    public class DestructuringTests
    {
        private JsonSerializerSettings _jsonSerializerSettings;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb).WriteTo.TestCorrelator().CreateLogger();
            _jsonSerializerSettings = new JsonSerializerSettings().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            _jsonSerializerSettings.Converters.Add(new NodaCalendarSystemConverter());
        }

        [Test]
        public void InstantRoundTripTest()
        {
            RoundTripTest(Instant.FromUtc(2012, 1, 2, 3, 4, 5));
        }

        [Test]
        public void NullableInstantRoundTripTest()
        {
            RoundTripTest((Instant?)Instant.FromUtc(2012, 1, 2, 3, 4, 5));
        }

        [Test]
        public void OffsetRoundTripTest()
        {
            RoundTripTest(Offset.FromHoursAndMinutes(1, 2));
        }

        [Test]
        public void NullableOffsetRoundTripTest()
        {
            RoundTripTest((Offset?)Offset.FromHoursAndMinutes(1, 2));
        }

        [Test]
        public void CalendarSystemRoundTripTest()
        {
            RoundTripTest(CalendarSystem.Julian);
        }

        [Test]
        public void LocalDateTimeRoundTripTest()
        {
            RoundTripTest(new LocalDateTime(2018, 12, 11, 10, 9, 8));
        }

        [Test]
        public void NullableLocalDateTimeRoundTripTest()
        {
            RoundTripTest((LocalDateTime?)new LocalDateTime(2018, 12, 11, 10, 9, 8));
        }

        [Test]
        public void LocalDateRoundTripTest()
        {
            RoundTripTest(new LocalDate(2018, 12, 11));
        }

        [Test]
        public void NullableLocalDateRoundTripTest()
        {
            RoundTripTest((LocalDate?)new LocalDate(2018, 12, 11));
        }

        [Test]
        public void LocalTimeRoundTripTest()
        {
            RoundTripTest(new LocalTime(14, 15, 16,17));
        }

        [Test]
        public void NullableLocalTimeRoundTripTest()
        {
            RoundTripTest((LocalTime?)new LocalTime(14, 15, 16, 17));
        }

        [Test]
        public void OffsetDateRoundTripTest()
        {
            RoundTripTest(new OffsetDate(new LocalDate(2018, 12, 11), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void NullableOffsetDateRoundTripTest()
        {
            RoundTripTest((OffsetDate?)new OffsetDate(new LocalDate(2018, 12, 11), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void OffsetDateTimeRoundTripTest()
        {
            RoundTripTest(new OffsetDateTime(new LocalDateTime(2018, 12, 11, 10, 9, 8), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void NullableOffsetDateTimeRoundTripTest()
        {
            RoundTripTest((OffsetDateTime?)new OffsetDateTime(new LocalDateTime(2018, 12, 11, 10, 9, 8), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void OffsetTimeRoundTripTest()
        {
            RoundTripTest(new OffsetTime(new LocalTime(10, 9, 8), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void NullableOffsetTimeRoundTripTest()
        {
            RoundTripTest((OffsetTime?)new OffsetTime(new LocalTime(10, 9, 8), Offset.FromHoursAndMinutes(1, 2)));
        }

        [Test]
        public void DateTimeZoneRoundTripTest()
        {
            RoundTripTest(DateTimeZoneProviders.Tzdb["Europe/London"]);
        }

        [Test]
        public void ZonedDateTimeRoundTripTest()
        {
            RoundTripTest(new LocalDateTime(2018, 12, 11, 10, 9, 8).InZoneLeniently(DateTimeZoneProviders.Tzdb["Australia/Canberra"]));
        }

        [Test]
        public void NullableZonedDateTimeRoundTripTest()
        {
            RoundTripTest((ZonedDateTime?)new LocalDateTime(2018, 12, 11, 10, 9, 8).InZoneLeniently(DateTimeZoneProviders.Tzdb["Australia/Canberra"]));
        }

        [Test]
        public void DurationRoundTripTest()
        {
            RoundTripTest(Duration.FromMinutes(77));
        }

        [Test]
        public void NullableDurationRoundTripTest()
        {
            RoundTripTest((Duration?)Duration.FromMinutes(77));
        }

        [Test]
        public void PeriodRoundTripTest()
        {
            RoundTripTest(Period.FromNanoseconds(1234567890));
        }

        [Test]
        public void IntervalWithStartAndEndRoundTripTest()
        {
            var interval = new Interval(start: Instant.FromJulianDate(0), end: Instant.FromUnixTimeMilliseconds(0));
            Assert.That(interval.HasStart);
            Assert.That(interval.HasEnd);
            RoundTripTest(interval);
        }

        [Test]
        public void NullableIntervalRoundTripTest()
        {
            var interval = new Interval(start: Instant.FromJulianDate(0), end: Instant.FromUnixTimeMilliseconds(0));
            Assert.That(interval.HasStart);
            Assert.That(interval.HasEnd);
            RoundTripTest((Interval?)interval);
        }

        [Test]
        public void IntervalWithStartRoundTripTest()
        {
            var interval = new Interval(start: Instant.FromUnixTimeMilliseconds(0), end: null);
            Assert.That(interval.HasStart);
            Assert.That(!interval.HasEnd);
            RoundTripTest(interval);
        }

        [Test]
        public void IntervalWithEndRoundTripTest()
        {
            var interval = new Interval(end: Instant.FromUnixTimeMilliseconds(0), start: null);
            Assert.That(!interval.HasStart);
            Assert.That(interval.HasEnd);
            RoundTripTest(interval);
        }

        [Test]
        public void IntervalWithNeitherStartNorEndRoundTripTest()
        {
            var interval = new Interval(null, null);
            Assert.That(!interval.HasStart);
            Assert.That(!interval.HasEnd);
            RoundTripTest(interval);
        }

        private void RoundTripTest<T>(T obj)
        {
            using (TestCorrelator.CreateContext())
            {
                Log.Information("This is a test message {@Obj}", obj);

                var loggedEvents = TestCorrelator.GetLogEventsFromCurrentContext().ToList();

                loggedEvents.Should().ContainSingle();

                var writer = new StringWriter();

                new CompactJsonFormatter().Format(loggedEvents.Single(), writer);

                var reader = new StringReader(writer.ToString());

                var jsonReader = new LogEventReader(reader);
                jsonReader.TryRead(out var deserialisedEvent);
                string objJson = deserialisedEvent.Properties["Obj"].ToString();

                var deserialisedObj = JsonConvert.DeserializeObject<T>(objJson, _jsonSerializerSettings);
                deserialisedObj.Should().BeEquivalentTo(obj, "of round trip");
            }
        }
    }
}