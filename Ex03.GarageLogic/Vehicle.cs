using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_VehicleModelName;
        private readonly string r_PlateNumber;
        private float m_LeftEnergyPercentage;
        protected PowerSourceSystem m_EnergySource = null;
        protected List<Wheel> m_WheelsList;
        protected List<object> m_ExtraInformation;

        public Vehicle(string i_ModelNameName, string i_PlateNumber, float i_LeftEnergyPercentage)
        {
            r_VehicleModelName = i_ModelNameName;
            r_PlateNumber = i_PlateNumber;
            m_LeftEnergyPercentage = i_LeftEnergyPercentage;
            m_WheelsList = new List<Wheel>();
            m_ExtraInformation = new List<object>();
            m_EnergySource = null;
        }

        public void InitializeWheels(int i_NumberOfWheels, string i_NameOfWheelManufacturer, float i_CurrentWheelPressure, eMaxAirPressure i_MaxWheelPressure)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_NameOfWheelManufacturer, i_CurrentWheelPressure, (float)i_MaxWheelPressure));
            }
        }

        public virtual void SetExtraInfo(object i_Obj1, object i_Obj2) {}

        public virtual void SetPowerSystem(ePowerSystem i_PowerSystem, float i_EnergyPercentage) {}

        public void SetAirPump()
        {
            float airPressureToAdd = 0;
            foreach (Wheel currentWheel in m_WheelsList)
            {
                airPressureToAdd = currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure;
                currentWheel.AddAirToWheel(airPressureToAdd);
            }
        }
        
        public void HandleFueling(float i_AmountOfFuelToAdd)
        {
            this.m_LeftEnergyPercentage = (i_AmountOfFuelToAdd * 100 / EnergySource.EnergyCapacity) + m_LeftEnergyPercentage;
            this.m_EnergySource.AddFuel(i_AmountOfFuelToAdd);
        }

        public void HandleRecharging(float i_AmountOfFuelToAdd)
        {
            this.m_LeftEnergyPercentage = ((i_AmountOfFuelToAdd / 60) * 100 / EnergySource.EnergyCapacity) + m_LeftEnergyPercentage;
            this.m_EnergySource.RechargeBattery(i_AmountOfFuelToAdd);
        }

        public PowerSourceSystem EnergySource
        {
            get { return m_EnergySource; }
        }

        public string VehicleModelName
        {
            get { return r_VehicleModelName; }
        }

        public List<Wheel> WheelsList
        {
            get { return m_WheelsList; }
        }

        public float LeftEnergyPercentage
        {
            get { return m_LeftEnergyPercentage; }
        }

        public List<object> ExtraInformation
        {
            get { return m_ExtraInformation; }
        }

        public string PlateNumber
        {
            get { return r_PlateNumber; }
        }
    }
}