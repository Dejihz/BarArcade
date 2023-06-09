﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using barArcadeGame._Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace barArcadeGame._Models
{
    internal class DoorToArcade
    {
        public Vector2 _position;
        private readonly AnimationManager _anims = new();
        public Rectangle bounds;
        public bool touch;

        public DoorToArcade(Vector2 pos)
        {
            touch = false;
            var doorTex = Globals.Content.Load<Texture2D>("picture/door");
            _position = pos;
            bounds = new Rectangle((int)_position.X/*centered at centre*/, (int)_position.Y, 60, 100);
            _anims.AddAnimation(1, new(doorTex, 7, 2, 0.2f, 1));
            _anims.AddAnimation(2, new(doorTex, 7, 2, 2000f, 2));
        }

        public void Update()
        {
            if (!touch)
            {
                _anims.Update(2);
            }
            if (touch)
            {             
                _anims.Update(1);        
            }
        }

        public void Draw(SpriteBatch spriteBatch, Matrix matrix, Matrix transformMatrix)
        {
            // SpriteBatch spriteBatch, Matrix matrix, Matrix transformMatrix
            spriteBatch.Begin(//All of these need to be here :(
                SpriteSortMode.Deferred,
                samplerState: SamplerState.PointClamp,
                effect: null,
                blendState: null,
                rasterizerState: null,
                depthStencilState: null,
                transformMatrix: transformMatrix);/*<-This is the main thing*/

            //Globals.SpriteBatch.Begin();
            _anims.Draw(_position);
            Color rectangleColor = Color.Red; //The color of the example rectangle
            Globals.SpriteBatch.DrawRectangle(bounds, rectangleColor);
            spriteBatch.End();
        }
    }
}





