using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace MeteoStation
{
    class MeteoStation
    {
        private RectangleShape body;
        private List<Sensor> sensors = new List<Sensor>();

        private float temperature;
        private float pressure;
        private float windSpeed;
        private float windDirection;

        public MeteoStation(float x, float y)
        {
            body = new RectangleShape(new Vector2f(380, 280));
            body.Position = new Vector2f(x + 10, y + 10);
            body.FillColor = Color.White;
            body.OutlineColor = Color.Black;
            body.OutlineThickness = 2f;

            sensors.Add(new TemperatureSensor());
            sensors.Add(new PressureSensor());
            sensors.Add(new WindSpeedSensor());
            sensors.Add(new WindDirectionSensor());

            PositionSensors(x, y);
        }

        private void PositionSensors(float baseX, float baseY)
        {
            for (int i = 0; i < sensors.Count && i < 4; i++)
            {
                int row = i / 2;
                int col = i % 2;
                float x = baseX + 20 + col * 190;
                float y = baseY + 20 + row * 140;
                sensors[i].SetPosition(x, y);
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(body);
            foreach (var sensor in sensors)
            {
                sensor.Draw(window);
            }
        }

        public float Temperature
        {
            get => temperature;
            set => temperature = value;
        }

        public float Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        public float WindSpeed
        {
            get => windSpeed;
            set => windSpeed = value;
        }

        public float WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }

        public List<Sensor> Sensors => sensors;
    }
}