using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    class AlignmentHelper
    {
        public static Vector2 Align(Vector2 container, Vector2 content, ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    return new Vector2(container.X / 2 - content.X / 2, container.Y - content.Y);
                case ContentAlignment.BottomRight:
                    return new Vector2(container.X - content.X, container.Y - content.Y);
                case ContentAlignment.BottomLeft:
                    return new Vector2(0, container.Y - content.Y);
                case ContentAlignment.MiddleCenter:
                    return new Vector2(container.X / 2 - content.X / 2, container.Y / 2 - content.Y / 2);
                case ContentAlignment.MiddleLeft:
                    return new Vector2(0, container.Y / 2 - content.Y / 2);
                case ContentAlignment.MiddleRight:
                    return new Vector2(container.X - content.X, container.Y / 2 - content.Y / 2);
                case ContentAlignment.TopCenter:
                    return new Vector2(container.X / 2 - content.X / 2, 0);
                case ContentAlignment.TopLeft:
                    return new Vector2(0, 0);
                case ContentAlignment.TopRight:
                    return new Vector2(container.X - content.X, 0);
            }
            return Vector2.Zero;
        }
    }
}
