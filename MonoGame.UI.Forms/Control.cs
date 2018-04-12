using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public abstract class Control
    {
        public bool Enabled { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Size { get; set; }
        public Color BackgroundColor { get; set; }
        public string FontName { get; set; }
        public Rectangle HitBox => new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
        public int ZIndex { get; set; }

        public event EventHandler Clicked;
        public event EventHandler MouseDown;
        public event EventHandler MouseUp;
        public event EventHandler MouseLeave;
        public event EventHandler MouseEnter;

        protected bool IsPressed;
        protected bool IsHovering;
    
        protected Control()
        {
            FontName = "defaultFont";
            IsVisible = true;
        }

        internal abstract void Draw(DrawHelper helper, Vector2 offset);

        internal virtual void Draw(DrawHelper helper)
        {
            if(IsVisible)
                Draw(helper, Vector2.Zero);
        }

        internal virtual void LoadContent(DrawHelper helper)
        {
            if (!string.IsNullOrEmpty(FontName))
                helper.LoadFont(FontName);
        }

        internal virtual void OnMouseDown()
        {
            IsPressed = true;
            MouseDown?.Invoke(this, new EventArgs());
        }

        internal virtual void OnMouseUp()
        {
            IsPressed = false;
            MouseUp?.Invoke(this, new EventArgs());
        }

        internal virtual void OnMouseEnter()
        {
            IsHovering = true;
            MouseEnter?.Invoke(this, new EventArgs());
        }

        internal virtual void OnMouseLeave()
        {
            IsHovering = false;
            MouseLeave?.Invoke(this, new EventArgs());
        }

        internal virtual void OnClicked()
        {
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}
