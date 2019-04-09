﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorial.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DesktopGame : Shared.GameBase
    {
        /// <summary>
        /// Make the game exit when the back button is pressed.
        /// </summary>
        /// <returns>True if the game shall exit.</returns>
        protected override bool ExitButtonsArePressed()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}
