using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace barArcadeGame
{
    public class DialogueManager
    {
        public static Button NextBtn { get; private set; }
        public static Button ExitBtn { get; private set; }
        private static Texture2D _textureBox;
        private static Rectangle _rectangle;
        //public static List<Label> _data;
        public static List<Label> _data = new List<Label>();
        public static Label _displayText;
        public static int _count;
       
        
        public static void Init()
        {
            NextBtn = new(Globals.Content.Load<Texture2D>("picture/next"), new(Globals.Bounds.X - 20, 60));
            NextBtn.setScale(new(1, 1));
            NextBtn.OnClick += ClickNext;
            ExitBtn = new(Globals.Content.Load<Texture2D>("picture/exit"), new(Globals.Bounds.X - 20, 20));
            ExitBtn.setScale(new(1, 1));
            ExitBtn.OnClick += ClickExit;
           
            _count = 0;
         
            var font = Globals.Content.Load<SpriteFont>("Font/defaultFont");
            addText(font);
            _displayText = new(font, new(Globals.Bounds.X / 2, 30));
            _displayText.SetText(_data.ElementAt(0).Text);

          

            _textureBox = new Texture2D(Globals.SpriteBatch.GraphicsDevice, 1, 1);
            _textureBox.SetData(new Color[] { new(0, 80, 30) });
            _rectangle = new(0, 0, Globals.Bounds.X, 80);
        }

        public static void HideAllSpritesAndTextures()
        {
            _displayText.SetText("");
            NextBtn.setScale(new(0, 0));
            ExitBtn.setScale(new(0, 0));
            _rectangle = new(0, 0, 0, 0);
        }

        public static void ClickNext(object sender, EventArgs e)
        {
            if (_count < _data.Count -1)
            {
                _count++;
            }
           
            _displayText.SetText(_data.ElementAt(_count).Text);
        }

        //public static void removeText(SpriteFont font)
        //{
        //    Label label1 = new(font, new(Globals.Bounds.X / 2, 30));
        //    label1.SetText("");
        //    _data.Add(label1);
        //}

        public static void addText(SpriteFont font)
        {
            Label label1 = new(font, new(Globals.Bounds.X / 2, 30));
            label1.SetText("jkasdhkja");
            _data.Add(label1);

            Label label2 = new(font, new(Globals.Bounds.X / 2, 70));
            label2.SetText("jkasdhkja222");
            _data.Add(label2);

            Label label3 = new(font, new(Globals.Bounds.X / 2, 70));
            label3.SetText("jkasdhaaaaaaaasdsad");
            _data.Add(label3);
        }

        public static void ClickExit(object sender, EventArgs e)
        {
            DialogueFinished();
        }

        public static void DialogueFinished()
        {
            HideAllSpritesAndTextures();
            //_displayText.Text = string.Empty;
            //_textureBox.Dispose();

        }

        public static void Update()
        {
            NextBtn.Update();
            ExitBtn.Update();
        }

        public static void Draw()
        {
          
            Globals.SpriteBatch.Draw(_textureBox, _rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            //foreach (Label label in _data)
            //{
            //    label.Draw();
            //}
            //label1.Draw();
            NextBtn.Draw();
            ExitBtn.Draw();
            _displayText.Draw();
            //DrawCoins();
            //Label.Draw(Coins.ToString());
            //Globals.SpriteBatch.Draw(_textureCoin, new(0, Globals.Bounds.Y - 40), null, Color.White * 0.75f, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 1f);
           
        }
    }




}
