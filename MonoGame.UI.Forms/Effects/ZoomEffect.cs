using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms.Effects
{
    public class ZoomEffect : Effect
    {
        public float ZoomTo { get; set; }
        public int Duration { get; set; }


        public override void Reset()
        {
            Zoom = 1.0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float delta = (ZoomTo - 1.0f) / (float)Duration;

            if (Zoom < ZoomTo)
            {
                Zoom += delta;
            }
        }
    }
}
