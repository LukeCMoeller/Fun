using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CollisionExample.Collisions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using ParticleSystemExample;



namespace Fun
{
    public class Game1 : Game
    {
        int combo;
        Random random;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Song _song;
        private NoteSource _noteSource;
        private Song1 NiceSpace;
        private TimeSpan _currentSongTime;
        public double score;
        private KeyboardState Kboard;
        private KeyboardState lastKboard;
        SpriteFont _font;

        FireworkParticleSystem _firework;

        //for the shacking
        private Vector2 shakeOffset = Vector2.Zero;
        private float shakeDuration = 0.3f;
        private float shakeTimer = 0f;
        private bool isShaking = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _firework = new FireworkParticleSystem(this, 20);
            Components.Add(_firework);
            random = new Random();
            combo = 0;
            score = 0;
            MediaPlayer.Volume = 0.3f;
            _graphics.PreferredBackBufferWidth = 650;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _noteSource = new NoteSource();
            _currentSongTime = TimeSpan.Zero;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Phy");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _song = Content.Load<Song>("NiceSpace");
            NiceSpace = new Song1(_song.Duration, _song);
            foreach (Note n in NiceSpace.gameplay)
            {
                n.LoadContent(Content);
            }
            _noteSource.LoadContent(Content);
            NiceSpace.Start();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(MediaPlayer.State != MediaState.Playing) MediaPlayer.Play(_song);
            _currentSongTime += gameTime.ElapsedGameTime;
            NiceSpace.Update(gameTime);
            Kboard = Keyboard.GetState();
            double multiplier = 1;
            foreach (Note n in NiceSpace.gameplay)
            {
                if (n.Position.Y <= -10 && !n.kill)
                {
                    combo = 0;
                    multiplier = 1;
                    n.kill = true;
                    isShaking = true;
                    shakeTimer = shakeDuration; 
                    shakeOffset = Vector2.Zero;

                    continue;
                }
                if (combo > 8)
                {
                    multiplier = 1.5;
                }
                else if (combo > 16)
                {
                    multiplier = 2;
                } else if (combo > 24)
                {
                    multiplier = 2.5;
                }else if (combo > 32)
                {
                    multiplier = 4;
                }
                foreach (var b in _noteSource.boundingRectangles)
                {
                    if (n.kill) continue;

                    if (n.hitbox.CollidesWith(b)){

                        if (Kboard.IsKeyDown(Keys.Left) && lastKboard.IsKeyUp(Keys.Left) && n.d == Direction.Left)
                        {
                            n.kill = true;
                            score += (1 * multiplier);
                            combo++;
                            _firework.PlaceFirework(new Vector2(97, 100));
                        }
                        if (Kboard.IsKeyDown(Keys.Down) && lastKboard.IsKeyUp(Keys.Down) && n.d == Direction.Down)
                        {
                            n.kill = true;
                            score += (1 * multiplier);
                            combo++;
                            _firework.PlaceFirework(new Vector2(247, 100));
                        }
                        if (Kboard.IsKeyDown(Keys.Up) && lastKboard.IsKeyUp(Keys.Up) && n.d == Direction.Up)
                        {
                            n.kill = true;
                            score += (1 * multiplier);
                            combo++;
                            _firework.PlaceFirework(new Vector2(397, 100));
                        }
                        if (Kboard.IsKeyDown(Keys.Right) && lastKboard.IsKeyUp(Keys.Right) && n.d == Direction.Right)
                        {
                            n.kill = true;
                            score += (1 * multiplier);
                            combo++;
                            _firework.PlaceFirework(new Vector2(547, 100));
                        }
                    }
                }
                
                n.Update(gameTime);
            }

            if (isShaking)
            {
                if (shakeTimer > 0)
                {
                    shakeOffset = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
                    shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    isShaking = false;
                    shakeOffset = Vector2.Zero;
                }
            }

            lastKboard = Kboard;
            _noteSource.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            string Scoretext = $"Score: {score}\nCombo: {combo}";
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0));
            _spriteBatch.DrawString(_font, Scoretext, new Vector2(10, 10), Color.White);
            _noteSource.Draw(gameTime, _spriteBatch);
            foreach (Note n in NiceSpace.gameplay) {
                if (n.IsActive)
                {
                    n.Draw(gameTime, _spriteBatch);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
     
        }

    }
}
