using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class UserInput
    {
        public const int k_NotFound = -1;
        public const int k_UnDefined = -1;
        public const int k_ShowAllPlateNumbers = 1;

        public static KeyValuePair<object, object> GetExtraInformation(eVehicleType i_VehicleType)
        {
            KeyValuePair<object, object> extraInformation = new KeyValuePair<object, object>(null, null);

            switch(i_VehicleType)
            {
                case eVehicleType.Car:
                    extraInformation = GetCarExtraInfo();
                    break;
                case eVehicleType.MotorCycle:
                    extraInformation = GetMotorCycleExtraInfo();
                    break;
                case eVehicleType.Truck:
                    extraInformation = GetTruckExtraInfo();
                    break;
            }

            Console.Clear();

            return extraInformation;
        }

        public static KeyValuePair<object, object> GetMotorCycleExtraInfo()
        {
            int i = 0;
            eLicenseType validLicenseType;
            int validEngineCapacity;
            KeyValuePair<object, object> extraInformation = new KeyValuePair<object, object>(null, null);
            string userInput;

            Console.WriteLine("Please insert the type of motorcycle's license:");
            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                Console.WriteLine(@"press ({0}) for {1}", i, licenseType);
                i++;
            }

            userInput = Console.ReadLine();
            while (!eLicenseType.TryParse(userInput, out validLicenseType) ||
                   !((int)validLicenseType >= 0 && (int)validLicenseType <= i - 1))
            {
                Console.WriteLine(@"Wrong input. The possible amount of doors is between {0} to {1}", 0, i-1);
                userInput = Console.ReadLine();
            }

            Console.WriteLine("Please insert the motorcycle's engine capacity:");
            userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out validEngineCapacity) || validEngineCapacity <= 0)
            {
                Console.WriteLine("Wrong input. please insert engine capacity (positive number)");
                userInput = Console.ReadLine();
            }

            Console.Clear();
            extraInformation = new KeyValuePair<object, object>(validLicenseType, validEngineCapacity);

            return extraInformation;
        }

        public static KeyValuePair<object, object> GetTruckExtraInfo()
        {
            KeyValuePair<object, object> extraInformation = new KeyValuePair<object, object>(null, null);
            string userInput;
            int validInputCargo;
            bool validInputBool;
            float validInputCarryWeight;

            Console.WriteLine("Please press (1) if the truck carries any dangerous cargo, or press (0) if it doesn't");
            userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out validInputCargo) || (validInputCargo != 1 && validInputCargo != 0))
            {
                Console.WriteLine("Wrong input. press (1) if the truck carries any dangerous cargo, or press (0) if it doesn't");
                userInput = Console.ReadLine();
            }

            validInputBool = validInputCargo > 0;
            Console.WriteLine("Please insert the maximum carrying weight of the truck");
            userInput = Console.ReadLine();
            while (!float.TryParse(userInput, out validInputCarryWeight) || (validInputCarryWeight < 0))
            {
                Console.WriteLine("Wrong input. please insert the maximum carrying weight of the truck ");
                userInput = Console.ReadLine();
            }

            Console.Clear();
            extraInformation = new KeyValuePair<object, object>(validInputBool, validInputCarryWeight);

            return extraInformation;
        }

        public static KeyValuePair<object, object> GetCarExtraInfo()
        {
            int i = 0;
            KeyValuePair<object, object> extraInformation = new KeyValuePair<object, object>(null, null);
            eAmountOfDoors validAmountOfDoors = 0;
            string userInput;

            Console.WriteLine("Please insert the amount of doors of the car");
            foreach (eAmountOfDoors amountOfDoors in Enum.GetValues(typeof(eAmountOfDoors)))
            {
                Console.WriteLine(@"press ({0}) for {1}", i, amountOfDoors);
                i++;
            }

            userInput = Console.ReadLine();
            while (!eAmountOfDoors.TryParse(userInput, out validAmountOfDoors) || !((int)validAmountOfDoors >= 0 && (int)validAmountOfDoors <= i - 1))
            {
                Console.WriteLine(@"Wrong input. The possible amount of doors is between {0} to {1}", 0, i-1);
                userInput = Console.ReadLine();
            }

            i = 0;
            foreach (eCarColor color in Enum.GetValues(typeof(eCarColor)))
            {
                Console.WriteLine(@"press ({0}) for {1}", i, color);
                i++;
            }

            eCarColor validCarColor = 0;
            userInput = Console.ReadLine();
            while (!eCarColor.TryParse(userInput, out validCarColor) || !((int)validCarColor >= 0 && (int)validCarColor <= i - 1))
            {
                Console.WriteLine("Wrong input. please press one of the above options");
                userInput = Console.ReadLine();
            }

            Console.Clear();
            extraInformation = new KeyValuePair<object, object>(validCarColor, validAmountOfDoors);

            return extraInformation;
        }

        public static int GetCurrentWheelPressure(eVehicleType i_VehicleType)
        {
            int validInput = k_UnDefined;
            string userInput = Console.ReadLine();
            int maxWheelPressure = (int)CreateNewVehicle.GetMaxAirPressure(i_VehicleType);

            while (!int.TryParse(userInput, out validInput) || !(validInput >= 0 && validInput <= maxWheelPressure))
            {
                Console.WriteLine("Wrong input. The maximum air pressure in this type of vehicle is {0} PSI!", maxWheelPressure);
                userInput = Console.ReadLine();
            }

            Console.Clear();

            return validInput;
        }

        public static float GetUserEnergyPercentage()
        {
            float validInput = k_UnDefined;
            string userInput = Console.ReadLine();

            while (!float.TryParse(userInput, out validInput) || !(validInput >= 0 && validInput <= 100))
            {
                Console.WriteLine("Wrong input. please insert the  remaining percentage of energy");
                userInput = Console.ReadLine();
            }

            Console.Clear();

            return validInput;
        }

        public static ePowerSystem GetPowerSourceSystem(eVehicleType i_VehicleType)
        {
            ePowerSystem chosenPowerSystem;
            string userInput;
            int i = 0;

            if (i_VehicleType == eVehicleType.Truck)
            {
                chosenPowerSystem = ePowerSystem.FuelDrivenEngine;
            }
            else
            {
                Console.WriteLine("Please choose drive system:");
                foreach (ePowerSystem powerSystem in Enum.GetValues(typeof(ePowerSystem)))
                {
                    Console.WriteLine(@"press ({0}) for {1}", i, powerSystem);
                    i++;
                }

                userInput = Console.ReadLine();
                while (!eVehicleType.TryParse(userInput, out chosenPowerSystem) ||
                       ((int)chosenPowerSystem > 1 || (int)chosenPowerSystem < 0))
                {
                    Console.WriteLine("Wrong input. please choose one of the above options");
                    userInput = Console.ReadLine();
                }
            }

            Console.Clear();

            return chosenPowerSystem;
        }

        public static eVehicleType GetVehicleType()
        {
            string userInput;
            eVehicleType validInput;
            int i = 0;

            foreach (eVehicleType v in Enum.GetValues(typeof(eVehicleType)))
            {
                Console.WriteLine(@"press ({0}) for {1}", i, v);
                i++;
            }

            userInput = Console.ReadLine();
            while (!eVehicleType.TryParse(userInput, out validInput) || ((int)validInput > i-1 || (int)validInput < 0))
            {
                Console.WriteLine("Wrong input. Please choose one of the above options");
                userInput = Console.ReadLine();
            }

            Console.Clear();

            return validInput;
        }

        public static string GetDigitsOnly(int i_AmountOfDesireDigits)
        {
            string userInput;
            bool inputIsValid;
            int i = 0;

            do
            {
                i = 0;
                inputIsValid = true;
                userInput = Console.ReadLine();
                foreach (char c in userInput)
                {
                    if (!char.IsDigit(c))
                    {
                        inputIsValid = false;
                        break;
                    }

                    i++;
                }

                if (i != i_AmountOfDesireDigits)
                {
                    inputIsValid = false;
                }

                if (!inputIsValid)
                {
                    Console.WriteLine(@"Wrong input. please enter {0} digits phone number", i_AmountOfDesireDigits);
                }
            }
            while (!inputIsValid);

            Console.Clear();

            return userInput;
        }

        public static string GetCostumerName()
        {
            string userInput;
            bool inputIsValid;

            do
            {
                inputIsValid = true;
                userInput = Console.ReadLine();
                foreach (char c in userInput)
                {
                    if (!char.IsLetter(c))
                    {
                        inputIsValid = false;
                    }
                }
            }
            while (!inputIsValid);

            Console.Clear();

            return userInput;
        }

        public static eMenuOptions GetOptionMainMenu()
        {
            eMenuOptions validInput = eMenuOptions.UnDefined;
            string userInput = Console.ReadLine();

            if (!eMenuOptions.TryParse(userInput, out validInput) || !((int)validInput >= 0 && (int)validInput <= 7))
            {
                throw new ValueOutOfRangeException(7, 0);
            }

            Console.Clear();

            return validInput;
        }

        public static eVehicleStatus GetVehicleStatusForPlateNumbers()
        {
            eVehicleStatus vehicleStatus;
            int validInput = 0;
            string userInput;

            Console.WriteLine("Press (1) to show all plate numbers\nPress (2) to show by vehicle status");
            userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out validInput) || (validInput != 1 && validInput != 2))
            {
                Console.WriteLine(@"Wrong input. Press (1) to show all plate numbers\nPress (2) to show by vehicle status");
                userInput = Console.ReadLine();
            }

            if(k_ShowAllPlateNumbers == validInput)
            {
                vehicleStatus = (eVehicleStatus)4;
            }
            else
            {
                vehicleStatus = GetVehicleStatus();
            }

            return vehicleStatus;
        }

        public static eVehicleStatus GetVehicleStatus()
        {
            int i = 0;
            string userInput;
            eVehicleStatus vehicleStatus = 0;

            Console.WriteLine("please choose one of the below options:");
            foreach (eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
            {
                Console.WriteLine(@"Press ({0}) to status: {1}", i, status);
                i++;
            }

            userInput = Console.ReadLine();
            while (!eVehicleStatus.TryParse(userInput, out vehicleStatus) || ((int)vehicleStatus < 0 || (int)vehicleStatus > i - 1))
            {
                Console.WriteLine(@"Wrong input. please choose number between 0 to {0}", i-1);
                userInput = Console.ReadLine();
            }

            return vehicleStatus;
        }

        public static eFuelTypes GetFuelType()
        {
            int i = 0;
            string userInput;
            eFuelTypes fuelTypeChoice = 0;

            Console.WriteLine("please choose one of the below options:");
            foreach (eFuelTypes fuelType in Enum.GetValues(typeof(eFuelTypes)))
            {
                Console.WriteLine(@"Press ({0}) to {1}: ", i, fuelType);
                i++;
            }

            userInput = Console.ReadLine();

            if(!eFuelTypes.TryParse(userInput, out fuelTypeChoice))
            {
                throw new FormatException("Format Exception - Fuel Type");
            }

            if((int)fuelTypeChoice < 0 || (int)fuelTypeChoice > i - 1)
            {
                throw new ArgumentException("Argument Exception - Fuel Type");
            }
            
            return fuelTypeChoice;
        }

        public static float GetFuelQuantityToAdd()
        {
            float fuelQuantity = 0;
            string userInput;

            Console.WriteLine("Please insert the desired fuel quantity to add to the vehicle's fuel tank");
            userInput = Console.ReadLine();
            if (!float.TryParse(userInput, out fuelQuantity))
            {
                throw new FormatException("Format Exception - Fuel Quantity");
            }

            return fuelQuantity;
        }

        public static int GetAmountOfMinutesToAdd()
        {
            int AmountOfMinutes = 0;
            string userInput;

            Console.WriteLine("Please insert the desired amount of minutes you wish to recharge the battery");
            userInput = Console.ReadLine();
            if (!int.TryParse(userInput, out AmountOfMinutes))
            {
                throw new FormatException("Format Exception - Amount Of Minutes");
            }

            return AmountOfMinutes;
        }
    }
}