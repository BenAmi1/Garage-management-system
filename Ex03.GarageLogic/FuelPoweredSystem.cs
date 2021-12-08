using System;

namespace Ex03.GarageLogic
{
    public class FuelPoweredSystem : PowerSourceSystem
    {
        private eFuelTypes m_FuelType;

        public FuelPoweredSystem(float i_FuelTankCapacity, eFuelTypes i_FuelType, float i_FuelLeftPercentage)
            : base(i_FuelTankCapacity, i_FuelLeftPercentage)
        {
            m_FuelType = i_FuelType;
        }

        public override void AddFuel(float i_AmountOfFuelEnergyToAdd)
        {
            CurrentAmountOfEnergyLeft += i_AmountOfFuelEnergyToAdd;
        }

        public eFuelTypes FuelType
        {
            get { return m_FuelType; }
        }
    }
}