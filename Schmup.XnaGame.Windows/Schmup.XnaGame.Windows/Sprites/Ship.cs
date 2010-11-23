using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Common;

namespace Schmup.XnaGame.Sprites
{
    /// <summary>
    /// The player's ship
    /// </summary>
    public class Ship : Sprite
    {
        private Vector2 ControlledVelocity = new Vector2(5);

        /// <summary>
        /// Initialize a new instance of Ship and sets the default position
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="position">Default position</param>
        public Ship(SchmupGame game, Vector2 position)
            : base(game, position)
        {
        }

        #region Sprite Members

        /// <summary>
        /// Loads the sprite (especially the texture)
        /// </summary>
        public override void Load()
        {
            Texture = Game.Content.Load<Texture2D>("Textures/Game/Ship/Ship");
        }

        /// <summary>
        /// Allows the sprite to run logic such as updating the position,
        /// velocity, texture, ...
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            InputState input = Game.InputState;
            Vector2 position = Position;
            if (input.MoveLeft)
                position.X -= ControlledVelocity.X;
            if (input.MoveRight)
                position.X += ControlledVelocity.X;
            if (input.MoveUp)
                position.Y -= ControlledVelocity.Y;
            if (input.MoveDown)
                position.Y += ControlledVelocity.Y;
            position.X = MathHelper.Clamp(position.X, 0, 640 - Texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, 720 - Texture.Height);
            Position = position;
            base.Update(gameTime);
        }

        #endregion
    }
}
