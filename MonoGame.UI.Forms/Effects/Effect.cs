using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms.Effects
{
    public abstract class Effect
    {
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        protected Effect()
        {
            Zoom = 1.0f;
            Rotation = 0;   
        }

        public abstract void Reset();

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
