using barArcadeGame._Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barArcadeGame._Managers
{
    public class GameManager
    {
        public static bool IsMickDialogueRun;
        public static bool IsJackDialogueRun;
        public Matrix _translation;
        DialogueManager diaMick;
        DialogueJackManager diaJack;
  
        public GameManager()
        {
            CoinManager.Init(Globals.Content.Load<Texture2D>("picture/coin"));
            IsJackDialogueRun = false;
            IsMickDialogueRun = false;
            diaJack = new DialogueJackManager();
            diaMick = new DialogueManager();
            List<string> list = new();
        }

        public void runMickDialogue()
        {
            if (!IsMickDialogueRun)
            {
                diaMick.Init();
                IsMickDialogueRun = true;
            }
        }

        public void hideMickDialogue()
        {
            if (IsMickDialogueRun)
            {
                diaMick.HideAllSpritesAndTextures();
                IsMickDialogueRun = false;
            }
        }

        public void runJackDialogue()
        {
            if (!IsJackDialogueRun)
            {
                diaJack.Init();
                IsJackDialogueRun = true;
            }
        }

        public void hideJackDialogue()
        {
            if (IsJackDialogueRun)
            {
                diaJack.HideAllSpritesAndTextures();
                IsJackDialogueRun = false;
            }
        }

        public void Update()
        {
            if (IsJackDialogueRun)
            {
                diaJack.Update();
            }

            if (IsMickDialogueRun)
            {
                diaMick.Update();
            }
      
            CoinManager.Update();
           // Door.Update();
            
            InputManager.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            CoinManager.Draw();
            Globals.SpriteBatch.End();
           

            Globals.SpriteBatch.Begin();
            if (IsJackDialogueRun)
            {
                diaJack.Draw();
            }

            if (IsMickDialogueRun)
            {
                diaMick.Draw();
            }
            Globals.SpriteBatch.End();
        }
    }
}
