using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Fun

{
    public class NoteSource
    {
        /// <summary>
        /// holds all 4 of the shapes seperatly
        /// </summary>
        Rectangle[] framesRectangle = new Rectangle[4];
        /// <summary>
        /// holds all 4 of the textures seperatly. so can edit individualy. 
        /// </summary>
        Texture2D[] framesTexture = new Texture2D[4];
        /// <summary>
        /// holds all 4 of the colrs seperatly. so can edit individualy. 
        /// </summary>
        Color[] framesColor = new Color[4];
        /// <summary>
        /// holds all 4 of the bounding rectangles seperatly. so can edit individualy. 
        /// </summary>
        public BoundingRectangle[] boundingRectangles = new BoundingRectangle[4];
        public KeyboardState Kboard;
        public KeyboardState lastKboard;

        /// <summary>
        /// position of the arrows
        /// </summary>
        public Vector2[] _position = new Vector2[4];

        /// <summary>
        /// what the scale of note and hitboxes will be. set to 3 so the hitbox of the notes is slightly smaller than the rest
        /// </summary>
        public int scaler = 4;
        public NoteSource()
        {
            Kboard = Keyboard.GetState();
            lastKboard = Kboard;
        }
        

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 4; i++)
            {
                framesRectangle[i] = new Rectangle(i * 19, 0, 19, 19);
                framesTexture[i] = content.Load<Texture2D>("arrowsempty");
                _position[i] = new Vector2(((1 + i) * 150) - 50, 100);
                boundingRectangles[i] = new BoundingRectangle(_position[i].X - 19 * scaler / 2, _position[i].Y - 19 * scaler / 2, 19 * scaler, 19 * scaler);
            }

        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            Kboard = Keyboard.GetState();

            if (Kboard.IsKeyDown(Keys.Left))
            {
                framesColor[0] = Color.Purple;
            }
            if (Kboard.IsKeyDown(Keys.Down))
            {
                framesColor[1] = Color.Blue;
            } 
            if (Kboard.IsKeyDown(Keys.Up))
            {
                framesColor[2] = Color.Green;
            }
            if (Kboard.IsKeyDown(Keys.Right))
            {
                framesColor[3] = Color.Red;
            }
            lastKboard = Kboard;

        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(framesTexture[i], _position[i], framesRectangle[i], framesColor[i], 0, new Vector2(10, 10), scaler , SpriteEffects.None, 0);
                framesColor[i] = Color.White;
            }
        }
    }
}
