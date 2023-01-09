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
    public class SmartPlayer : Player
    {
        private new Color Colour = Color.Yellow;

        public List<Sensor> Sensors = new List<Sensor> { };
        private Texture2D sensorTexture;
        public SmartPlayer(Texture2D texture, Texture2D sensorTexture) : base(texture)
        {
            this.sensorTexture=sensorTexture;
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Debug.WriteLine(this.Position.X + " " + this.Position.Y + " ");
            Debug.WriteLine(Sensors[1].isColliding);
            Move();
            if (!gameOver)
            {
                foreach (var sprite in sprites)
                {
                    if (!(sprite is Player) && (!(sprite is Sensor)))
                    {
                        if (sprite.Rectangle.Intersects(this.Rectangle))
                        {
                            Debug.WriteLine("jeff");
                            gameOver= true;
                        }
                    }

                }
            }
            foreach (var sprite in sprites)
            {
                if (sprite == this)



                    if (this.Velocity.Y < 0 && (this.Position.Y < 0) ||
                    (this.Velocity.Y > 0 & (this.Position.Y > 768 - sprite.Rectangle.Height)))
                        this.Velocity.Y = 0;

                if (this.Velocity.X < 0 && (this.Position.X < 301) ||
                    (this.Velocity.X > 0 & (this.Position.X > 1024+550 - sprite.Rectangle.Width)))
                    this.Velocity.X = 0;
            }


            Position += Velocity;
            Velocity = Vector2.Zero;
        }
        private void Move()
        {
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!gameOver)
            {
                spriteBatch.Draw(_texture, Position, Colour);

                
            }

        }

    }
}

