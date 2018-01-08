using System;
using System.Collections.Generic;
using System.Linq;

namespace SQH.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> ListaDatasExcetoFDS(this DateTime startDate, DateTime endDate)
        {
            var cannotBe = new List<DayOfWeek>() { DayOfWeek.Saturday, DayOfWeek.Sunday };
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Where(x => !cannotBe.Contains(startDate.AddDays(x).DayOfWeek)).Select(d => startDate.AddDays(d));
        }
    }
}
