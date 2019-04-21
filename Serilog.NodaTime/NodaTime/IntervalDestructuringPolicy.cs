// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using NodaTime;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.NodaTime
{
    public sealed class IntervalDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value is Interval interval)
            {
                if (interval.HasStart)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (interval.HasEnd) // start and end
                    {
                        result = propertyValueFactory.CreatePropertyValue(new {interval.Start, interval.End}, destructureObjects: true);
                    }
                    else // start only
                    {
                        result = propertyValueFactory.CreatePropertyValue(new { interval.Start }, destructureObjects: true);
                    }
                }
                else if (interval.HasEnd) // end only
                {
                    result = propertyValueFactory.CreatePropertyValue(new { interval.End }, destructureObjects: true);
                }
                else // neither
                {
                    result = new StructureValue(new LogEventProperty[0]);
                }

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