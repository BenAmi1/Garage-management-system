namespace Ex03.GarageLogic
{
    public class ElectricPoweredSystem : PowerSourceSystem
    {
        public ElectricPoweredSystem(float i_BatteryCapacity, float i_BatteryLeftPercentage)
            : base(i_BatteryCapacity, i_BatteryLeftPercentage) {}

        public override void RechargeBattery(float i_AmountOfMinutesEnergyToAdd)
        {
            CurrentAmountOfEnergyLeft += (i_AmountOfMinutesEnergyToAdd / 60);
        }
    }
}