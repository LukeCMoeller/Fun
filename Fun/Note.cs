using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CollisionExample.Collisions;
namespace Fun
{
    public enum direction
    {
        Left,
        Down,
        Up,
        Right
    }
    public class Note
    {
        int scaler = 4;
        public bool IsActive { get; set; }
        int currentFrame;
        Rectangle sourceRectangle;
        Vector2 Position;
        direction d;
        Texture2D texture;
        public BoundingRectangle hitbox;
        public Note(int i) 
        {
            IsActive = false;
            Position.Y = 1010;
            currentFrame = i;
            //dont really need this code as of now. evenutally i may do a thign wher eits like 4 would be 2 notes at once. idk
            if (i == 0)
            {
                d = direction.Left;
            }
            else if (i == 1)
            {
                d = direction.Down;
            }
            else if (i == 2)
            {
                d = direction.Up;
            }
            else if (i == 3)
            {
                d = direction.Right;
            }
            else
            {
                //should never hit
            }
            sourceRectangle = new Rectangle(i * 19, 0, 19, 19);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("arrowsfull");
            hitbox = new BoundingRectangle(new Vector2(Position.X - 10, Position.Y - 10), 19, 19);
            Position = new Vector2(((1 + currentFrame) * 150) - 50, 1010);
            hitbox = new BoundingRectangle(Position.X - 19 * scaler / 2, Position.Y - 19 * scaler / 2, 19 * scaler, 19 * scaler);
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                Position.Y -= 3;
            }

        }
        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //if not is offscreen
            if(Position.Y < -50)
            {
                IsActive = false;
                return;
            }
            if (IsActive)
            {
                spriteBatch.Draw(texture, Position, sourceRectangle, Color.White, 0, new Vector2(10, 10), scaler, SpriteEffects.None, 0);
            }

        }
    }
}
