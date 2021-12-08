namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName = null;
        private float m_MaxAirPressure = 0;
        private float m_CurrentAirPressure = 0;

        public Wheel(string i_NameOfWheelManufacturer, float i_CurrentWheelPressure, float i_MaxWheelPressure)
        {
            m_ManufacturerName = i_NameOfWheelManufacturer;
            m_CurrentAirPressure = i_CurrentWheelPressure;
            m_MaxAirPressure = i_MaxWheelPressure;
        }

        public void AddAirToWheel(float i_AmountOfAirToAdd)
        {
            m_CurrentAirPressure += i_AmountOfAirToAdd;
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }
    }
}