using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Helpers
{
    public class CongestionTaxFromDBHelper : CongestionTaxHelper
    {
        private readonly TaxTestDbContext _taxTestDB;
        public CongestionTaxFromDBHelper(TaxTestDbContext TaxTestDB)
        {
            _taxTestDB = TaxTestDB;
        }

        public override int GetTollFee(DateTime date, Vehicle vehicle)
        {
            var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
            var toll = _taxTestDB.TaxPeriods.Where(q =>
            startOfDay.AddHours(q.StartTime.Hour).AddMinutes(q.StartTime.Minute) <= date &&
            date < startOfDay.AddHours(q.EndTime.Hour).AddMinutes(q.EndTime.Minute)).ToList();//.Max(q => q.TaxAmmount);
            if(toll != null && toll.Count > 0)
            {
                return toll.Max(q=>q.TaxAmmount);
            }
            else
            {
                return 0;
            }
            //return toll;
        }
        public override Dictionary<DateTime, int> GetTax(DateTime[] dates, Vehicle vehicle)
        {
            var totalFee = 0;
            var dayTax = new Dictionary<DateTime, int>();
            var tempTollFees = new Dictionary<DateTime, int>();
            var orderedDates = dates.OrderBy(q => q).ToArray();
            DateTime firstDate = orderedDates[0];
            for (var i = 0; i < orderedDates.Count(); i++)
            {
                if (orderedDates[i].Date != firstDate.Date)
                {

                    firstDate = orderedDates[i];
                    if (tempTollFees.Count > 0)
                    {
                        var maxValue = tempTollFees.Values.Max();
                        if (totalFee < 60)
                        {
                            totalFee += maxValue;
                            if (totalFee > 60)
                            {
                                totalFee = 60;
                            }
                        }
                        tempTollFees = new Dictionary<DateTime, int>();
                        dayTax.Add(orderedDates[i], totalFee);

                    }
                    else
                    {
                        dayTax.Add(orderedDates[i], totalFee);
                    }

                }
                else
                {
                    if (orderedDates[i] != firstDate && orderedDates[i] - firstDate < new TimeSpan(0, 60, 0))
                    {
                        tempTollFees.Add(orderedDates[i], GetTollFee(orderedDates[i], vehicle));
                    }
                    else
                    {
                        firstDate = orderedDates[i];
                        if (tempTollFees.Count > 0)
                        {
                            var maxValue = tempTollFees.Values.Max();
                            if (totalFee < 60)
                            {
                                totalFee += maxValue;
                                if (totalFee > 60)
                                {
                                    totalFee = 60;
                                }
                            }
                            tempTollFees = new Dictionary<DateTime, int>();
                        }
                        else if (totalFee < 60)
                        {
                            totalFee += GetTollFee(orderedDates[i], vehicle);
                            if (totalFee > 60)
                            {
                                totalFee = 60;
                            }
                        }
                    }
                }
            }
            //dayTax.Add(totalFee, firstDate);
            return dayTax;
        }
    }
}
