using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Providers
{
    internal abstract class DateChartDataProviderBase
    {
        private const int MaxMonthCount = 6;

        protected static IDictionary<string, float> GetAmountTimeData(IReadOnlyCollection<IDateAmount> dateAmounts, IDateFilter incomingPaymentFilter)
        {
            int monthDifference = GetMonthCount(incomingPaymentFilter.ToDate) - GetMonthCount(incomingPaymentFilter.FromDate);
            DateTime firstDayOfMonth = GetFirstDayOfMonth(incomingPaymentFilter.ToDate);
            return Enumerable.Range(0, Math.Min(monthDifference, MaxMonthCount))
                .Select(monthCount => firstDayOfMonth.AddMonths(-monthCount))
                .ToDictionary(
                    firstDayOfEachMonth => firstDayOfEachMonth.ToString("MMMM"),
                    firstDayOfEachMonth => dateAmounts
                        .Where(dateAmount => IsAmountInMonth(dateAmount, firstDayOfEachMonth)).Sum(dateAmount => dateAmount.Amount));
        }

        private static int GetMonthCount(DateTime date)
        {
            return (date.Year * 12) + date.Month;
        }

        private static bool IsAmountInMonth(IDateAmount dateAmount, DateTime firstDayOfMonth)
        {
            return dateAmount.Date >= firstDayOfMonth && dateAmount.Date <= GetLastDayOfMonth(firstDayOfMonth);
        }

        private static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private static DateTime GetLastDayOfMonth(DateTime firstDayOfMonth)
        {
            return firstDayOfMonth.AddMonths(1).AddDays(-1);
        }
    }
}