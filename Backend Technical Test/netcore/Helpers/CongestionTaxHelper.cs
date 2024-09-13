using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Threading;
using congestion.calculator;
using congestion.calculator.enums;
using congestion.calculator.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace congestion.calculator.Helpers
{
    public class CongestionTaxHelper//: ICongestionTaxHelper /* if you want helper not to be static */
    {
        /*
             * Calculate the total toll fee for one day
             *
             * @param vehicle - the vehicle
             * @param dates   - date and time of all passes on one day
             * @return - the total congestion tax for that day
        */

        public virtual Dictionary<DateTime, int> GetTax(DateTime[] dates, Vehicle vehicle)
        {
            var totalFee = 0;
            var dayTax = new Dictionary<DateTime,int>();
            var tempTollFees = new Dictionary<DateTime,int>();
            var orderedDates = dates.OrderBy(q=>q).ToArray();
            DateTime firstDate = orderedDates[0];
            for (var i = 0; i < orderedDates.Count(); i++)
            {
                if(orderedDates[i].Date != firstDate.Date)
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

        public virtual int GetTollFee(DateTime date, Vehicle vehicle)
        {
            var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            if (date >= startOfDay.AddHours(6) && date < startOfDay.AddHours(6).AddMinutes(30))
                return 8;
            if (date >= startOfDay.AddHours(6).AddMinutes(30) && date < startOfDay.AddHours(7))
                return 13;
            if (date >= startOfDay.AddHours(7) && date < startOfDay.AddHours(8))
                return 18;
            if (date >= startOfDay.AddHours(8) && date < startOfDay.AddHours(8).AddMinutes(30))
                return 13;
            if (date >= startOfDay.AddHours(8).AddMinutes(30) && date < startOfDay.AddHours(15))
                return 8;
            if (date >= startOfDay.AddHours(15) && date < startOfDay.AddHours(15).AddMinutes(30))
                return 13;
            if (date >= startOfDay.AddHours(15).AddMinutes(30) && date < startOfDay.AddHours(17))
                return 18;
            if (date >= startOfDay.AddHours(17) && date < startOfDay.AddHours(18))
                return 13;
            if (date >= startOfDay.AddHours(18) && date < startOfDay.AddHours(18).AddMinutes(30))
                return 8;
            else return 0;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            return date.Year == 2013 && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }
        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            return vehicle.GetTollFreeVehiclesType() != TollFreeVehicles.NotTollFree;
        }
    }
}
