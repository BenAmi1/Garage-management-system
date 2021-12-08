using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly Garage r_MyGarage;
        public const int k_PlateNumberLength = 8;
        public const int k_NoVehiclesFound = 0;
        public const int k_PhoneNumberLength = 10;

        public UserInterface()
        {
            r_MyGarage = new Garage();
            MainMenu();
        }

        public void MainMenu()
        {
            eMenuOptions userInput = eMenuOptions.UnDefined;
            
            Console.WriteLine("Welcome to the garage manager system!\n");
            while(userInput != eMenuOptions.Exit)
            {
                Console.WriteLine("Please choose one of the following options:\n");
                Console.WriteLine("press (1) to add new vehicle to the garage");
                Console.WriteLine("press (2) to show all vehicle's plate numbers");
                Console.WriteLine("press (3) to change status of specific vehicle");
                Console.WriteLine("press (4) to inflate the wheels of a specific vehicle");
                Console.WriteLine("press (5) to refuel a specific vehicle");
                Console.WriteLine("press (6) to recharge battery of a specific vehicle");
                Console.WriteLine("press (7) to show all data about a specific vehicle\n");
                Console.WriteLine("press (0) to exit\n");
                try
                {
                    userInput = UserInput.GetOptionMainMenu();
                    if(userInput != eMenuOptions.AddNewVehicle && userInput != eMenuOptions.Exit)
                    {
                        r_MyGarage.CheckIfGarageIsEmpty();
                    }

                    doUsersChoice(userInput);
                    Console.Clear();
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.RangeErrorToUser());
                }
                catch(InstanceNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    System.Threading.Thread.Sleep(1200); // pause before clear screen
                    Console.Clear();
                }

                Console.Clear();
            }
        }

        private void doUsersChoice(eMenuOptions i_UserInput)
        {
            switch(i_UserInput)
            {
                case eMenuOptions.Exit:
                    break;
                case eMenuOptions.AddNewVehicle:
                    AddNewVehicle();
                    break;
                case eMenuOptions.ShowAllPlateNumbers:
                    showAllPlateNumbers();
                    break;
                case eMenuOptions.ChangeVehicleStatus:
                    changeStatus();
                    break;
                case eMenuOptions.InflateAirInWheels:
                    inflateAirInWheelsToMax();
                    break;
                case eMenuOptions.Refueling:
                    refuelVehicle();
                    break;
                case eMenuOptions.Recharging:
                    rechargeVehicle();
                    break;
                case eMenuOptions.ShowAllVehicleData:
                    showDataOfVehicle();
                    break;
            }
        }

        private void showDataOfVehicle()
        {
            string plateNumber;
            int i = 1;
            List<object> costumerData = null;

            Console.WriteLine("Please insert vehicle's 8 digits plate number you want to his details:");
            plateNumber = UserInput.GetDigitsOnly(k_PlateNumberLength);
            try
            {
                costumerData = r_MyGarage.GetAllCostumerData(plateNumber);
            }
            catch(InstanceNotFoundException ex)
            {
                Console.WriteLine(@"The vehicle carrying plate number: {0}" + ex.Message, plateNumber);
                endFunction();

                return;
            }

            Console.WriteLine(
                @"Costumer name:       | {0}
License Number:      | {1}
Vehicle model name:  | {2}
Vehicle status:      | {3}",
                costumerData[0],
                costumerData[1],
                costumerData[2],
                costumerData[3]);
            Console.WriteLine("\nWheels status:");
            foreach(Wheel wheel in (List<Wheel>)costumerData[4])
            {
                Console.WriteLine(
                    @"Wheel #{0}: manufacturer: {1} | Wheel pressure: {2}",
                    i,
                    wheel.ManufacturerName,
                    wheel.CurrentAirPressure);
                i++;
            }

            Console.WriteLine();
            if(((List<object>)costumerData[5]).Count > 1) // Fuel details are inserted -- > Fuel driven vehicle
            {
                Console.WriteLine(@"Fuel tank: {0}% left", ((List<object>)costumerData[5])[0]);
                Console.WriteLine(@"Fuel type: {0}", ((List<object>)costumerData[5])[1]);
            }
            else
            {
                Console.WriteLine(@"Battery voltage: {0}% left.", ((List<object>)costumerData[5])[0]);
            }
        
            int indexOfCostumer = r_MyGarage.GetCostumerIndex(plateNumber);
            printExtraInformation(costumerData[6], costumerData[7], indexOfCostumer);
            endFunction();
        }
        
        private void printExtraInformation(object i_Obj1, object i_Obj2, int i_IndexOfCostumer)
        {
            Type vehicleType = r_MyGarage.ListOfCostumer[i_IndexOfCostumer].CostumerVehicle.GetType();

            if(vehicleType == typeof(Car))
            {
                Console.WriteLine("The car has {0} doors\nThe color of the car is {1}", (int)i_Obj1, (eCarColor)i_Obj2);
            }

            if(vehicleType == typeof(MotorCycle))
            {
                Console.WriteLine(
                    "The Motorcycle's license type is {0}\nThe engine capacity is {1} CC", (eLicenseType)i_Obj1, (int)i_Obj2);
            }

            if(vehicleType == typeof(Truck))
            {
                if((bool)i_Obj1)
                {
                    Console.WriteLine("The truck is carrying dangerous cargo\nThe max carrying weight is: {0} Kilograms",
                        (float)i_Obj2);
                }
                else
                {
                    Console.WriteLine(
                        "The truck does not carrying dangerous cargo\nThe max carrying weight is: {0} kilograms", (float)i_Obj2);
                }
            }
        }

        private void rechargeVehicle()
        {
            string plateNumber = null;
            int amountOfMinutes = 0;

            try
            {
                amountOfMinutes = UserInput.GetAmountOfMinutesToAdd();
                Console.WriteLine("Please insert vehicle's 8 digits plate number you want to charge:");
                plateNumber = UserInput.GetDigitsOnly(k_PlateNumberLength);
                r_MyGarage.Recharge(plateNumber, amountOfMinutes);
                Console.WriteLine("Vehicle has been recharged!");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.ChargeBeyondCapacity());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(InstanceNotFoundException ex)
            {
                Console.WriteLine(@"The vehicle carrying plate number: {0}" + ex.Message, plateNumber);
            }

            endFunction();
        }

        private void refuelVehicle()
        {
            string plateNumber = null;
            float amountOfFuel = 0;
            eFuelTypes fuelType = 0;

            try
            {
                fuelType = UserInput.GetFuelType();
                amountOfFuel = UserInput.GetFuelQuantityToAdd();
                Console.WriteLine("Please insert vehicle's 8 digits plate number you want to fuel");
                plateNumber = UserInput.GetDigitsOnly(8);
                r_MyGarage.Refuel(plateNumber, fuelType, amountOfFuel);
                Console.WriteLine("Vehicle has been refueled!");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.RefuelingBeyondCapacity());
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(InstanceNotFoundException ex)
            {
                Console.WriteLine(@"The vehicle carrying plate number: {0}" + ex.Message, plateNumber);
            }

            endFunction();
        }

        private void inflateAirInWheelsToMax()
        {
            string plateNumber;

            Console.WriteLine("Please insert vehicle's 8 digits plate number");
            plateNumber = UserInput.GetDigitsOnly(8);
            try
            {
                r_MyGarage.PumpAirToWheels(plateNumber);
                Console.WriteLine(@"The wheels of vehicle carrying plate number: {0} has been inflated to maximum ", plateNumber);
            }
            catch (InstanceNotFoundException ex)
            {
                Console.WriteLine(@"The vehicle carrying plate number: {0}" + ex.Message, plateNumber);
            }

            endFunction();
        }

        private void changeStatus()
        {
            string plateNumber;
            eVehicleStatus newStatus = UserInput.GetVehicleStatus();

            Console.Clear();
            Console.WriteLine("Please insert the 8 digits plate number of the vehicle you want to change his status:");
            plateNumber = UserInput.GetDigitsOnly(k_PlateNumberLength);
            try
            {
                r_MyGarage.ChangeVehicleStatus(plateNumber, newStatus);
                Console.WriteLine(@"The status of vehicle carrying plate number: {0} changed to {1}", plateNumber, newStatus);
            }
            catch (InstanceNotFoundException ex)
            {
                Console.WriteLine(@"The vehicle carrying plate number: {0}" + ex.Message, plateNumber);
            }

            endFunction();
        }

        private void showAllPlateNumbers()
        {
            eVehicleStatus userInput;
            bool allVehicle;
            string currentPlateNumber = null;
            int counter = 0;

            if (r_MyGarage.ListOfCostumer.Count == 0)
            {
                Console.WriteLine("No cars in the garage!");
                endFunction();
                return;
            }

            userInput = UserInput.GetVehicleStatusForPlateNumbers();
            allVehicle = userInput != eVehicleStatus.HasBeenPaid && userInput != eVehicleStatus.Repaired &&
                                  userInput != eVehicleStatus.InProcess;
            for(int i = 0; i < r_MyGarage.ListOfCostumer.Count; i++)
            {
                currentPlateNumber = r_MyGarage.ListOfCostumer[i].CostumerVehicle.PlateNumber;
                if (!allVehicle)
                {
                    if (r_MyGarage.ListOfCostumer[i].VehicleStatus == userInput)
                    {
                        Console.WriteLine(@"vehicle #{0}: [{1}]", counter+1, currentPlateNumber);
                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine(@"vehicle #{0}: [{1}]", counter+1, currentPlateNumber);
                    counter++;
                }
            }

            if(k_NoVehiclesFound == counter)
            {
                Console.WriteLine(@"No vehicles found in status: '{0}'", userInput);
            }

            endFunction();
        }

        public void AddNewVehicle()
        {
            int index;
            int currentWheelPressure;
            string plateNumber;
            string costumerName;
            string PhoneNumber;
            string NameModel;
            string NameOfWheelManufacturer;
            float currentEnergyPercentage;
            bool costumerFound;
            eVehicleType vehicleType;
            ePowerSystem powerSystem;
            KeyValuePair<object, object> extraInformationPair;

            Console.WriteLine("Please insert vehicle's 8 digits plate number");
            plateNumber = UserInput.GetDigitsOnly(k_PlateNumberLength);
            index = r_MyGarage.GetCostumerIndex(plateNumber);
            costumerFound = index != UserInput.k_NotFound;
            if(costumerFound)
            {
                r_MyGarage.ChangeVehicleStatus(plateNumber, eVehicleStatus.InProcess);
                Console.WriteLine("Vehicle is already exist! Updated vehicle status: in process");
            }
            else
            {
                Console.WriteLine("Please enter costumer's name:");
                costumerName = UserInput.GetCostumerName();
                Console.WriteLine("Please enter costumer's 10 digits phone number:");
                PhoneNumber = UserInput.GetDigitsOnly(k_PhoneNumberLength);
                Console.WriteLine("Please choose which vehicle type to insert");
                vehicleType = UserInput.GetVehicleType();
                powerSystem = UserInput.GetPowerSourceSystem(vehicleType);
                Console.WriteLine("Please insert the name of vehicle model");
                NameModel = Console.ReadLine();
                Console.WriteLine("Please insert current energy percentage");
                currentEnergyPercentage = UserInput.GetUserEnergyPercentage();
                Console.WriteLine("Please insert the name of wheel manufacturer");
                NameOfWheelManufacturer = Console.ReadLine();
                Console.WriteLine("Please insert current wheel pressure");
                currentWheelPressure = UserInput.GetCurrentWheelPressure(vehicleType);
                extraInformationPair = UserInput.GetExtraInformation(vehicleType);
                r_MyGarage.AddCostumerToGarage(costumerName, PhoneNumber, vehicleType, powerSystem, NameModel, plateNumber,
                                               currentEnergyPercentage, NameOfWheelManufacturer, currentWheelPressure,
                                               extraInformationPair.Key,
                                               extraInformationPair.Value);
                Console.WriteLine("The vehicle was added to the garage!");
            }

            endFunction();
        }

        private void endFunction()
        {
            Console.WriteLine("\nPlease press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }
}