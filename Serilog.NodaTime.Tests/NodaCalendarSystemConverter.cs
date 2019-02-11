// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

// Contains code from NodaTime.Serialization.JsonNet
// Copyright 2012 The Noda Time Authors.

using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace Serilog.NodaTime.Tests
{
    /// <summary>
    /// Json.NET converter for <see cref="CalendarSystem"/>.
    /// </summary>
    public sealed class NodaCalendarSystemConverter : NodaConverterBase<CalendarSystem>
    {
        /// <summary>
        /// Reads the CalendarSystem ID (which must be a string) from the reader, and converts it to a calendar system.
        /// </summary>
        /// <param name="reader">The JSON reader to fetch data from.</param>
        /// <param name="serializer">The serializer for embedded serialization.</param>
        /// <returns>The <see cref="CalendarSystem"/> identified in the JSON, or null.</returns>
        protected override CalendarSystem ReadJsonImpl(JsonReader reader, JsonSerializer serializer)
        {
            Preconditions.CheckData(reader.TokenType == JsonToken.String,
                "Unexpected token parsing instant. Expected String, got {0}.",
                reader.TokenType);

            var calendarSystemId = reader.Value.ToString();
            return CalendarSystem.ForId(calendarSystemId);
        }

        /// <summary>
        /// Writes the calendar system ID to the writer.
        /// </summary>
        /// <param name="writer">The writer to write JSON data to</param>
        /// <param name="value">The value to serializer</param>
        /// <param name="serializer">The serializer to use for nested serialization</param>
        protected override void WriteJsonImpl(JsonWriter writer, CalendarSystem value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Id);
        }
    }
}
