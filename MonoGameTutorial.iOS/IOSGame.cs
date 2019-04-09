using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorial.iOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class IOSGame : Shared.GameBase
    {
        public IOSGame()
        {
            Graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Make the game exit when the back button or escape key is pressed.
        /// For IOS and TVOS, exit will never be called since it is obsolete.
        /// </summary>
        /// <returns>True if the game shall exit.</returns>
        protected override bool ExitButtonsArePressed()
        {
#if !__IOS__ && !__TVOS__
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape));
#else
            return false;
#endif
        }
    }
}

