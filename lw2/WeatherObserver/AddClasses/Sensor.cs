using SFML.Graphics;
using SFML.System;
using System;

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

        public virtual void UpdateValue(float value)
        {
            mainValue = value;
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

    class ColumnSensor : Sensor
    {
        private RectangleShape scaleBackground;
        private RectangleShape valueColumn;
        private float minValue;
        private float maxValue;
        private Color columnColor;

        private const float SCALE_HEIGHT = 80f;

        public ColumnSensor(float minVal = -10f, float maxVal = 10f, Color color = default)
        {
            minValue = minVal;
            maxValue = maxVal;
            columnColor = color == default ? Color.Blue : color;

            scaleBackground = new RectangleShape(new Vector2f(20, SCALE_HEIGHT));
            scaleBackground.FillColor = new Color(200, 200, 200);

            valueColumn = new RectangleShape(new Vector2f(15, 0));
            valueColumn.FillColor = columnColor;
        }

        public override void UpdateValue(float value)
        {
            mainValue = value;
            UpdateColumnVisual();
        }

        private void UpdateColumnVisual()
        {
            float normalizedValue = (mainValue - minValue) / (maxValue - minValue);
            float columnHeight = normalizedValue * SCALE_HEIGHT;

            columnHeight = Math.Max(0, Math.Min(SCALE_HEIGHT, columnHeight));
            valueColumn.Size = new Vector2f(15, columnHeight);

            Vector2f bodyPos = body.Position;
            if (bodyPos.X != 0 || bodyPos.Y != 0) 
            {
                valueColumn.Position = new Vector2f(
                    bodyPos.X + 77.5f,
                    bodyPos.Y + 20 + (SCALE_HEIGHT - columnHeight)
                );
            }
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            Vector2f bodyPos = body.Position;
            scaleBackground.Position = new Vector2f(bodyPos.X + 75, bodyPos.Y + 20);

            window.Draw(scaleBackground);
            window.Draw(valueColumn);
        }
    }

    class WindSpeedSensor : Sensor
    {
        private Text valueText;
        private Font font;

        public WindSpeedSensor()
        {
            font = new Font("arial.ttf");
            valueText = new Text("0.0", font, 20);
            valueText.FillColor = Color.Black;
        }

        public override void UpdateValue(float value)
        {
            mainValue = value;
            valueText.DisplayedString = value.ToString("F1");
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

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
        private ConvexShape arrowHead;
        private Text directionText;
        private Font font;

        public WindDirectionSensor()
        {
            directionArrow = new RectangleShape(new Vector2f(30, 3));
            directionArrow.FillColor = Color.Black;
            directionArrow.Origin = new Vector2f(0, 1.5f);

            arrowHead = new ConvexShape(3);
            arrowHead.SetPoint(0, new Vector2f(0, 0));
            arrowHead.SetPoint(1, new Vector2f(10, -5));
            arrowHead.SetPoint(2, new Vector2f(10, 5));
            arrowHead.FillColor = Color.Black;
            arrowHead.Origin = new Vector2f(0, 0);

            font = new Font("arial.ttf");
            directionText = new Text("0°", font, 16);
            directionText.FillColor = Color.Black;
        }

        public override void UpdateValue(float value)
        {
            mainValue = value;
            directionText.DisplayedString = $"{value:F0}°";
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            Vector2f bodyPos = body.Position;
            Vector2f center = new Vector2f(
                bodyPos.X + body.Size.X / 2,
                bodyPos.Y + body.Size.Y / 2
            );

            float rotationAngle = mainValue - 90;

            directionArrow.Position = center;
            directionArrow.Rotation = rotationAngle;

            float arrowLength = 30;
            float triangleOffset = 5;

            float angleRad = rotationAngle * (float)Math.PI / 180f;
            Vector2f arrowEnd = new Vector2f(
                center.X + (arrowLength + triangleOffset) * (float)Math.Cos(angleRad),
                center.Y + (arrowLength + triangleOffset) * (float)Math.Sin(angleRad)
            );

            arrowHead.Position = arrowEnd;
            arrowHead.Rotation = rotationAngle + 180;

            FloatRect textBounds = directionText.GetLocalBounds();
            directionText.Position = new Vector2f(
                bodyPos.X + (body.Size.X - textBounds.Width) / 2,
                bodyPos.Y + body.Size.Y - 25
            );

            window.Draw(directionArrow);
            window.Draw(arrowHead);
            window.Draw(directionText);
        }
    }
}