using congestion.calculator.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator
{
    public class Motorbike : Vehicle
    {
        private TollFreeVehicles _tollFreeVehicles { get; set; }
        private VehicleTypes _vehicleTypes { get; set; }

        public Motorbike()
        {

        }
        public Motorbike(TollFreeVehicles TollFreeVehicles, VehicleTypes VehicleTypes)
        {
            _vehicleTypes = VehicleTypes;
            _tollFreeVehicles = TollFreeVehicles;
        }

        public VehicleTypes GetVehicleType()
        {
            return _vehicleTypes;
        }

        public TollFreeVehicles GetTollFreeVehiclesType()
        {
            return _tollFreeVehicles;
        }
    }
}
