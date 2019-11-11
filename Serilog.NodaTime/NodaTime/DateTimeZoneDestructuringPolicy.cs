// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using NodaTime;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.NodaTime
{
    internal sealed class DateTimeZoneDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue? result)
        {
            if (value is DateTimeZone dtz)
            {
                result = propertyValueFactory.CreatePropertyValue(dtz.Id);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}