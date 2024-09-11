using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Interfaces
{
    public interface ICongestionTaxHelper
    {
        int GetTax(DateTime[] dates, Vehicle vehicle);
        int GetTollFee(DateTime date, Vehicle vehicle);
        bool IsTollFreeDate(DateTime date);
        bool IsTollFreeVehicle(Vehicle vehicle);
    }
}
