using SFML.Graphics;
using SFML.System;

namespace MeteoStation
{
    abstract class Sensor
    {
        protected RectangleShape body;
        protected float mainValue; 

        public Sensor()
        {
            body = new RectangleShape(new Vector2f(170, 120));
            body.FillColor = Color.White;
            body.OutlineColor = Color.Black;
            body.OutlineThickness = 2f;
        }

        public void SetPosition(float x, float y)
        {
            body.Position = new Vector2f(x, y);
        }

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(body);
        }

        public float MainValue
        {
            get => mainValue;
            set => mainValue = value;
        }
    }

    abstract class ColumnSensor : Sensor
    {
        protected RectangleShape scaleBackground; 
        protected RectangleShape valueColumn;     

        public ColumnSensor()
        {
            scaleBackground = new RectangleShape(new Vector2f(20, 80));
            scaleBackground.FillColor = Color.Black;

            valueColumn = new RectangleShape(new Vector2f(15, 40));
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            Vector2f bodyPos = body.Position;
            scaleBackground.Position = new Vector2f(bodyPos.X + 75, bodyPos.Y + 20);
            valueColumn.Position = new Vector2f(bodyPos.X + 77.5f, bodyPos.Y + 60); 

            window.Draw(scaleBackground);
            window.Draw(valueColumn);
        }
    }

    class TemperatureSensor : ColumnSensor
    {
        public TemperatureSensor()
        {
            valueColumn.FillColor = Color.Blue;
        }
    }

    class PressureSensor : ColumnSensor
    {
        public PressureSensor()
        {
            valueColumn.FillColor = Color.Red;
        }
    }

    class WindSpeedSensor : Sensor
    {
        private Text valueText;
        private Font font;

        public WindSpeedSensor()
        {
            font = new Font("arial.ttf"); 
            valueText = new Text("0", font, 20);
            valueText.FillColor = Color.Black;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            valueText.DisplayedString = mainValue.ToString("F1");

            FloatRect textBounds = valueText.GetLocalBounds();
            Vector2f bodyPos = body.Position;
            valueText.Position = new Vector2f(
                bodyPos.X + (body.Size.X - textBounds.Width) / 2,
                bodyPos.Y + (body.Size.Y - textBounds.Height) / 2
            );

            window.Draw(valueText);
        }
    }

    class WindDirectionSensor : Sensor
    {
        private RectangleShape directionArrow;

        public WindDirectionSensor()
        {
            directionArrow = new RectangleShape(new Vector2f(60, 4));
            directionArrow.FillColor = Color.Black;
            directionArrow.Origin = new Vector2f(0, 2);
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            Vector2f bodyPos = body.Position;
            directionArrow.Position = new Vector2f(
                bodyPos.X + body.Size.X / 2,
                bodyPos.Y + body.Size.Y / 2
            );

            directionArrow.Rotation = mainValue;

            window.Draw(directionArrow);
        }
    }
}