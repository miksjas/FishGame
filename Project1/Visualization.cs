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
    class Visualization : Sprite
    {
        private readonly Texture2D texture;


        public Visualization(Texture2D texture)
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

                spriteBatch.Draw(_texture, Rectangle, Colour);

        }


    }
}
