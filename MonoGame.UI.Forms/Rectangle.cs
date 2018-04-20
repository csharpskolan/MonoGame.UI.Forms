using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class FilledRectangle : Control
    {
        private Rectangle _rectangle;

        public FilledRectangle(int x, int y, int width, int height)
        {
            _rectangle = new Rectangle(x, y, width, height);
            BackgroundColor = Color.White;

            Location = new Vector2(x, y);
            Size = new Vector2(width, height);
        }
        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            _rectangle.Offset(offset);
            helper.DrawRectangle(_rectangle, BackgroundColor);
        }

    }
}
