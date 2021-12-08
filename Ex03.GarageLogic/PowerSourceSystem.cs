namespace Ex03.GarageLogic
{
    public abstract class PowerSourceSystem
    {
        private readonly float r_EnergyCapacity;
        private float m_CurrentAmountOfEnergyLeft;

        public PowerSourceSystem(float i_EnergyCapacity, float i_EnergyLeftPercentagePercentage)
        {
            r_EnergyCapacity = i_EnergyCapacity;
            m_CurrentAmountOfEnergyLeft = i_EnergyLeftPercentagePercentage / 100 * r_EnergyCapacity;
        }

        public virtual void AddFuel(float i_AmountOfFuelEnergyToAdd) {}

        public virtual void RechargeBattery(float i_AmountOfMinutesEnergyToAdd) {}

        public float CurrentAmountOfEnergyLeft
        {
            get { return m_CurrentAmountOfEnergyLeft; }
            set { m_CurrentAmountOfEnergyLeft = value; }
        }

        public float EnergyCapacity
        {
            get { return r_EnergyCapacity; }
        }
    }
}