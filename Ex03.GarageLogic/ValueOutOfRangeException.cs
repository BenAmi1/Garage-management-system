using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public string RangeErrorToUser()
        {
            return string.Format("Input has to be between {0} to {1}", r_MinValue, r_MaxValue);
        }

        public string RefuelingBeyondCapacity()
        {
            return string.Format("Too much fuel. The max amount of fuel you can add is: {0}.", r_MaxValue);
        }

        public string ChargeBeyondCapacity()
        {
            // *60 to transfer from hours to minutes
            return string.Format("Too much recharging time. The max amount of minutes you can recharge is: {0}.", r_MaxValue * 60);
        }
    }
}