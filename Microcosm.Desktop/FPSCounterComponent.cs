using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Desktop
{
    public class FPSCounterComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;


        public FPSCounterComponent(Game game, SpriteBatch Batch, SpriteFont Font)
            : base(game)
        {
            spriteFont = Font;
            spriteBatch = Batch;
        }


        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            var memory = GC.GetTotalMemory(false).Bytes();
            string fps = string.Format("fps: {0} mem: {1}", frameRate, memory);

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fps, new Vector2(1, 1), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
