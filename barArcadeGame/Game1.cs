using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Numerics;
using TiledSharp;
using System.Collections.Generic;
using GeonBit.UI.Entities;
using GeonBit.UI;
using System.Runtime.ConstrainedExecution;
using MonoGame.UI.Forms;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace barArcadeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private NPCJack jack;
        private NPCMick mick;
        private TmxMap map;
        private TileMapManager mapManager;
        private List<Rectangle> collisionObjects;
        private List<Rectangle> collisionDoor;
        private Matrix matrix;
        private Matrix _translation;
        private GameManager _gameManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void calculateTransaction()
        {
            var dx = (_graphics.PreferredBackBufferWidth / 2) - player.pos.X;
            var dy = (_graphics.PreferredBackBufferHeight / 2) - player.pos.Y;
            _translation = Matrix.CreateTranslation(dx, dy, 0f);

        }

        protected override void Initialize()
        {
            Globals.Game = this;
     
            player = new Player();
            jack = new NPCJack();
            mick = new NPCMick();

            _graphics.PreferredBackBufferWidth = 200 * 2;//Making the window size twice our tilemap size
            _graphics.PreferredBackBufferHeight = 200 * 2;
            _graphics.ApplyChanges();
            Window.Title = "Bararcade Game";


            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
            Globals.Bounds = new(Width, Height);
            var WindowSize = new System.Numerics.Vector2(Width, Height);
            var mapSize = new System.Numerics.Vector2(400, 400);//Our tile map size
            matrix = Matrix.CreateScale(new System.Numerics.Vector3(WindowSize / mapSize, 1));
            
            base.Initialize();
        }

        private void LoadSecondScene()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new TmxMap("Content/Spaceship 3/Spaceship 3.tmx");
            var tileset = Content.Load<Texture2D>("Spaceship 3/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;

            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);

            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Rooms"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playerSheetIdle.sf",new JsonContentLoader()),
                                    Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playerSheetWalk.sf",new JsonContentLoader())};
            player.Load(sheets);
            jack.Load(sheets);
            // TODO: use this.Content to load your game content here
        }

        protected override void LoadContent()
        {
           
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.Content = Content;
            map = new TmxMap("Content/testTile/testTile.tmx");
            var tileset = Content.Load<Texture2D>("testTile/" + map.Tilesets[0].Name.ToString());
            Console.WriteLine(map.Tilesets[0].Name.ToString());
            // Console.WriteLine(map.Tilesets[1].Name.ToString());
            //Console.WriteLine(map.Tilesets[2].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;

            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);

            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Object Layer 1"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            collisionDoor = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Door"].Objects)
            {
                collisionDoor.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playerSheetIdle.sf",new JsonContentLoader()),
                                    Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playerSheetWalk.sf",new JsonContentLoader())};
            player.Load(sheets);
            //loadPanel();
            jack.Load(sheets);
            mick.Load(sheets);
            _gameManager = new();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            player.Update(gameTime);
            jack.Update(gameTime);
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(player.playerBounds))
                {
                    player.pos = initpos;
                    player.isIdle = true;
                }
            }

            foreach (var rect in collisionDoor)
            {
                if (rect.Intersects(player.playerBounds))
                {
                    //player.pos = initpos;
                    //player.isIdle = true;
                    LoadSecondScene();
                    //Exit();
                }
            }

            if (jack.playerBounds.Intersects(player.playerBounds))
            {
                _gameManager.runDialogue();
            }
            else
            {
                _gameManager.hideDialogue();
            }

            //else if (mick.playerBounds.Intersects(player.playerBounds))
            //{
            //    _gameManager.runDialogue();
            //}
            //else
            //{
            //    _gameManager.hideDialogue();
            //}

            // TODO: Add your update logic
            _gameManager.Update();
            calculateTransaction();
            Globals.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           // _spriteBatch.Begin();
            // TODO: Add your drawing code here
            mapManager.Draw(matrix, _translation);
            player.Draw(_spriteBatch, matrix, transformMatrix: _translation);
            _spriteBatch.Begin();
            _gameManager.Draw();
            _spriteBatch.End();

            // _spriteBatch.DrawString(spriteFont, "Hello, World!", new System.Numerics.Vector2(100, 100), Color.White);
            jack.Draw(_spriteBatch, matrix, transformMatrix: _translation);
            mick.Draw(_spriteBatch, matrix, transformMatrix: _translation);
            // _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}