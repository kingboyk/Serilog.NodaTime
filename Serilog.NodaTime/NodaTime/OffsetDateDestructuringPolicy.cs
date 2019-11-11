// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using NodaTime;
using NodaTime.Text;

namespace Serilog.NodaTime
{
    internal sealed class OffsetDateDestructuringPolicy : DestructuringPolicyBase<OffsetDate>
    {
        protected override IPattern<OffsetDate> Pattern => OffsetDatePattern.GeneralIso;

        public OffsetDateDestructuringPolicy() : base(CreateIsoValidator(x => x.Calendar)) { }
    }
}