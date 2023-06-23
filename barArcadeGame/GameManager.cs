using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barArcadeGame
{
    public enum GameStates
    {
        Menu,
        FlipFirstCard,
        FlipSecondCard,
        ResolveTurn,
        Win
    }

    public class GameManager
    {
        public static bool IsDialogueRun;
        public GameManager()
        {
            CoinManager.Init(Globals.Content.Load<Texture2D>("picture/coin"));
            IsDialogueRun = false;
        }

        public void Init()
        {

        }

        public void runDialogue()
        {
           
            if (!IsDialogueRun)
            {
                DialogueManager.Init();
                IsDialogueRun = true;
            }
        }

        public void hideDialogue()
        {
            if (IsDialogueRun)
            {
                DialogueManager.HideAllSpritesAndTextures();
                IsDialogueRun = false;
            }
        }

        public void Update()
        {
            if (IsDialogueRun)
            {
                DialogueManager.Update();
            }
            CoinManager.Update();
          
            InputManager.Update();
        }

        public void Draw()
        {
            CoinManager.Draw();
            if (IsDialogueRun)
            {
                DialogueManager.Draw();
            }

        }
    }
}
