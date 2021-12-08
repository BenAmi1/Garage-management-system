namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_Color;
        private eAmountOfDoors m_AmountOfDoors;
        
        public Car(string i_ModelName, string i_PlateNumber, float i_EnergyPercentage, string i_NameOfWheelManufacturer, 
               float i_CurrentWheelPressure, int i_NumberOfWheels, eMaxAirPressure i_MaxWheelPressure)
            : base(i_ModelName, i_PlateNumber, i_EnergyPercentage) 
        {
            InitializeWheels(i_NumberOfWheels, i_NameOfWheelManufacturer, i_CurrentWheelPressure, i_MaxWheelPressure);
        }
        
        public override void SetExtraInfo(object i_Obj1, object i_Obj2)
        {
            m_Color = (eCarColor)i_Obj1;
            m_AmountOfDoors = (eAmountOfDoors)i_Obj2;
            ExtraInformation.Add(m_Color);
            ExtraInformation.Add(m_AmountOfDoors);
        }

        public override void SetPowerSystem(ePowerSystem i_PowerSystem, float i_EnergyPercentage)
        {
            if (i_PowerSystem == ePowerSystem.ElectricEngine)
            {
                m_EnergySource = new ElectricPoweredSystem(3.2f, i_EnergyPercentage);
            }
            else
            {
                m_EnergySource = new FuelPoweredSystem(45f, eFuelTypes.Octan95, i_EnergyPercentage);
            }
        }

        public eCarColor ColorOfCar
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eAmountOfDoors NumberOfDoors
        {
            get { return m_AmountOfDoors; }
        }
    }
}