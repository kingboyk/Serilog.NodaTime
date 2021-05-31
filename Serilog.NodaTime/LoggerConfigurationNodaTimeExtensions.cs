// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

// Contains code from NodaTime.Serialization.JsonNet
// Copyright 2012 The Noda Time Authors.

using System;
using NodaTime;
using Serilog.Configuration;
using Serilog.NodaTime;

namespace Serilog
{
    /// <summary>
    /// Static class containing extension methods to configure Serilog for Noda Time types.
    /// </summary>
    public static class LoggerConfigurationNodaTimeExtensions
    {
        /// <summary>
        /// Configures Serilog with everything required to properly destructure NodaTime data types.
        /// </summary>
        /// <param name="lc">The logger configuration.</param>
        /// <param name="provider">The time zone provider to use when parsing time zones and zoned date/times.</param>
        /// <remarks>Destructuring policies are only applied when an object is logged with destructuring.</remarks>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration ConfigureForNodaTime(this LoggerConfiguration lc, IDateTimeZoneProvider provider)
        {
            if (lc == null)
                throw new ArgumentNullException(nameof(lc));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            return lc
                .Destructure.WithInstant()
                .Destructure.WithOffset()
                .Destructure.WithCalendarSystem()
                .Destructure.WithLocalDateTime()
                .Destructure.WithLocalDate()
                .Destructure.WithLocalTime()
                .Destructure.WithOffsetDate()
                .Destructure.WithOffsetDateTime()
                .Destructure.WithOffsetTime()
                .Destructure.WithDateTimeZone()
                .Destructure.WithZonedDateTime(provider)
                .Destructure.WithDuration()
                .Destructure.WithPeriod()
                .Destructure.WithInterval()
                .Destructure.WithYearMonth();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.Instant
        /// </summary>
        /// <remarks>Deserialisation: InstantPattern.General.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithInstant(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<InstantDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.Offset
        /// </summary>
        /// <remarks>Deserialisation: OffsetPattern.GeneralInvariant.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithOffset(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.AsScalar<Offset>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.CalendarSystem
        /// </summary>
        /// <remarks>Deserialisation: CalendarSystem.ForId(str)</remarks>
        public static LoggerConfiguration WithCalendarSystem(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.AsScalar<CalendarSystem>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.LocalDateTime
        /// </summary>
        /// <remarks>Deserialisation: LocalDateTimePattern.ExtendedIso.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithLocalDateTime(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<LocalDateTimeDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.LocalDate
        /// </summary>
        /// <remarks>Deserialisation: LocalDatePattern.Iso.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithLocalDate(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<LocalDateDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.LocalTime
        /// </summary>
        /// <remarks>Deserialisation: LocalTimePattern.ExtendedIso.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithLocalTime(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<LocalTimeDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.OffsetDate
        /// </summary>
        /// <remarks>Deserialisation: OffsetDatePattern.GeneralIso.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithOffsetDate(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<OffsetDateDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.OffsetDateTime
        /// </summary>
        /// <remarks>Deserialisation: OffsetDateTimePattern.Rfc3339.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithOffsetDateTime(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<OffsetDateTimeDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.OffsetTime
        /// </summary>
        /// <remarks>Deserialisation: OffsetTimePattern.ExtendedIso.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithOffsetTime(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<OffsetTimeDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.DateTimeZone
        /// </summary>
        /// <remarks>Deserialisation: Use IDateTimeZoneProvider or NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithDateTimeZone(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<DateTimeZoneDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.ZonedDateTime
        /// </summary>
        /// <remarks>Deserialisation: Use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithZonedDateTime(this LoggerDestructuringConfiguration ldc, IDateTimeZoneProvider provider)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With(new ZonedDateTimeDestructuringPolicy(provider));
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.Duration
        /// </summary>
        /// <remarks>Deserialisation: Use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithDuration(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<DurationDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.Period
        /// </summary>
        /// <remarks>Deserialisation: PeriodPattern.Roundtrip.Parse(str) or use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithPeriod(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<PeriodDestructuringPolicy>();
        }

        /// <summary>
        /// Adds support for logging instances of NodaTime.Interval
        /// </summary>
        /// <remarks>Deserialisation: Use NodaTime.Serialization.JsonNet</remarks>
        public static LoggerConfiguration WithInterval(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<IntervalDestructuringPolicy>();
        }

        public static LoggerConfiguration WithYearMonth(this LoggerDestructuringConfiguration ldc)
        {
            if (ldc == null) throw new ArgumentNullException(nameof(ldc));
            return ldc.With<YearMonthDestructuringPolicy>();
        }
    }
}