namespace Ex03.GarageLogic
{
    public class MotorCycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public MotorCycle(string i_ModelName, string i_PlateNumber, float i_EnergyPercentage, string i_NameOfWheelManufacturer,
                          float i_CurrentWheelPressure, int i_NumberOfWheels, eMaxAirPressure i_MaxWheelPressure)
            : base(i_ModelName, i_PlateNumber, i_EnergyPercentage)
        {
            InitializeWheels(i_NumberOfWheels, i_NameOfWheelManufacturer, i_CurrentWheelPressure, i_MaxWheelPressure);
        }

        public override void SetExtraInfo(object i_Obj1, object i_Obj2)
        {
            m_LicenseType = (eLicenseType)i_Obj1;
            m_EngineCapacity = (int)i_Obj2;
            ExtraInformation.Add(m_LicenseType);
            ExtraInformation.Add(m_EngineCapacity);
        }

        public override void SetPowerSystem(ePowerSystem i_PowerSystem, float i_EnergyPercentage)
        {
            if (i_PowerSystem == ePowerSystem.ElectricEngine)
            {
                m_EnergySource = new ElectricPoweredSystem(1.8f, i_EnergyPercentage);
            }
            else
            {
                m_EnergySource = new FuelPoweredSystem(6f, eFuelTypes.Octan98, i_EnergyPercentage);
            }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
        }
    }
}