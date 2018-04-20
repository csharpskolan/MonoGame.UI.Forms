using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class Form : Control, IControls
    {
        public bool IsMovable { get; set; }
        public string Title { get; set; }
        public List<Control> Controls { get; private set; }

        public string WinTopTexture { get; set; }
        public string WinTopLeftTexture { get; set; }
        public string WinTopRightTexture { get; set; }
        public string WinLeftTexture { get; set; }
        public string WinRightTexture { get; set; }
        public string WinBottomTexture { get; set; }
        public string WinBottomLeftTexture { get; set; }
        public string WinBottomRightTexture { get; set; }

        public Form()
        {
            BackgroundColor = Color.Gray;
            Controls = new List<Control>();
        }

        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            if (!IsVisible)
                return;

            var topLeftOffset = Vector2.Zero;
            var topRightOffset = Vector2.Zero;
            var bottomLeftOffset = Vector2.Zero;
            var bottomRightOffset = Vector2.Zero;

            if (!string.IsNullOrEmpty(WinTopLeftTexture))
                topLeftOffset = helper.DrawTextureWithOffset(Location, WinTopLeftTexture);
            if (!string.IsNullOrEmpty(WinTopRightTexture))
                topRightOffset = helper.DrawTextureWithOffset(Location + new Vector2(Size.X, 0), WinTopRightTexture, DrawHelper.AlignOffset.TopRight);
            if (!string.IsNullOrEmpty(WinBottomLeftTexture))
                bottomLeftOffset = helper.DrawTextureWithOffset(Location + new Vector2(0, Size.Y), WinBottomLeftTexture, DrawHelper.AlignOffset.BottomLeft);
            if (!string.IsNullOrEmpty(WinBottomRightTexture))
                bottomRightOffset = helper.DrawTextureWithOffset(Location + new Vector2(Size.X, Size.Y), WinBottomRightTexture, DrawHelper.AlignOffset.BottomRight);

            //Top
            if(!string.IsNullOrEmpty(WinTopTexture))
                helper.DrawTextureRepeat(Location + new Vector2(topLeftOffset.X, 0), 
                    Location + new Vector2(Size.X - topRightOffset.X, topLeftOffset.Y), WinTopTexture);
            //Bottom
            if (!string.IsNullOrEmpty(WinBottomTexture))
                helper.DrawTextureRepeat(Location + new Vector2(topLeftOffset.X, Size.Y - bottomLeftOffset.Y),
                    Location + new Vector2(Size.X - topRightOffset.X, Size.Y), WinBottomTexture);
            //Left
            if (!string.IsNullOrEmpty(WinLeftTexture))
                helper.DrawTextureRepeat(Location + new Vector2(0, topLeftOffset.Y),
                    Location + new Vector2(topLeftOffset.X, Size.Y - bottomLeftOffset.Y), WinLeftTexture);
            //Right
            if (!string.IsNullOrEmpty(WinRightTexture))
                helper.DrawTextureRepeat(Location + new Vector2(Size.X - topRightOffset.X, topLeftOffset.Y),
                    Location + new Vector2(Size.X, Size.Y - bottomLeftOffset.Y), WinRightTexture);


            //Fill
            var rectangle = new Rectangle((int)(Location.X + topLeftOffset.X), (int)(Location.Y + topLeftOffset.Y), 
                (int)(Size.X - topLeftOffset.X - topRightOffset.X), (int)(Size.Y - topLeftOffset.Y - bottomLeftOffset.Y));
            helper.DrawRectangle(rectangle, BackgroundColor);

            helper.DrawString(this, Location + new Vector2(10, 6), Title, Color.White);

            foreach (var control in Controls)
            {
                if(control.IsVisible)
                    control.Draw(helper, Location);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            foreach (var control in Controls)
            {
                control.Update(gameTime);
            }
        }

        Control IControls.FindControlAt(Point position)
        {
            var point = position - Location.ToPoint();
            var control = Controls.LastOrDefault(c => c.Contains(point));
            return control ?? this;
        }

        internal override void LoadContent(DrawHelper helper)
        {
            base.LoadContent(helper);

            string[] assets =
            {
                WinTopTexture, WinTopLeftTexture, WinTopRightTexture, WinLeftTexture,
                WinRightTexture, WinBottomTexture, WinBottomLeftTexture, WinBottomRightTexture
            };

            foreach (var asset in assets)
            {
                if (!string.IsNullOrEmpty(asset))
                    helper.LoadTexture(asset);
            }

        }
    }
}
