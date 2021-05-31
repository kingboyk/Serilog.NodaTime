using NodaTime;
using NodaTime.Text;

namespace Serilog.NodaTime
{
    internal sealed class YearMonthDestructuringPolicy : DestructuringPolicyBase<YearMonth>
    {
        protected override IPattern<YearMonth> Pattern => YearMonthPattern.Iso;

        public YearMonthDestructuringPolicy() : base(CreateIsoValidator(x => x.Calendar)) { }
    }
}