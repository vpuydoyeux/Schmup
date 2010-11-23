using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup.XnaGame.Sprites
{
    /// <summary>
    /// Provide a base class to manage sprites
    /// </summary>
    public abstract class Sprite
    {
        /// <summary>
        /// Outer bounds of the sprite (texture's bounds)
        /// </summary>
        public Rectangle Bounds { get; protected set; }

        /// <summary>
        /// Collision mask of the sprite
        /// </summary>
        public Rectangle CollisonMask { get; protected set; }

        /// <summary>
        /// Position of the top left corner
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Texture to display
        /// </summary>
        public Texture2D Texture { get; protected set; }

        /// <summary>
        /// Velocity of the sprite
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// The instance of the game.
        /// Gives access to the input state, the content manager, ...
        /// </summary>
        protected SchmupGame Game { get; private set; }

        /// <summary>
        /// Initialize a new instance of Sprite and sets the default position
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="position">Default position</param>
        public Sprite(SchmupGame game, Vector2 position)
        {
            Game = game;
            Position = position;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 1, 1);
        }

        /// <summary>
        /// Loads the sprite (especially the texture)
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Allows the sprite to run logic such as updating the position,
        /// velocity, texture, ...
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (Velocity != Vector2.Zero)
                Position += Velocity;

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}
