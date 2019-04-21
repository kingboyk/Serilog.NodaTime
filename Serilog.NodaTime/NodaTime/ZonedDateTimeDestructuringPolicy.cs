// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

// Contains code from NodaTime.Serialization.JsonNet
// Copyright 2012 The Noda Time Authors.

using NodaTime;
using NodaTime.Text;

namespace Serilog.NodaTime
{
    internal sealed class ZonedDateTimeDestructuringPolicy : DestructuringPolicyBase<ZonedDateTime>
    {
        protected override IPattern<ZonedDateTime> Pattern { get; }

        public ZonedDateTimeDestructuringPolicy(IDateTimeZoneProvider provider) : base(CreateIsoValidator(x => x.Calendar))
        {
            Pattern = ZonedDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFFo<G> z", provider);
        }
    }
}