namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_DangerousCargo;
        private float m_MaxCargoWeight;

        public Truck(string i_ModelName, string i_PlateNumber, float i_EnergyPercentage, string i_NameOfWheelManufacturer,
                     float i_CurrentWheelPressure, int i_NumberOfWheels, eMaxAirPressure i_MaxWheelPressure)
                     :base(i_ModelName, i_PlateNumber, i_EnergyPercentage)
        {
            InitializeWheels(i_NumberOfWheels, i_NameOfWheelManufacturer, i_CurrentWheelPressure, i_MaxWheelPressure);
        }

        public override void SetExtraInfo(object i_Obj1, object i_Obj2)
        {
            m_DangerousCargo = (bool)i_Obj1;
            m_MaxCargoWeight = (float)i_Obj2;
            ExtraInformation.Add(m_DangerousCargo);
            ExtraInformation.Add(m_MaxCargoWeight);
        }

        public override void SetPowerSystem(ePowerSystem i_PowerSystem, float i_EnergyPercentage)
        {
            m_EnergySource = new FuelPoweredSystem(120f, eFuelTypes.Soler, i_EnergyPercentage);
        }

        public bool DangerousCargo
        {
            get { return m_DangerousCargo; }
        }

        public float MaxCargoWeight
        {
            get { return m_MaxCargoWeight; }
        }
    }
}
