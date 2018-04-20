using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class Button : Control
    {
        public string Text { get; set; }
        public ContentAlignment TextAlign { get; set; }
        public Color TextColor { get; set; }
        
        public string BtnLeftTexture { get; set; }
        public string BtnRightTexture { get; set; }
        public string BtnMiddleTexture { get; set; }

        public Button()
        {
            BackgroundColor = Color.Aquamarine;
            TextColor = Color.White;
            TextAlign = ContentAlignment.MiddleCenter;
        }

        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            var rectangle = HitBox;
            rectangle.Offset(offset);
            var start = Location + offset;

            var topLeftOffset = Vector2.Zero;
            var topRightOffset = Vector2.Zero;
            var hover = IsHovering ? new Color(255, 255, 255, 240) : Color.White;
            var blend = IsPressed ? new Color(205, 205, 205, 230) : hover;
            
            if (!string.IsNullOrEmpty(BtnLeftTexture))
                topLeftOffset = helper.DrawTextureWithOffset(start, BtnLeftTexture, blend);
            if (!string.IsNullOrEmpty(BtnRightTexture))
                topRightOffset = helper.DrawTextureWithOffset(start + new Vector2(Size.X, 0), BtnRightTexture, blend, DrawHelper.AlignOffset.TopRight);

            if (string.IsNullOrEmpty(BtnMiddleTexture))
                helper.DrawRectangle(rectangle, BackgroundColor.Blend(blend));
            else
                helper.DrawTextureRepeat(start + new Vector2(topLeftOffset.X, 0),
                    start + new Vector2(Size.X - topRightOffset.X, topLeftOffset.Y), BtnMiddleTexture, blend);

            var txtSize = helper.MeasureString(FontName, Text);
            helper.DrawString(this, Location + AlignmentHelper.Align(Size, txtSize, TextAlign) + offset, Text, TextColor, Zoom);
        }

        internal override void LoadContent(DrawHelper helper)
        {
            base.LoadContent(helper);

            string[] assets =
            {
                BtnLeftTexture, BtnMiddleTexture, BtnRightTexture
            };

            foreach (var asset in assets)
            {
                if (!string.IsNullOrEmpty(asset))
                    helper.LoadTexture(asset);
            }
        }
    }
}
