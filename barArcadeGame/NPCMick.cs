using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barArcadeGame
{
    internal class NPCMick
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds;//For the collisions
        public bool isIdle = false;

        public NPCMick()
        {
            playerSprite = new AnimatedSprite[10];
            pos = new Vector2(100, 100);
            playerBounds = new Rectangle((int)pos.X - 8/*centered at centre*/, (int)pos.Y - 8, 16, 17);
        }

        public void Load(SpriteSheet[] spriteSheets)
        {
            for (int i = 0; i < spriteSheets.Length; i++)
            {
                sheet = spriteSheets[i];
                playerSprite[i] = new AnimatedSprite(sheet);
            }
        }

        public void Update(GameTime gameTime)
        {
            isIdle = true;
            playerSprite[0].Play("idleDown");

            playerBounds.X = (int)pos.X - 8;//Apparently by default the rectangle gets centred at the player's centre when using monogame extended's draw function.
            playerBounds.Y = (int)pos.Y - 8;

            playerSprite[0].Update(gameTime);

    
        }

        public void Draw(SpriteBatch spriteBatch, Matrix matrix, Matrix transformMatrix)
        {
            spriteBatch.Begin(//All of these need to be here :(
               SpriteSortMode.Deferred,
               samplerState: SamplerState.PointClamp,
               effect: null,
               blendState: null,
               rasterizerState: null,
               depthStencilState: null,
               transformMatrix: transformMatrix);/*<-This is the main thing*/
           //if (isIdle)
                spriteBatch.Draw(playerSprite[0], pos);
            //spriteBatch.DrawString(spriteFont, currentDialogue, new Vector2(pos.X, pos.Y - 20), Color.White);
            spriteBatch.End();
        }
    }
}
