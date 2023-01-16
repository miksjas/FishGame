using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    class Obstacle : Sprite
    {
        private readonly Texture2D texture;


        public Obstacle(Texture2D texture)
            : base(texture)

        {
            this.texture=texture;

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

        }
        private void Move()
        {
            Position += Velocity;
            Velocity.X = speedHorizontal;

        }
        override public void Draw(SpriteBatch spriteBatch)
        {

                spriteBatch.Draw(_texture, Position, null, Colour,0f,Vector2.Zero,1f,SpriteEffects.None,0.9f);

        }
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel<data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);

            return texture;
        }
        public static List<int> CalculateRandomRectangleParameters() {
            List<int> parameters = new List<int>();
            Random rnd = new Random();
            int height = rnd.Next(32, 80);
            int width = rnd.Next(height-20, height);
            int speed = rnd.Next(-3, -1);
            int X = 1500;
            int Y = rnd.Next(0, 768);
            parameters.Add(height);
            parameters.Add(width);
            parameters.Add(speed);
            parameters.Add(X);
            parameters.Add(Y);
            return parameters;
            }
    }
}
