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
        public Network brain;
        public bool isCurrent = false;

        private float[] offsets;
        private float[] outputs;
        public SmartPlayer(Texture2D texture) : base(texture)
        {

            this.brain = new Network(new int[] {6,6,4});


        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Debug.WriteLine(this.Position.X + " " + this.Position.Y + " ");

            this.offsets = new float[Sensors.Count];

            int i = 0;
            foreach (Sensor sensor in Sensors)
            {
                offsets[i++] = sensor.collidingOffset;
            }
            foreach (var item in offsets)
            {
                //Debug.WriteLine(("[{0}]", string.Join(", ", offsets)));
            }
            this.outputs = Network.FeedForward(offsets, this.brain);
            Move();
            if (!gameOver)
            {
                foreach (var sprite in sprites)
                {
                    if (!(sprite is Player) && (!(sprite is Sensor)))
                    {
                        if (sprite.Rectangle.Intersects(this.Rectangle))
                        {
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
                    (this.Velocity.X > 0 & (this.Position.X > 1024 + 1200 - sprite.Rectangle.Width)))
                    this.Velocity.X = 0;
            }


            Position += Velocity;
            Velocity = Vector2.Zero;
        }
        private void Move()
        {
            {
                if (this.outputs[1] == 1)
                {
                    //left
                    Velocity.X = -speedHorizontal;
                }

                if (this.outputs[2] == 1)
                {
                    //right
                    Velocity.X = speedHorizontal;
                }
                if (this.outputs[0] == 1)
                {
                    //up
                    Velocity.Y = -speedHorizontal;
                }

                if (this.outputs[3] == 1)
                {
                    //down
                    Velocity.Y = speedHorizontal;
                }



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

