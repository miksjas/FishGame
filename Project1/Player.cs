using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FishGame
{
    public class Player : Sprite
    {
        public Player(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (!(sprite is Player) && (!(sprite is Sensor)))
                {
                    if (sprite.Rectangle.Intersects(Rectangle))
                    {
                        IsNotAlive = true;
                    }
                }
            }

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    if (Velocity.Y < 0 && (Position.Y < 0) ||
                        (Velocity.Y > 0 & (Position.Y > 768 - sprite.Rectangle.Height)))
                        Velocity.Y = 0;

                    if (Velocity.X < 0 && (Position.X < 0) ||
                        (Velocity.X > 0 & (Position.X > 1024 - sprite.Rectangle.Width)))
                        Velocity.X = 0;
                }
            }

            Position += Velocity;

            Velocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsNotAlive)
            {
                spriteBatch.Draw(_texture, Position, Colour);
            }

        }

        private void Move()
        {
            if (!IsNotAlive)
            {
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                    Velocity.X = -speedHorizontal;
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                    Velocity.X = speedHorizontal;

                if (Keyboard.GetState().IsKeyDown(Input.Up))
                    Velocity.Y = -speedHorizontal;
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                    Velocity.Y = speedHorizontal;

            }

        }
    }
}