using System;
using System.Collections.Generic;
using System.Management.Instrumentation;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<Costumer> r_CostumersList;
        public const int k_NotFound = -1;

        public Garage()
        {
            r_CostumersList = new List<Costumer>();
        }

        public void AddCostumerToGarage(string i_CostumerName, string i_StringPhoneNumber, eVehicleType i_VehicleType,
                                        ePowerSystem i_PowerSystem, string i_ModelName, string i_PlateNumber, 
                                        float i_CurrentEnergyPercentage, string i_NameOfWheelManufacturer,
                                        float i_CurrentWheelPressure, object i_ExtraInformation1, object i_ExtraInformation2)
        {
            r_CostumersList.Add(new Costumer(i_CostumerName, i_StringPhoneNumber, i_VehicleType, i_PowerSystem,
                                                 i_ModelName, i_PlateNumber, i_CurrentEnergyPercentage,
                                                 i_NameOfWheelManufacturer, i_CurrentWheelPressure,
                                                 i_ExtraInformation1, i_ExtraInformation2));
        }

        public void CheckIfGarageIsEmpty()
        {
            if (0 == r_CostumersList.Count)
            {
                throw new InstanceNotFoundException("No vehicles found in the garage!");
            }
        }

        public void ChangeVehicleStatus(string i_PlateNumber, eVehicleStatus i_NewStatus)
        {
            int index = GetCostumerIndex(i_PlateNumber);
            bool costumerFound = index != k_NotFound;

            if (costumerFound)
            {
                r_CostumersList[index].VehicleStatus = i_NewStatus;
            }
            else
            {
                throw new InstanceNotFoundException(" was not found in the garage");
            }
        }

        public int GetCostumerIndex(string i_PlateNumber)
        {
            int index = k_NotFound;
            for(int i = 0; i < r_CostumersList.Count; i++)
            {
                if(r_CostumersList[i].CostumerVehicle.PlateNumber == i_PlateNumber)
                {
                    index = i;
                }
            }

            return index;
        }

        public void PumpAirToWheels(string i_PlateNumber)
        {
            int index = GetCostumerIndex(i_PlateNumber);
            bool costumerFound = index != k_NotFound;

            if (costumerFound)
            {
                r_CostumersList[index].CostumerVehicle.SetAirPump();
            }
            else
            {
                throw new InstanceNotFoundException(" was not found in the garage");
            }
        }

        public void Refuel(string i_PlateNumber, eFuelTypes i_FuelType, float i_FuelQuantityToAdd)
        {
            float currentAmountOfFuel;
            float fuelTankCapacity;
            eFuelTypes currentFuelType;
            int index = GetCostumerIndex(i_PlateNumber);
            bool costumerFound = index != k_NotFound;

            if (costumerFound)
            {
                if(r_CostumersList[index].CostumerVehicle.EnergySource.GetType() == typeof(FuelPoweredSystem))
                {
                    currentAmountOfFuel = r_CostumersList[index].CostumerVehicle.EnergySource.CurrentAmountOfEnergyLeft;
                    fuelTankCapacity = r_CostumersList[index].CostumerVehicle.EnergySource.EnergyCapacity;
                    currentFuelType = ((FuelPoweredSystem)r_CostumersList[index].CostumerVehicle.EnergySource).FuelType;
                    if(currentFuelType == i_FuelType)
                    {
                        if(i_FuelQuantityToAdd + currentAmountOfFuel <= fuelTankCapacity)
                        {
                            r_CostumersList[index].CostumerVehicle.HandleFueling(i_FuelQuantityToAdd);
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(fuelTankCapacity - currentAmountOfFuel, 0);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Wrong type of fuel!");
                    }
                }
                else
                {
                    throw new ArgumentException("Wrong Argument - This vehicle in not fuel driven!");
                }
            }
            else
            {
                throw new InstanceNotFoundException(" was not found in the garage");
            }
        }

        public void Recharge(string i_PlateNumber, float i_MinutesToRecharge)
        {
            int index = GetCostumerIndex(i_PlateNumber);
            bool costumerFound = index != k_NotFound;
            float currentAmountOfHoursLeft = 0;
            float batteryCapacity = 0;

            if (costumerFound)
            {
                currentAmountOfHoursLeft = r_CostumersList[index].CostumerVehicle.EnergySource.CurrentAmountOfEnergyLeft;
                batteryCapacity = r_CostumersList[index].CostumerVehicle.EnergySource.EnergyCapacity;
                if (r_CostumersList[index].CostumerVehicle.EnergySource.GetType() == typeof(ElectricPoweredSystem))
                {
                    if ((i_MinutesToRecharge / 60) + currentAmountOfHoursLeft <= batteryCapacity)
                    {
                        r_CostumersList[index].CostumerVehicle.HandleRecharging(i_MinutesToRecharge);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(batteryCapacity - currentAmountOfHoursLeft, 0);
                    }
                }
                else
                {
                    throw new ArgumentException("This vehicle is not Electricity driven!");
                }
            }
            else
            {
                throw new InstanceNotFoundException(" was not found in the garage");
            }
        }
        
        public List<object> GetAllCostumerData(string i_PlateNumber)
        {
            int index = GetCostumerIndex(i_PlateNumber);
            bool costumerFound = index != k_NotFound;
            List<object> dataOfCostumer = new List<object>();

            if(costumerFound)
            {
                Costumer currentCostumer = r_CostumersList[index];
                List<object> powerSourceObjects = new List<object>();
                dataOfCostumer.Add(currentCostumer.CostumerName);
                dataOfCostumer.Add(currentCostumer.CostumerVehicle.PlateNumber);
                dataOfCostumer.Add(currentCostumer.CostumerVehicle.VehicleModelName);
                dataOfCostumer.Add(currentCostumer.VehicleStatus.ToString());
                dataOfCostumer.Add(currentCostumer.CostumerVehicle.WheelsList);
                powerSourceObjects.Add(currentCostumer.CostumerVehicle.LeftEnergyPercentage);
                if (currentCostumer.CostumerVehicle.EnergySource.GetType() == typeof(FuelPoweredSystem))
                {
                    powerSourceObjects.Add(((FuelPoweredSystem)currentCostumer.CostumerVehicle.EnergySource).FuelType.ToString());
                }

                dataOfCostumer.Add(powerSourceObjects);
                dataOfCostumer.Add(currentCostumer.CostumerVehicle.ExtraInformation[0]);
                dataOfCostumer.Add(currentCostumer.CostumerVehicle.ExtraInformation[1]);

                return dataOfCostumer;
            }
            else
            {
                throw new InstanceNotFoundException(" was not found in the garage");
            }
        }

        public List<Costumer> ListOfCostumer
        {
            get { return r_CostumersList; }
        }
    }
}