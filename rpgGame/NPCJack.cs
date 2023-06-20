using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;

namespace rpgGame
{
    public class NPCJack 
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds;//For the collisions
        public bool isIdle = false;
 

        private String[] dialogue;
        private int currentDialogueIndex;

        public NPCJack()
        {
            playerSprite = new AnimatedSprite[10];

            pos = new Vector2(100, 50);
            playerBounds = new Rectangle((int)pos.X - 8/*centered at centre*/, (int)pos.Y - 8, 16, 17);

            //string[] dialogue = { "nihao", "woshishei", "zaijian", "kanxi", "shangxue" };


            this.dialogue = new String[3];
            dialogue[0] = "nihao";
            dialogue[1] = "woshishei";
            dialogue[2] = "zaijian";
            this.currentDialogueIndex = 0;
           
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

            string animation = "";
            var keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.I))//Move right
            {
                animation = "walkRight";
                pos.X += moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.J))//Move right
            {
                animation = "walkLeft";
                pos.X -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.K))//Move right
            {
                animation = "walkUp";
                pos.Y -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.L))//Move right
            {
                animation = "walkDown";
                pos.Y += moveSpeed;
                isIdle = false;
            }
            if (!isIdle)
            {
                playerSprite[1].Play(animation);
                playerSprite[1].Update(gameTime);
            }
            playerBounds.X = (int)pos.X - 8;//Apparently by default the rectangle gets centred at the player's centre when using monogame extended's draw function.
            playerBounds.Y = (int)pos.Y - 8;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Increment the dialogue index
                currentDialogueIndex++;

                // Wrap around to the beginning if all dialogue has been shown
                if (currentDialogueIndex >= dialogue.Length)
                {
                    currentDialogueIndex = 0;
                }
            }
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
            if (isIdle)
                spriteBatch.Draw(playerSprite[0], pos);
            else
                spriteBatch.Draw(playerSprite[1], pos);
            Console.WriteLine(dialogue[0]);
            string currentDialogue = dialogue[currentDialogueIndex];

            //spriteBatch.DrawString(spriteFont, currentDialogue, new Vector2(pos.X, pos.Y - 20), Color.White);

            spriteBatch.End();
        }
    }
}
