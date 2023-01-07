using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public class Sensor : Sprite
    {
        private readonly Texture2D texture;
        private SmartPlayer smartfish;
        public bool isColliding = false;
        private Rectangle Rectangle;
        public Sensor(Texture2D texture, SmartPlayer smartfish) : base(texture)
        {
            this.texture=texture;
            this.smartfish = smartfish;
        }



        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            this.Position = PositionVector();

            Matrix transform =
            Matrix.CreateTranslation(new Vector3(-(new Vector2(0,0)), 0.0f)) *
            Matrix.CreateRotationZ(1.57079633f) *
            Matrix.CreateTranslation(new Vector3(this.Position, 0.0f));
            this.Rectangle = CalculateBoundingRectangle(Rectangle, transform);
            foreach (var sprite in sprites)
            {
                if (sprite is not Player && (sprite is not Sensor))
                {
                    if (this.Rectangle.Intersects(sprite.Rectangle))
                    {
                        Debug.WriteLine("hit");
                        isColliding= true;
                    }
                    else
                    {
                        isColliding= false;
                    }
                }

            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Vector2(smartfish.Position.X + smartfish.RectangleWidth / 2 ,smartfish.Position.Y + smartfish.RectangleHeight /2 ), this.Rectangle,Color.Yellow, 1.57079633f, new Vector2(texture.Width/2,texture.Height/2),1, SpriteEffects.None,0f);
            //spriteBatch.Draw(texture, new Vector2(300+32+50+,100+25/2), this.Rectangle, Color.Yellow, 1.57079633f, new Vector2(texture.Width/2, texture.Height/2), 1, SpriteEffects.None, 0f);
            //300=smartfish.Position.X     32/2 = smartfish.rectnaglewidth/2  50 = texture.Width/2 16 = smartfish.rectanglewidth/2
            DrawRays(spriteBatch);
        }

        private void DrawRays(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, PositionVector(), new Rectangle(), GetColor(), -1.57079633f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
/*            spriteBatch.Draw(texture, PositionVector(), this.Rectangle, GetColor(), -0.785398163f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, PositionVector(), this.Rectangle, GetColor(), -2.356194493f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);*/
        }

        private Color GetColor()
        {
            if (this.isColliding == false)
            {
                return Color.Yellow;
            }
            else
            {
                return Color.Black;
            }

        }

        private Vector2 PositionVector()
        {
            return new Vector2(smartfish.Position.X+smartfish.Rectangle.Width+0, smartfish.Position.Y+smartfish.Rectangle.Height/2+2);
        }

        private void Move()
        {


        }

        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                   Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}



