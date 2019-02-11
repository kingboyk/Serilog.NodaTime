// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

// Contains code from NodaTime.Serialization.JsonNet
// Copyright 2012 The Noda Time Authors.
using System;
using NodaTime;

namespace Serilog.NodaTime
{
    /// <summary>
    /// Static class containing extension methods to configure Serilog for Noda Time types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Configures Serilog with everything required to properly destructure NodaTime data types.
        /// </summary>
        /// <param name="lc">The logger configuration.</param>
        /// <param name="provider">The time zone provider to use when parsing time zones and zoned date/times.</param>
        /// <remarks>Destructuring policies are only applied when an object is logged with destructuring. Serialisation of NodaTime types which are not destructured can be controlled by logging their string output or by other means.</remarks>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration ConfigureForNodaTime(this LoggerConfiguration lc, IDateTimeZoneProvider provider)
        {
            if (lc == null)
            {
                throw new ArgumentNullException(nameof(lc));
            }
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return lc
                .Destructure.With<InstantDestructuringPolicy>() // Deserialisation: InstantPattern.General.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.AsScalar<Offset>() // Deserialisation: OffsetPattern.GeneralInvariant.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.AsScalar<CalendarSystem>() // Deserialisation: CalendarSystem.ForId(str)
                .Destructure.With<LocalDateTimeDestructuringPolicy>() // Deserialisation: LocalDateTimePattern.ExtendedIso.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.With<LocalDateDestructuringPolicy>() // Deserialisation: LocalDatePattern.Iso.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.With<LocalTimeDestructuringPolicy>() // Deserialisation: LocalTimePattern.ExtendedIso.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.With<OffsetDateTimeDestructuringPolicy>() // Deserialisation: OffsetDateTimePattern.Rfc3339.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.With<DateTimeZoneDestructuringPolicy>() // Deserialisation: Use IDateTimeZoneProvider or NodaTime.Serialization.JsonNet
                .Destructure.With(new ZonedDateTimeDestructuringPolicy(provider)) // Deserialisation: Use NodaTime.Serialization.JsonNet
                .Destructure.With<DurationDestructuringPolicy>() // Deserialisation: Use NodaTime.Serialization.JsonNet
                .Destructure.With<PeriodDestructuringPolicy>() // Deserialisation: PeriodPattern.Roundtrip.Parse(str) or use NodaTime.Serialization.JsonNet
                .Destructure.With<IntervalDestructuringPolicy>(); // Deserialisation: Use NodaTime.Serialization.JsonNet
        }
    }
}