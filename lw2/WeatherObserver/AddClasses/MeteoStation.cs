using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace MeteoStation
{
    class MeteoStation
    {
        private RectangleShape body;
        private List<Sensor> sensors = new List<Sensor>();
        private Weather weather;

        public MeteoStation(float x, float y, Weather weatherData)
        {
            this.weather = weatherData;

            body = new RectangleShape(new Vector2f(380, 280));
            body.Position = new Vector2f(x + 10, y + 10);
            body.FillColor = Color.White;
            body.OutlineColor = Color.Black;
            body.OutlineThickness = 2f;

            var tempSensor = new ColumnSensor(-50f, 50f, Color.Red);
            var pressSensor = new ColumnSensor(0f, 1000f, Color.Green);
            var windSpeedSensor = new WindSpeedSensor();
            var windDirSensor = new WindDirectionSensor();

            sensors.Add(tempSensor);
            sensors.Add(pressSensor);
            sensors.Add(windSpeedSensor);
            sensors.Add(windDirSensor);

            PositionSensors(x, y);

            weather.TemperatureChanged += tempSensor.UpdateValue;
            weather.PressureChanged += pressSensor.UpdateValue;
            weather.WindSpeedChanged += windSpeedSensor.UpdateValue;
            weather.WindDirectionChanged += windDirSensor.UpdateValue;

            tempSensor.UpdateValue(weather.Temperature);
            pressSensor.UpdateValue(weather.Pressure);
            windSpeedSensor.UpdateValue(weather.WindSpeed);
            windDirSensor.UpdateValue(weather.WindDirection);

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

        public List<Sensor> Sensors => sensors;
    }
}