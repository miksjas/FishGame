using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FishGame
{
    public class FishPlayer : Player
    {
        private new Color Colour = Color.Yellow;

        public List<Sensor> Sensors = new List<Sensor> { };
        public int NumberOfHiddenNeurons = 6;
        public int NumberOfSensors = 6;
        public Network Brain;
        public bool IsCurrent = false;

        private float[] offsets;
        private float[] outputs;
        public const int NumberOfOutputs = 4;

        public FishPlayer(Texture2D texture) : base(texture)
        {
            Brain = new Network(new int[] { NumberOfSensors, NumberOfHiddenNeurons, NumberOfOutputs });
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Debug.WriteLine(this.Position.X + " " + this.Position.Y + " ");

            offsets = new float[Sensors.Count];

            int i = 0;
            foreach (Sensor sensor in Sensors)
            {
                offsets[i++] = sensor.collidingOffset;
            }
            foreach (var item in offsets)
            {
                //Debug.WriteLine(("[{0}]", string.Join(", ", offsets)));
            }
            outputs = Network.FeedForward(offsets, Brain);
            Move();
            if (!IsNotAlive)
            {
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
            }
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    if (Velocity.Y < 0 && (Position.Y < 0) ||
                    (Velocity.Y > 0 & (Position.Y > 768 - sprite.Rectangle.Height)))
                        Velocity.Y = 0;

                    if (Velocity.X < 0 && (Position.X < 301) ||
                        (Velocity.X > 0 & (Position.X > 1024 + 1200 - sprite.Rectangle.Width)))
                        Velocity.X = 0;
            }

            Position += Velocity;
            Velocity = Vector2.Zero;
        }
        private void Move()
        {
            {
                if (outputs[1] == 1)
                {
                    //left
                    Velocity.X = -speedHorizontal;
                }

                if (outputs[2] == 1)
                {
                    //right
                    Velocity.X = speedHorizontal;
                }
                if (outputs[0] == 1)
                {
                    //up
                    Velocity.Y = -speedHorizontal;
                }

                if (outputs[3] == 1)
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
            if (!IsNotAlive)
            {
                spriteBatch.Draw(_texture, Position, Colour);
            }
        }
    }
}

