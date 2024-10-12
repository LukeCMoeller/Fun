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
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using SharpDX.MediaFoundation;

namespace Fun
{
    public class Song1
    {


        List<Tuple<int, int, int>> parsedData = new List<Tuple<int, int, int>>();

        private TimeSpan SongLength;
        private double _currentTime;
        private bool _isRunning;
        public TimeSpan[] activationTimes;
        public Song song;
        public Note[] gameplay;

        public Song1(TimeSpan duration, Song s)
        {
            Import n = new Import();
            List<Tuple<double, int>> test = new List<Tuple<double, int>>();
            test = n.LoadDataFromFile();

            _currentTime = 0;
            song = s;
            SongLength = s.Duration;
            _isRunning = false;

            double[] spawnIntervals = { 0, 0, 1.25, 0, 0, 0.375, 0, 0, 0.375, 0, 0, 1.25, 0, 0, 0.375, 0.375, 1.25, 0.375, 0.375, 1.25, 0.375, 0.375,0, 0, 0, 1.25, 0.375, 0.375, 1.25, 0.375, 0.375, 1.25, 0.375, 0.375, 1.25 };
            int[] notedirection = { 0, 1, 3, 3, 2, 1, 1, 2, 3, 0, 2, 3, 2, 3, 0, 2, 0, 3, 2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2 };
            gameplay = new Note[test.Count];
            int i = 0;
             foreach(var x in test)
             {
                 double activationTime = (i == 0) ? 0 : gameplay[i - 1].time + x.Item1;
                 gameplay[i] = new Note(x.Item2, (float)activationTime);
                 i++;
             }

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
                _currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < gameplay.Length; ++i)
                {
                    if (!gameplay[i].IsActive && gameplay[i].time <= _currentTime)
                    {
                        gameplay[i].IsActive = true;
                        gameplay[i].speed = CalculateSpeed(1010, 150);
                    }

                }
                if (_currentTime >= SongLength.TotalSeconds + 2)
                {
                    Stop(); // Stops the timer when the duration is met
                    OnTimerComplete(gameTime);
                }

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
        private double CalculateSpeed(double spawnAreaY, double targetAreaY)
        {
            double distance = spawnAreaY - targetAreaY;
            return distance / 2;
        }


    }
}
