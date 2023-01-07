using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public class SmartPlayer : Player
    {
        private new Color Colour = Color.Yellow;


        private Texture2D sensorTexture;
        Sensor Sensor;
        public SmartPlayer(Texture2D texture, Texture2D sensorTexture) : base(texture)
        {
            this.sensorTexture=sensorTexture;
            this.Sensor = new Sensor(sensorTexture, this);

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            Move();
            Position += Velocity;
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

