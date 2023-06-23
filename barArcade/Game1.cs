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

namespace barArcade
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private NPCJack jack;
        private TmxMap map;
        private TileMapManager mapManager;
        private List<Rectangle> collisionObjects;
        private List<Rectangle> collisionDoor;
        private Matrix matrix;
        private Matrix _translation;

        private SpriteFont spriteFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //spriteFont = Content.Load<SpriteFont>("font");
        }

        private void calculateTransaction()
        {
            var dx = (_graphics.PreferredBackBufferWidth / 2) - player.pos.X;
            var dy = (_graphics.PreferredBackBufferHeight / 2) - player.pos.Y;
            _translation = Matrix.CreateTranslation(dx, dy, 0f);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            jack = new NPCJack();
            _graphics.PreferredBackBufferWidth =  200 * 2;//Making the window size twice our tilemap size
            _graphics.PreferredBackBufferHeight = 200 * 2;
            _graphics.ApplyChanges();
            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
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
            //var tileWidth = 64;
            var tileHeight = map.Tilesets[0].TileHeight;
            //var tileHeight = 64;

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
            map = new TmxMap("Content/testTile/testTile.tmx");
            var tileset = Content.Load<Texture2D>("testTile/" + map.Tilesets[0].Name.ToString());
            Console.WriteLine(map.Tilesets[0].Name.ToString());
           // Console.WriteLine(map.Tilesets[1].Name.ToString());
            //Console.WriteLine(map.Tilesets[2].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            //var tileWidth = 64;
            var tileHeight = map.Tilesets[0].TileHeight;
            //var tileHeight = 64;

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
            jack.Load(sheets);
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
            // TODO: Add your update logic
            calculateTransaction();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            mapManager.Draw(matrix,_translation);
            player.Draw(_spriteBatch, matrix,transformMatrix: _translation);
           
            _spriteBatch.DrawString(spriteFont, "Hello, World!", new System.Numerics.Vector2(100, 100), Color.White);
           
            jack.Draw(_spriteBatch, matrix, transformMatrix: _translation);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}