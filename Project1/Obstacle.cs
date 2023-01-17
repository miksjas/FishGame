using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FishGame
{
    class Obstacle : Sprite
    {
        private readonly Texture2D texture;

        public Obstacle(Texture2D texture)
            : base(texture)

        {
            this.texture = texture;

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

            spriteBatch.Draw(_texture, Position, null, Colour, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

        }

        public static List<int> CalculateRandomRectangleParameters()
        {
            List<int> parameters = new List<int>();
            Random rnd = new Random();
            int height = rnd.Next(32, 80);
            int width = rnd.Next(height - 20, height);
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
