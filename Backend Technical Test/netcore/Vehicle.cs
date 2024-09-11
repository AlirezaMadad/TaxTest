using congestion.calculator.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator
{
    public interface Vehicle
    {
        VehicleTypes GetVehicleType();
        TollFreeVehicles GetTollFreeVehiclesType();
    }
}