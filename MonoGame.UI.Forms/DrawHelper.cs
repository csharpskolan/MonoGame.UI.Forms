using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.UI.Forms
{
    internal class DrawHelper
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _manager;
        private Dictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> _fontCache = new Dictionary<string, SpriteFont>();

        private Texture2D whiteTexture;

        public DrawHelper(SpriteBatch spriteBatch, ContentManager manager, GraphicsDevice device)
        {
            _spriteBatch = spriteBatch;
            _manager = manager;

            var size = 100;
            whiteTexture = new Texture2D(device, size, size);
            Color[] data = new Color[size * size];

            for (int i = 0; i < size*size; i++)
                data[i] = Color.White;

            whiteTexture.SetData(data);
        }

        public void ReloadResources(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                control.LoadContent(this);
            }
        }

        public void DrawString(Control control, Vector2 position, string text, Color color)
        {
            var font = LoadFont(control.FontName);
            _spriteBatch.DrawString(font, text, position, color, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            _spriteBatch.Draw(whiteTexture, rectangle, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0);
        }

        public Vector2 DrawTextureWithOffset(Vector2 position, string asset, AlignOffset align = AlignOffset.TopLeft)
        {
            return DrawTextureWithOffset(position, asset, Color.White, align);
        }

        public Vector2 DrawTextureWithOffset(Vector2 position, string asset, Color blend, AlignOffset align = AlignOffset.TopLeft)
        {
            var texture = LoadTexture(asset);
            var offset = Vector2.Zero;

            switch (align)
            {
                case AlignOffset.TopRight: offset = new Vector2(-texture.Width, 0); break;
                case AlignOffset.BottomLeft: offset = new Vector2(0, -texture.Height); break;
                case AlignOffset.BottomRight: offset = new Vector2(-texture.Width, -texture.Height); break;
            }

            _spriteBatch.Draw(texture, position + offset, blend);
            return new Vector2(texture.Width, texture.Height);
        }

        public void DrawTextureRepeat(Vector2 start, Vector2 stop, string asset)
        {
            DrawTextureRepeat(start, stop, asset, Color.White);
        }

        public void DrawTextureRepeat(Vector2 start, Vector2 stop, string asset, Color blend)
        {
            var texture = LoadTexture(asset);

            int width = (int) (stop.X - start.X);
            int height = (int) (stop.Y - start.Y);

            while (height > 0)
            {
                var startX = start;
                while (width > 0)
                {
                    var srcRect = new Rectangle(0, 0,
                        width >= texture.Width ? texture.Width : width,
                        height >= texture.Height ? texture.Height : height);
                    _spriteBatch.Draw(texture, start, srcRect, blend);
                    width -= texture.Width;
                    start += new Vector2(texture.Width, 0);
                }
                start = startX;
                width = (int)(stop.X - start.X);

                height -= texture.Height;
                start += new Vector2(0, texture.Height);
            }
        }

        public Vector2 MeasureString(string asset, string text)
        {
            var font = LoadFont(asset);
            return font.MeasureString(text);
        }

        public SpriteFont LoadFont(string asset)
        {
            if (_fontCache.ContainsKey(asset))
                return _fontCache[asset];

            var font = _manager.Load<SpriteFont>(asset);
            _fontCache.Add(asset, font);
            return font;
        }

        public Texture2D LoadTexture(string asset)
        {
            if (_textureCache.ContainsKey(asset))
                return _textureCache[asset];

            var texture = _manager.Load<Texture2D>(asset);
            _textureCache.Add(asset, texture);
            return texture;
        }

        internal enum AlignOffset
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }
    }
}
