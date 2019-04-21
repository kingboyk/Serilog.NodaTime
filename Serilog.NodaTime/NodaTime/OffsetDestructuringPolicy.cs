// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using NodaTime;
using NodaTime.Text;

namespace Serilog.NodaTime
{
    public sealed class OffsetDestructuringPolicy : DestructuringPolicyBase<Offset>
    {
        protected override IPattern<Offset> Pattern => OffsetPattern.GeneralInvariant;
    }
}