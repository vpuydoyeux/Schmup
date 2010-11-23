using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Common;

namespace Schmup.XnaGame.Levels
{
    /// <summary>
    /// Store and display player's informations
    /// </summary>
    public class ScoreManager : SchmupDrawableGameComponent
    {
        /// <summary>
        /// Number of bombs left
        /// </summary>
        public int Bombs { get; set; }
        /// <summary>
        /// Remaining energy of a boss
        /// </summary>
        public int BossHealth { get; set; }
        /// <summary>
        /// Chains count
        /// </summary>
        public int Chains { get; set; }
        /// <summary>
        /// Remaining ships
        /// </summary>
        public int Lives { get; set; }
        /// <summary>
        /// Current score
        /// </summary>
        public int Score { get; set; }

        private SpriteFont scoreManagerFont;
        private SpriteBatch spriteBatch;

        private Vector2 bombsLabelPosition;
        private Vector2 chainsLabelPosition;
        private Vector2 livesLabelPosition;
        private Vector2 scoreLabelPosition;

        private Vector2 bombsValuePosition;
        private Vector2 chainsValuePosition;
        private Vector2 livesValuePosition;
        private Vector2 scoreValuePosition;

        private Texture2D bombTexture;
        private Texture2D lifeTexture;

        private Viewport viewport;

        /// <summary>
        /// Initialize a new instance of ScoreManager
        /// </summary>
        /// <param name="game">The game</param>
        public ScoreManager(SchmupGame game)
            : base(game)
        {
            Bombs = 3;
            Lives = 3;

            bombsLabelPosition = bombsValuePosition = new Vector2(40, 250);
            chainsLabelPosition = chainsValuePosition = new Vector2(40, 150);
            livesLabelPosition = livesValuePosition = new Vector2(40, 200);
            scoreLabelPosition = scoreValuePosition = new Vector2(40, 100);

            bombsValuePosition.X = 140;
            chainsValuePosition.X = 140;
            livesValuePosition.X = 140;
            scoreValuePosition.X = 140;

            this.Enabled = this.Visible = false;
        }

        #region SchmupDrawableGameComponent Members

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public override void Initialize()
        {
            viewport = new Viewport(960, 0, 320, 720);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            scoreManagerFont = Game.Content.Load<SpriteFont>("Fonts/ScoreManager");
            bombTexture = Game.Content.Load<Texture2D>("Textures/ScoreManager/Bomb");
            lifeTexture = Game.Content.Load<Texture2D>("Textures/ScoreManager/Life");
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Viewport oldViewport = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = viewport;

            Vector2 position;
            string value;

            spriteBatch.Begin();

            spriteBatch.DrawString(scoreManagerFont, "Score", scoreLabelPosition, Color.White);
            spriteBatch.DrawString(scoreManagerFont, "Chains", chainsLabelPosition, Color.White);
            spriteBatch.DrawString(scoreManagerFont, "Lives", livesLabelPosition, Color.White);
            spriteBatch.DrawString(scoreManagerFont, "Bombs", bombsLabelPosition, Color.White);

            value = Score.ToString("000000000");
            spriteBatch.DrawString(scoreManagerFont, value, scoreValuePosition, Color.Red);

            value = "x" + Chains.ToString("0000");
            spriteBatch.DrawString(scoreManagerFont, value, chainsValuePosition, Color.Red);

            position = livesValuePosition;
            for (int i = 0; i < Lives; i++)
            {
                spriteBatch.Draw(lifeTexture, position, Color.White);
                position.X += lifeTexture.Width;
            }

            position = bombsValuePosition;
            for (int i = 0; i < Lives; i++)
            {
                spriteBatch.Draw(bombTexture, position, Color.White);
                position.X += bombTexture.Width;
            }

            spriteBatch.End();

            GraphicsDevice.Viewport = oldViewport;
        }

        #endregion
    }
}
