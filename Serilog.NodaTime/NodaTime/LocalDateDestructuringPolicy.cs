﻿// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using NodaTime;
using NodaTime.Text;

namespace Serilog.NodaTime
{
    internal sealed class LocalDateDestructuringPolicy : DestructuringPolicyBase<LocalDate>
    {
        protected override IPattern<LocalDate> Pattern => LocalDatePattern.Iso;

        public LocalDateDestructuringPolicy() : base(CreateIsoValidator(x => x.Calendar)) { }
    }
}