using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public class Sensor : Sprite
    {
        private readonly Texture2D texture;
        private SmartPlayer smartfish;
        public bool isColliding;
        public float rayAngle;
        public Rectangle rotatedRectangle;

        public Sensor(Texture2D texture, SmartPlayer smartfish, float angle) : base(texture)
        {
            this.texture=texture;
            this.smartfish = smartfish;
            this.rayAngle=(float)ConvertToRadians(angle+90);
        }



        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            this.Position = PositionVector();


            this.isColliding=false;
            foreach (var sprite in sprites)
            {
                if (sprite is not Player && (sprite is not Sensor))
                {
                    if (CheckIfLineIntersects(sprite, rayAngle))
                    {
                        Debug.WriteLine("hit");
                        this.isColliding= true;
                    }

                }

            }


        }

        private bool CheckIfLineIntersects(Sprite sprite,float angle)
        {
            //https://math.stackexchange.com/questions/404407/new-x-coordinate-of-a-rotated-line
            //Point C = PositionVector()
            //Point A = (PositionVector().X+texture.Height), PositionVector().Y)
            //XC = PositionVector().X
            //YC = PositionVector().Y
            //old XA = (PositionVector().X+texture.Height)
            //old YA = PositionVector().Y

            //XB = PositionVector().X + ((PositionVector().X+texture.Height) - PositionVector().X) * cos(angle) + (PositionVector().Y - PositionVector().Y) * sin(angle)
            //YB = PositionVector().Y - ((PositionVector().X+texture.Height) - PositionVector().X) * sin(angle) + (PositionVector().Y - PositionVector().Y) * cos(angle)


            //new XA = (PositionVector().X)
            //new YA = (PositionVector().Y+texture.Height)
            //
            //new XB = PositionVector().X + ((PositionVector().X) - PositionVector().X) * cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * sin(angle)
            // new XY = PositionVector().Y - ((PositionVector().X) - PositionVector().X) * sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * cos(angle)
            return LineIntersectsRect(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X) * Math.Sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Cos(angle))),sprite.Rectangle);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Vector2(smartfish.Position.X + smartfish.RectangleWidth / 2 ,smartfish.Position.Y + smartfish.RectangleHeight /2 ), this.Rectangle,Color.Yellow, 1.57079633f, new Vector2(texture.Width/2,texture.Height/2),1, SpriteEffects.None,0f);
            //spriteBatch.Draw(texture, new Vector2(300+32+50+,100+25/2), this.Rectangle, Color.Yellow, 1.57079633f, new Vector2(texture.Width/2, texture.Height/2), 1, SpriteEffects.None, 0f);
            //300=smartfish.Position.X     32/2 = smartfish.rectnaglewidth/2  50 = texture.Width/2 16 = smartfish.rectanglewidth/2
            DrawRay(spriteBatch);
        }

        private void DrawRay(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, PositionVector(), null, GetColor(), -rayAngle, new Vector2(0, 0), 1, SpriteEffects.None, 0f);

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
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        private Vector2 PositionVector()
        {
            return new Vector2(smartfish.Position.X+smartfish.Rectangle.Width+0, smartfish.Position.Y+smartfish.Rectangle.Height/2+2);
        }

        private void Move()
        {


        }
        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}



