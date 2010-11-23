using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup.XnaGame.Sprites
{
    /// <summary>
    /// Bullet from the player's ship or from ennemies
    /// </summary>
    public class Bullet : Sprite
    {
        private string bulletTexturePath;

        /// <summary>
        /// Initialize a new instance of Bullet and sets the default position, velocity and texture's path
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="position">Default position</param>
        /// <param name="velocity">Default velocity</param>
        /// <param name="bulletTexturePath">Path to the texture to load with the content manager</param>
        public Bullet(SchmupGame game, Vector2 position, Vector2 velocity, string bulletTexturePath)
            : base(game, position)
        {
            Velocity = velocity;
            this.bulletTexturePath = bulletTexturePath;
        }

        #region Sprite Members

        /// <summary>
        /// Loads the sprite (especially the texture)
        /// </summary>
        public override void Load()
        {
            Texture = Game.Content.Load<Texture2D>(bulletTexturePath);
            Position = new Vector2(Position.X - (Texture.Width / 2), Position.Y - Texture.Height);
        }

        #endregion
    }
}
