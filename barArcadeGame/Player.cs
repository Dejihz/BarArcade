﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace barArcadeGame
{
    public class Player
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds;//For the collisions
        public bool isIdle = false;

        public Player()
        {
            playerSprite = new AnimatedSprite[10];
            pos = new Vector2(600, 50);
            playerBounds = new Rectangle((int)pos.X - 8/*centered at centre*/, (int)pos.Y - 8, 16, 17);
        }
        public void Load(SpriteSheet[] spriteSheets,Vector2 location)
        {
            pos = location;
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
            if (keyboardstate.IsKeyDown(Keys.D))//Move right
            {
                animation = "walkRight";
                pos.X += moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.A))//Move right
            {
                animation = "walkLeft";
                pos.X -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.W))//Move right
            {
                animation = "walkUp";
                pos.Y -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.S))//Move right
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
                spriteBatch.Draw(playerSprite[0], pos,0, new Vector2(3,3));
            else
                spriteBatch.Draw(playerSprite[1], pos,0, new Vector2(3,3));
                
            spriteBatch.End();
        }
    }
}