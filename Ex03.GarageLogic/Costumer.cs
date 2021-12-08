namespace Ex03.GarageLogic
{
    public class Costumer
    {
        private readonly string r_NameOfCostumer;
        private readonly string r_PhoneNumber;
        private readonly Vehicle r_CostumerVehicle; 
        private eVehicleStatus m_VehicleStatus;

        public Costumer(string i_NameOfCostumer, string i_PhoneNumber, eVehicleType i_VehicleType,
                        ePowerSystem i_PowerSource, string i_NameOfModel, string i_PlateNumber, float i_EnergyPercentage, 
                        string i_NameOfWheelManufacturer, float i_CurrentWheelPressure,
                        object i_ExtraInfo1, object i_ExtraInfo2)
        {
            r_NameOfCostumer = i_NameOfCostumer;
            r_PhoneNumber = i_PhoneNumber;
            m_VehicleStatus = eVehicleStatus.InProcess;
            r_CostumerVehicle = CreateNewVehicle.CreateVehicle(i_VehicleType, i_PowerSource, i_NameOfModel, i_PlateNumber,
                                                               i_EnergyPercentage, i_NameOfWheelManufacturer,
                                                               i_CurrentWheelPressure, i_ExtraInfo1, i_ExtraInfo2);
        }

        public Vehicle CostumerVehicle
        {
            get { return r_CostumerVehicle; }
        }

        public string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public string CostumerName
        {
            get { return r_NameOfCostumer; }
        }
    }
}