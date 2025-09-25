using System;
using System.Collections.Generic;

namespace MeteoStation
{


    public class Weather
    {
        private const float MIN_TEMP = -100f;
        private const float MAX_TEMP = 100f;
        private const float MIN_PRESSURE = 0f;
        private const float MAX_PRESSURE = 2000f;
        private const float MIN_WIND_SPEED = 0f;
        private const float MAX_WIND_SPEED = 100f;

        private float _temperature;
        private float _pressure;
        private float _windSpeed;
        private float _windDirection;

        public event Action<float> TemperatureChanged;
        public event Action<float> PressureChanged;
        public event Action<float> WindSpeedChanged;
        public event Action<float> WindDirectionChanged;

        public Weather()
        {
            _temperature = 0f;
            _pressure = 760f;
            _windSpeed = 0f;
            _windDirection = 0f;
        }

        public float Temperature
        {
            get => _temperature;
            set
            {
                if (value < MIN_TEMP || value > MAX_TEMP)
                    throw new ArgumentOutOfRangeException($"Температура должна быть в диапазоне {MIN_TEMP}...{MAX_TEMP}");

                _temperature = value;
                TemperatureChanged?.Invoke(value);
            }
        }

        public float Pressure
        {
            get => _pressure;
            set
            {
                if (value < MIN_PRESSURE || value > MAX_PRESSURE)
                    throw new ArgumentOutOfRangeException($"Давление должно быть в диапазоне {MIN_PRESSURE}...{MAX_PRESSURE}");

                _pressure = value;
                PressureChanged?.Invoke(value);
            }
        }

        public float WindSpeed
        {
            get => _windSpeed;
            set
            {
                if (value < MIN_WIND_SPEED || value > MAX_WIND_SPEED)
                    throw new ArgumentOutOfRangeException($"Скорость ветра должна быть в диапазоне {MIN_WIND_SPEED}...{MAX_WIND_SPEED}");

                _windSpeed = value;
                WindSpeedChanged?.Invoke(value);
            }
        }

        public float WindDirection
        {
            get => _windDirection;
            set
            {
                _windDirection = value % 360;
                if (_windDirection < 0) _windDirection += 360;

                WindDirectionChanged?.Invoke(_windDirection);
            }
        }

        public override string ToString()
        {
            return $" Температура: {_temperature}\n Давление: {_pressure} мм рт.ст.\n Ветер: {_windSpeed} м/с\n Направление {_windDirection}°";
        }
    }
}