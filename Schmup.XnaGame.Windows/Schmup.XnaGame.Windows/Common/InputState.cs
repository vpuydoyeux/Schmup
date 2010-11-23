using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Schmup.XnaGame.Common
{
    /// <summary>
    /// Store the previous and current Keyboard and GamePad states to provide more features
    /// </summary>
    public class InputState
    {
        private GamePadState currentGamePadState;
        private KeyboardState currentKeyboardState;

        private GamePadState lastGamePadState;
        private KeyboardState lastKeyboardState;

        /// <summary>
        /// Should move selection up in menus
        /// </summary>
        public bool MenuUp
        {
            get
            {
                return IsNewKeyPress(Keys.Up)
                    || (currentGamePadState.DPad.Up == ButtonState.Pressed
                        && lastGamePadState.DPad.Up == ButtonState.Released)
                    || (currentGamePadState.ThumbSticks.Left.Y > 0
                        && lastGamePadState.ThumbSticks.Left.Y <= 0);
            }
        }

        /// <summary>
        /// Should move selection down in menus
        /// </summary>
        public bool MenuDown
        {
            get
            {
                return IsNewKeyPress(Keys.Down)
                    || (currentGamePadState.DPad.Down == ButtonState.Pressed
                        && lastGamePadState.DPad.Down == ButtonState.Released)
                    || (currentGamePadState.ThumbSticks.Left.Y < 0
                        && lastGamePadState.ThumbSticks.Left.Y >= 0);
            }
        }

        /// <summary>
        /// Should validate selection in menus
        /// </summary>
        public bool MenuSelect
        {
            get
            {
                return IsNewKeyPress(Keys.Space)
                    || IsNewKeyPress(Keys.Enter)
                    || (currentGamePadState.Buttons.A == ButtonState.Pressed
                        && lastGamePadState.Buttons.A == ButtonState.Released)
                    || (currentGamePadState.Buttons.Start == ButtonState.Pressed
                        && lastGamePadState.Buttons.Start == ButtonState.Released);
            }
        }

        /// <summary>
        /// Should pause / leave game and go back to the main menu
        /// </summary>
        public bool PauseGame
        {
            get
            {
                return IsNewKeyPress(Keys.Escape)
                    || (currentGamePadState.Buttons.Back == ButtonState.Pressed
                        && lastGamePadState.Buttons.Back == ButtonState.Released)
                    || (currentGamePadState.Buttons.Start == ButtonState.Pressed
                        && lastGamePadState.Buttons.Start == ButtonState.Released);
            }
        }

        /// <summary>
        /// Should move the ship left
        /// </summary>
        public bool MoveLeft
        {
            get
            {
                return currentKeyboardState.IsKeyDown(Keys.Left)
                    || currentGamePadState.DPad.Left == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Should move the ship right
        /// </summary>
        public bool MoveRight
        {
            get
            {
                return currentKeyboardState.IsKeyDown(Keys.Right)
                    || currentGamePadState.DPad.Right == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Should move the ship up
        /// </summary>
        public bool MoveUp
        {
            get
            {
                return currentKeyboardState.IsKeyDown(Keys.Up)
                    || currentGamePadState.DPad.Up == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Should move the ship down
        /// </summary>
        public bool MoveDown
        {
            get
            {
                return currentKeyboardState.IsKeyDown(Keys.Down)
                    || currentGamePadState.DPad.Down == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Should shoot a bullet
        /// </summary>
        public bool ShootBullet
        {
            get
            {
                return IsNewKeyPress(Keys.Space)
                    || (currentGamePadState.Buttons.A == ButtonState.Pressed
                        && lastGamePadState.Buttons.A == ButtonState.Released);
            }
        }

        /// <summary>
        /// Update the previous and current input states
        /// </summary>
        public void Update()
        {
            lastKeyboardState = currentKeyboardState;
            lastGamePadState = currentGamePadState;
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        /// <summary>
        /// Was the key down before and is up now ?
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>The key has just been released</returns>
        private bool IsNewKeyPress(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key)
                && lastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Was one of the keys down before and is up now ?
        /// </summary>
        /// <param name="keys">Keys to test</param>
        /// <returns>At least one of the keys has just been released</returns>
        private bool OneOfKeysPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (IsNewKeyPress(key))
                    return true;
            return false;
        }
    }
}
