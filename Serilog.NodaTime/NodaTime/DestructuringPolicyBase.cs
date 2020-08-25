// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

// Contains code from NodaTime.Serialization.JsonNet
// Copyright 2012 The Noda Time Authors.

using System;
using NodaTime;
using NodaTime.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.NodaTime
{
    internal abstract class DestructuringPolicyBase<T> : IDestructuringPolicy
    {
        protected abstract IPattern<T> Pattern { get; }
        protected readonly Action<T>? Validator;

        protected DestructuringPolicyBase(Action<T>? validator = null)
        {
            Validator = validator;
        }

        public virtual bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue? result)
        {
            if (value is T t)
            {
                Validator?.Invoke(t);
                result = propertyValueFactory.CreatePropertyValue(Pattern.Format(t));
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        protected static Action<T> CreateIsoValidator(Func<T, CalendarSystem> calendarProjection)
        {
            return value =>
            {
                var calendar = calendarProjection(value);
                // We rely on CalendarSystem.Iso being a singleton here.
                Preconditions.CheckArgument(calendar == CalendarSystem.Iso, "Values of type {0} must (currently) use the ISO calendar in order to be serialized.", typeof(T).Name);
            };
        }
    }
}