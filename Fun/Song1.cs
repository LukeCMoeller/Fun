using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1.Effects;
using System.Diagnostics;
using System.Globalization;

namespace Fun
{
    public class Song1
    {
        private TimeSpan SongLength;
        private double _currentTime;
        private bool _isRunning;
        public TimeSpan[] activationTimes;

        public Song song;
        public Note[] gameplay;

        public Song1(TimeSpan duration, Song s)
        {
            _currentTime = 0;
            song = s;
            SongLength = s.Duration;
            _isRunning = false;
            gameplay = new Note[]
            {
                new Note(0, 0),
                new Note(1, 0),
                new Note(2,  0),
                new Note(3, 0),
                new Note(0, 1),
                new Note(1, 2),
                new Note(2, 5),
                new Note(3, 1)
            };
            /*
            activationTimes = new TimeSpan[gameplay.Length];
            for (int i = 0; i < interval.Length; i++)
            {
                if (i == 0)
                    activationTimes[i] = interval[i];
                else
                    activationTimes[i] = activationTimes[i - 1] + interval[i];
            }
            */
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            foreach (var note in gameplay)
            {
                {
                    note.LoadContent(content);
                }
            }
        }
        public void Start()
        {
            MediaPlayer.Play(song);
            
            _isRunning = true;
            _currentTime = 0;

            
        }

        public void Stop()
        {
            _isRunning = false;
            MediaPlayer.Stop();
        }

        public void Update(GameTime gameTime)
        {
            if (_isRunning)
            {

                /*              
                                if (_nextNoteIndex < activationTimes.Length && cumulativeTime >= activationTimes[_nextNoteIndex] )
                                {
                                    gameplay[_nextNoteIndex].IsActive = true;
                                    if()
                                    _nextNoteIndex++;
                                }*/
                /*
                if(cumulativeTime > TimeSpan.FromSeconds(10)){
                    int[] h = new int[5];
                    for(int j = 0; j < 12; j++)
                    {
                        h[j] = 0;

                    }
                }*/
                for(int i = 0; i < gameplay.Length; ++i)
                {
                    if (gameplay[i].time <= _currentTime)
                    {
                        gameplay[i].IsActive = true;
                    }
                    
                }
                if (_currentTime >= SongLength.TotalSeconds)
                {
                    Stop(); // Stops the timer when the duration is met
                    OnTimerComplete(gameTime);
                }
                _currentTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        // Event that triggers when the timer finishes
        public event Action TimerCompleted;

        protected virtual void OnTimerComplete(GameTime gameTime)
        {
            TimeSpan wait4 = default;
            wait4 += gameTime.ElapsedGameTime;
            if (wait4.TotalSeconds > 4)
            {
                TimerCompleted?.Invoke(); //return something here i guess
            }
            
        }

        public void PlayMusic(Song song)
        {
            MediaPlayer.Play(song);
        }

    }
}
