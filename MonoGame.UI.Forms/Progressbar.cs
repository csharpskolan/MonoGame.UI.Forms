using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class Progressbar : Control
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public Color BarColor { get; set; }

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                if (value > Max)
                    _value = Max;
                else if (value < Min)
                    _value = Min;
                else
                    _value = value;
            }
        }


        public Progressbar()
        {
            Max = 100;
            Min = 0;
            Value = 20;
            BarColor = new Color(70, 70, 230);
            BackgroundColor = new Color(30, 30, 50);
        }

        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            var start = Location + offset;
            var widthDone = Value / (float) Max * Size.X;
            var widthLeft = Size.X - widthDone;

            helper.DrawRectangle(new Rectangle((int)start.X, (int)start.Y, (int)widthDone, (int)Size.Y), BarColor);
            helper.DrawRectangle(new Rectangle((int)(start.X + widthDone), (int)start.Y, (int)widthLeft, (int)Size.Y), BackgroundColor);
        }
    }
}
