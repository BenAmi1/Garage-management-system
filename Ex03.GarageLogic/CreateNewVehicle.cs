namespace Ex03.GarageLogic
{
    public class CreateNewVehicle
    {
        public static Vehicle CreateVehicle(eVehicleType TypeOfVehicle, ePowerSystem i_PowerSource, string i_ModelName,
                                            string i_PlateNumber, float i_EnergyPercentage,
                                            string i_NameOfWheelManufacturer, float i_CurrentWheelPressure,
                                            object i_ExtraInfo1, object i_ExtraInfo2)
        {
            Vehicle newVehicle = null;
            switch (TypeOfVehicle)
            {
                case eVehicleType.Car:
                    newVehicle = new Car(i_ModelName, i_PlateNumber, i_EnergyPercentage, i_NameOfWheelManufacturer,
                                     i_CurrentWheelPressure, 4, eMaxAirPressure.CarMaxAirPressure);
                    break;
                case eVehicleType.MotorCycle:
                    newVehicle = new MotorCycle(i_ModelName, i_PlateNumber, i_EnergyPercentage, i_NameOfWheelManufacturer, 
                                     i_CurrentWheelPressure, 2, eMaxAirPressure.MotorCycleMaxAirPressure);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_ModelName, i_PlateNumber, i_EnergyPercentage, i_NameOfWheelManufacturer,
                                     i_CurrentWheelPressure, 16, eMaxAirPressure.TruckMaxAirPressure);
                    break;
            }

            newVehicle.SetExtraInfo(i_ExtraInfo1, i_ExtraInfo2);
            newVehicle.SetPowerSystem(i_PowerSource, i_EnergyPercentage);

            return newVehicle;
        }

        public static eMaxAirPressure GetMaxAirPressure(eVehicleType i_TypeOfVehicle)
        {
            eMaxAirPressure vehicleMaxAirPressure = 0;

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.Car:
                    vehicleMaxAirPressure = eMaxAirPressure.CarMaxAirPressure;
                    break;
                case eVehicleType.Truck:
                    vehicleMaxAirPressure = eMaxAirPressure.TruckMaxAirPressure;
                    break;
                case eVehicleType.MotorCycle:
                    vehicleMaxAirPressure = eMaxAirPressure.MotorCycleMaxAirPressure;
                    break;
            }

            return vehicleMaxAirPressure;
        }
    }
}