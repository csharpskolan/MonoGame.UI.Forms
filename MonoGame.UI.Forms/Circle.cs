using System;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class Circle : Control
    {
        public float Radius { get; set; }

        public Vector2 Center
        {
            get => new Vector2(Location.X + Radius, Location.Y + Radius);
            set => Location = new Vector2(value.X - Radius, value.Y - Radius);
        }

        public Circle(Vector2 center, float radius)
        {
            Radius = radius;
            Center = center;
            BackgroundColor = Color.White;
            Size = new Vector2(radius * 2, radius * 2);
        }

        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            helper.DrawCircle(Center + offset, Radius, BackgroundColor);
        }

        public override bool Contains(Point point)
        {
            var delta = point.ToVector2() - Center;
            return (delta.LengthSquared() < (float)(Radius * Radius));
        }
    }
}
