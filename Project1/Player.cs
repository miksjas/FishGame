using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FishGame
{
    public class Player : Sprite
    {
        public bool gameOver = false;
        public Player(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
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
/*                        if (this.IsTouchingBottom(sprite) | this.IsTouchingTop(sprite) | this.IsTouchingLeft(sprite) | this.IsTouchingRight(sprite))
                        {
                            Debug.WriteLine("hit");
                            
                        }*/
                    }

                }
            }
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    //Debug.WriteLine("X:" + this.Position.X + " " + "Y:"+ this.Position.Y);



                    if (this.Velocity.Y < 0 && (this.Position.Y < 0) ||
                    (this.Velocity.Y > 0 & (this.Position.Y > 768 - sprite.Rectangle.Height)))
                        this.Velocity.Y = 0;

                if (this.Velocity.X < 0 && (this.Position.X < 0) ||
                    (this.Velocity.X > 0 & (this.Position.X > 1024 - sprite.Rectangle.Width)))
                    this.Velocity.X = 0;
/*
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))



                    if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                        (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                        this.Velocity.Y = 0;*/



            }

            Position += Velocity;

            Velocity = Vector2.Zero;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {   
            if (!gameOver)
            {
                spriteBatch.Draw(_texture, Position, Colour);
            }

        }
        private void Move()
        {
            if (!gameOver)
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