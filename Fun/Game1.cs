using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CollisionExample.Collisions;


namespace Fun
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Song _song;
        private Note[] notes;
        private NoteSource _noteSource;
        private Song1 NiceSpace;
        private TimeSpan _currentSongTime;
        private int _nextNoteIndex;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            MediaPlayer.Volume = 0.3f;
            _graphics.PreferredBackBufferWidth = 650;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _noteSource = new NoteSource();
            _currentSongTime = TimeSpan.Zero;
            _nextNoteIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
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
            if(MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(_song);

            }
            _currentSongTime += gameTime.ElapsedGameTime;
            NiceSpace.Update(gameTime);

            foreach (Note n in NiceSpace.gameplay)
            {
                foreach(var b in _noteSource.boundingRectangles)
                {
                    if (n.hitbox.CollidesWith(b)){
                        //here
                    }
                }
               
                n.Update(gameTime);
            }
            _noteSource.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
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
