using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Common;
using Schmup.XnaGame.DebugHelpers;
using Schmup.XnaGame.Sprites;
using Schmup.XnaGame.Sprites.Ennemies;

namespace Schmup.XnaGame.Levels
{
    /// <summary>
    /// Provide all the basic behaviours of a game's level.
    /// Inherited levels should only have a "configuration" role.
    /// </summary>
    public abstract class Level : SchmupDrawableGameComponent
    {
        /// <summary>
        /// Value of the total game time when the level began
        /// </summary>
        public TimeSpan StartTime { get; private set; }

        /// <summary>
        /// Contains all the ennemy waves of the level.
        /// Waves are removed from this list as soon as they begin.
        /// </summary>
        protected List<EnnemyWave> EnnemyWaves { get; private set; }

        private List<Bullet> shipBullets = new List<Bullet>();
        private List<Ennemy> ennemies = new List<Ennemy>();
        private Ship ship;

        private SpriteBatch spriteBatch;
        private Viewport viewport;

        private Vector2 shipBulletVelocity = new Vector2(0, -10);
        private List<EnnemyWave> currentEnnemyWaves = new List<EnnemyWave>();

#if DEBUG
        private LevelTracer levelTracer;
#endif

        #region Properties for the LevelTracer

        /// <summary>
        /// Number of bullets from the player's ship currently displayed
        /// </summary>
        public int ShipBulletsCount
        {
            get
            {
                return shipBullets.Count;
            }
        }

        /// <summary>
        /// Number of ennemies currently displayed
        /// </summary>
        public int EnnemiesCount
        {
            get
            {
                return ennemies.Count;
            }
        }

        #endregion

        /// <summary>
        /// Initialize a new instance of Level that is disabled and invisible
        /// </summary>
        /// <param name="game">The game instance</param>
        public Level(SchmupGame game)
            : base(game)
        {
            Stop();
            EnnemyWaves = new List<EnnemyWave>();
#if DEBUG
            levelTracer = new LevelTracer(game, this);
#endif
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
            viewport = new Viewport(320, 0, 640, 720);
            ship = new Ship(SchmupGame, new Vector2(280, 600));
#if DEBUG
            levelTracer.Initialize();
#endif
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            ship.Load();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (StartTime == TimeSpan.MinValue)
                StartTime = gameTime.TotalGameTime;

            InputState input = SchmupGame.InputState;
            if (input.PauseGame)
            {
                SchmupGame.MainMenu.Enabled = SchmupGame.MainMenu.Visible = true;
                SchmupGame.ScoreManager.Enabled = SchmupGame.ScoreManager.Visible = false;
                this.Enabled = this.Visible = false;
                return;
            }

            ManageWaves(gameTime);

            ship.Update(gameTime);
            shipBullets.ForEach(sprite => sprite.Update(gameTime));
            ennemies.ForEach(sprite => sprite.Update(gameTime));
            if (input.ShootBullet)
                AddBullet(shipBullets, new Bullet(SchmupGame, new Vector2(ship.Bounds.Center.X, ship.Position.Y), shipBulletVelocity, "Textures/Game/Ship/Bullet"));
            RemoveAllOutOfScreenBullets();
            RemoveAllOutOfScreenEnnemies();
            ManageCollisions();
#if DEBUG
            levelTracer.Update(gameTime);
#endif
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Viewport oldViewport = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = viewport;
            spriteBatch.Begin();
            spriteBatch.Draw(ship.Texture, ship.Position, Color.White);
            shipBullets.ForEach(sprite => spriteBatch.Draw(sprite.Texture, sprite.Position, Color.White));
            ennemies.ForEach(sprite => spriteBatch.Draw(sprite.Texture, sprite.Position, Color.White));
            spriteBatch.End();
            GraphicsDevice.Viewport = oldViewport;
#if DEBUG
            levelTracer.Draw(gameTime);
#endif
        }

        #endregion

        #region Bullets

        /// <summary>
        /// Add a bullet to a managed bullet list
        /// </summary>
        /// <param name="bulletList">The list</param>
        /// <param name="bullet">The bullet</param>
        private void AddBullet(List<Bullet> bulletList, Bullet bullet)
        {
            bullet.Load();
            bulletList.Add(bullet);
        }

        /// <summary>
        /// Remove all bullets that are out of the game area
        /// </summary>
        private void RemoveAllOutOfScreenBullets()
        {
            RemoveAllOutOfScreenBullets(shipBullets);
        }

        /// <summary>
        /// Remove all bullets from a specified list that are out of the game area
        /// </summary>
        /// <param name="bulletList">The bullet list</param>
        private void RemoveAllOutOfScreenBullets(List<Bullet> bulletList)
        {
            Rectangle bounds = new Rectangle(0, 0, viewport.Width, viewport.Height);
            for (int i = 0; i < bulletList.Count; ++i)
            {
                Bullet bullet = bulletList[i];
                if (!bullet.Bounds.Intersects(bounds))
                {
                    bulletList.Remove(bullet);
                    --i;
                }
            }
        }

        #endregion

        #region Ennemies

        /// <summary>
        /// Add an ennemy to the managed ennemy list
        /// </summary>
        /// <param name="ennemy">The ennemy to add</param>
        private void AddEnnemy(Ennemy ennemy)
        {
            ennemy.Load();
            ennemies.Add(ennemy);
        }

        /// <summary>
        /// Remove all ennemies from the managed ennemy list that are out of the game area
        /// </summary>
        private void RemoveAllOutOfScreenEnnemies()
        {
            Rectangle bounds = new Rectangle(0, 0, viewport.Width, viewport.Height);
            for (int i = 0; i < ennemies.Count; ++i)
            {
                Ennemy ennemy = ennemies[i];
                if (!ennemy.Bounds.Intersects(bounds))
                {
                    ennemies.Remove(ennemy);
                    --i;
                }
            }
        }

        #endregion

        /// <summary>
        /// Manage ennemy waves
        /// </summary>
        private void ManageWaves(GameTime gameTime)
        {
            // Add new waves if their delay is over
            for (int i = 0; i < EnnemyWaves.Count; i++)
            {
                EnnemyWave ennemyWave = EnnemyWaves[i];
                if (gameTime.TotalGameTime < ennemyWave.Delay)
                    break;
                ennemyWave.StartTime = gameTime.TotalGameTime;
                currentEnnemyWaves.Add(ennemyWave);
                EnnemyWaves.RemoveAt(i);
                --i;
            }

            // Add new ennemies from current waves
            for (int i = 0; i < currentEnnemyWaves.Count; i++)
            {
                EnnemyWave ennemyWave = currentEnnemyWaves[i];
                foreach (Ennemy ennemy in ennemyWave.GetNewEnnemies(gameTime))
                    AddEnnemy(ennemy);
            }

            // Remove empty waves
            for (int i = 0; i < currentEnnemyWaves.Count; i++)
            {
                EnnemyWave ennemyWave = currentEnnemyWaves[i];
                if (ennemyWave.EnnemyWaveItems.Count == 0)
                {
                    currentEnnemyWaves.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        /// Manage collisions between :
        /// - ennemies and the player's bullets
        /// </summary>
        private void ManageCollisions()
        {
            for (int iEnnemy = 0; iEnnemy < ennemies.Count; iEnnemy++)
            {
                Ennemy ennemy = ennemies[iEnnemy];
                for (int iBullet = 0; iBullet < shipBullets.Count; iBullet++)
                {
                    Bullet bullet = shipBullets[iBullet];
                    if (ennemy.Bounds.Intersects(bullet.Bounds))
                    {
                        ennemy.Energy--;
                        shipBullets.RemoveAt(iBullet);
                        iBullet--;
                    }
                }
                if (ennemy.Energy <= 0)
                {
                    ennemies.RemoveAt(iEnnemy);
                    --iEnnemy;
                    SchmupGame.ScoreManager.Score += ennemy.Points;
                }
            }
        }
    }
}
