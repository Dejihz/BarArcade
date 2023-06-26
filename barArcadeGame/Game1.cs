using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using barArcadeGame._Models;
using TiledSharp;
using barArcadeGame._Managers;
using System.Collections;
using MonoGame.Extended.Timers;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace barArcadeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Ninja ninja;
        private Jack jack;
        private TmxMap map;
        private TileMapManager mapManager1;
        private TileMapManager mapManager;
        private List<Rectangle> collisionObjects;
        private List<Rectangle> collisionDoor;
        private Matrix matrix;
        private Matrix _translation;
        private GameManager _gameManager;
        private string currentScene;
        private DoorToArcade door;
        private DoorToArcade doorToBar;
        private ArcadeMachine arcadeMachine;

        private float _timer;
        private const float DelayInSeconds = 1.0f;

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
            currentScene = "bar";
            player = new Player();
            
            Window.Title = "Bararcade Game";
            Globals.Bounds = new(1000, 1000);
            _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
            _graphics.ApplyChanges();
            var WindowSize = new System.Numerics.Vector2(Width, Height);
            var mapSize = new System.Numerics.Vector2(500, 500);//Our tile map size
            matrix = Matrix.CreateScale(new System.Numerics.Vector3(WindowSize / mapSize, 1));
            Globals.Matrix = matrix;         
            base.Initialize();
        }

        private void LoadSecondScene()
        {
            currentScene = "arcade";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.Content = Content;
            map = new TmxMap("Content/arcade/arcadeRoom.tmx");
         
            var tileset = Content.Load<Texture2D>("arcade/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;

            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);

            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Objects"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playerSheetIdle.sf",new JsonContentLoader()),
                                    Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playerSheetWalk.sf",new JsonContentLoader())};
            player.Load(sheets, new Vector2(100, 200));
            doorToBar = new DoorToArcade(new Vector2(230,100));
            var arcadeTexture = Globals.Content.Load<Texture2D>("picture/arcadeMachine");
            arcadeMachine = new ArcadeMachine(arcadeTexture,new Vector2(100, 60));
            ninja = new Ninja();
            _gameManager = new();
        }

        protected override void LoadContent()
        {
            currentScene = "bar";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.Content = Content;
            map = new TmxMap("Content/barMap/mapBar.tmx");
            var tileset = Content.Load<Texture2D>("barMap/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);
       
            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Objects"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            collisionDoor = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["door"].Objects)
            {
                collisionDoor.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playerSheetIdle.sf",new JsonContentLoader()),
                                    Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playerSheetWalk.sf",new JsonContentLoader())};
  
            player.Load(sheets, new Vector2(600,50));
           
            jack = new Jack();
            door = new DoorToArcade(new Vector2(0, 285));
            _gameManager = new();
        }

        public void updateBarScene(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            player.Update(gameTime);
            jack.Update();
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(player.playerBounds))
                {
                    player.pos = initpos;               
                    player.isIdle = true;
                    SoundManager.PlayCollideFx();
                }
            }

            //foreach (var rect in collisionDoor)
            //{
            //    if (rect.Intersects(player.playerBounds))
            //    {                
            //        LoadSecondScene();
            //    }
            //}
            
            if (player.playerBounds.Intersects(door.bounds))
            {
                door.touch = true;               
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer >= DelayInSeconds)
                {
                    _timer = 0;
                    SoundManager.PlayDoorOpenFX();
                    LoadSecondScene(); 
                }
            }
            else
            {
                door.touch = false;
            }

            if (jack.playerBounds.Intersects(player.playerBounds))
            {
                jack.stop();             
                _gameManager.runJackDialogue();
            } 
            else
            {
                jack.walk();
                _gameManager.hideJackDialogue();
            }         

            _gameManager.Update();
            door.Update();
            calculateTransaction();
            Globals.Update(gameTime);
        }

        public void updateArcadeScene(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            player.Update(gameTime);

            ninja.Update();
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(player.playerBounds))
                {
                    player.pos = initpos;
                    player.isIdle = true;
                }
            }

            if (player.playerBounds.Intersects(doorToBar.bounds))
            {
                door.touch = true;
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer >= DelayInSeconds)
                {
                    _timer = 0;
                    SoundManager.PlayDoorOpenFX();
                    LoadContent(); 
                }
            }
            else
            {
                door.touch = false;
            }

            if (ninja.playerBounds.Intersects(player.playerBounds))
            {
                ninja.stop();
                _gameManager.runMickDialogue();
            }
            else
            {
                ninja.walk();
                _gameManager.hideMickDialogue();
            }

            if (arcadeMachine._rectangle.Intersects(player.playerBounds))
            {
                //currentScene = "close Scenes";
                runArcadeGame();      
            }

            _gameManager.Update();
            doorToBar.Update();
            arcadeMachine.Update();
            calculateTransaction();
            Globals.Update(gameTime);
        }

        public void runArcadeGame()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if(currentScene == "bar")
            {
                updateBarScene(gameTime);
            }
            else if(currentScene == "arcade")
            {
                updateArcadeScene(gameTime);
            }
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentScene == "bar")
            {
                // TODO: Add your drawing code here       
                mapManager.Draw(matrix, _translation);
                door.Draw(_spriteBatch, matrix, transformMatrix: _translation);
                player.Draw(_spriteBatch, matrix, transformMatrix: _translation);
                jack.Draw(_spriteBatch, matrix, transformMatrix: _translation);                      
                _gameManager.Draw();
            }
            else if(currentScene == "arcade")
            {
                mapManager.Draw(matrix, _translation);
                doorToBar.Draw(_spriteBatch, matrix, transformMatrix: _translation);
                player.Draw(_spriteBatch, matrix, transformMatrix: _translation);
                ninja.Draw(_spriteBatch, matrix, transformMatrix: _translation);       
                arcadeMachine.Draw(_spriteBatch, matrix, transformMatrix: _translation);
                _gameManager.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
