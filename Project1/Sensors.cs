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
        public Vector2 IntersectionPoint;
        public bool isColliding;
        public float rayAngle;
        public Rectangle rotatedRectangle;

        public Sensor(Texture2D texture, SmartPlayer smartfish, float angle) : base(texture)
        {
            this.texture=texture;
            this.smartfish = smartfish;
            this.rayAngle=(float)ConvertToRadians(angle+90);
            smartfish.Sensors.Add(this);
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
                        Debug.WriteLine((GetIntersectionOffset(sprite, rayAngle)));
                        this.isColliding= true;
                    }

                }

            }


        }

        private bool CheckIfLineIntersects(Sprite sprite, float angle)
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
            return LineIntersectsRect(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X) * Math.Sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Cos(angle))), sprite.Rectangle);
        }
        private Vector2 VectCheckIfLineIntersects(Sprite sprite, float angle)
        {

            return GetIntersection(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X) * Math.Sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Cos(angle))), sprite.Rectangle);
        }
        private float GetIntersectionOffset(Sprite sprite, float angle)
        {   
            Vector2 startingPoint= Vector2.Zero;
            Vector2 intersection = Vector2.Zero;
            float offset = 0;
            startingPoint = PositionVector();
            intersection = GetIntersection(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X) * Math.Sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * Math.Cos(angle))), sprite.Rectangle);
            float distance = ((startingPoint.X-intersection.X)*(startingPoint.X-intersection.X)+(startingPoint.Y-intersection.Y)*(startingPoint.Y-intersection.Y));
            offset = (float)(Math.Sqrt(distance) / texture.Height);
            return offset;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Vector2(smartfish.Position.X + smartfish.RectangleWidth / 2 ,smartfish.Position.Y + smartfish.RectangleHeight /2 ), this.Rectangle,Color.Yellow, 1.57079633f, new Vector2(texture.Width/2,texture.Height/2),1, SpriteEffects.None,0f);
            //spriteBatch.Draw(texture, new Vector2(300+32+50+,100+25/2), this.Rectangle, Color.Yellow, 1.57079633f, new Vector2(texture.Width/2, texture.Height/2), 1, SpriteEffects.None, 0f);
            //300=smartfish.Position.X     32/2 = smartfish.rectnaglewidth/2  50 = texture.Width/2 16 = smartfish.rectanglewidth/2
            if (!smartfish.gameOver)
            {
                DrawRay(spriteBatch);
            }

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
        private static Vector2 GetIntersection(Vector2 p1, Vector2 p2, Rectangle r)
        {
            Vector2 intersect = new();

            intersect = VectLineIntersectsLine(p1,p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y));
            if (intersect == Vector2.Zero)
            {
                intersect = VectLineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height));
            }
            if (intersect == Vector2.Zero)
            {
                intersect = VectLineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height));
            }
            if (intersect == Vector2.Zero)
            {
                intersect = VectLineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y));
            }
            return intersect;
        }
        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {

            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        //https://stackoverflow.com/questions/2083942/draw-the-two-lines-with-intersect-each-other-and-need-to-find-the-intersect-poin
        private static bool LineIntersectsLine(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            float dx, dy, da, db, t, s;

            dx = a2.X - a1.X;
            dy = a2.Y - a1.Y;
            da = b2.X - b1.X;
            db = b2.Y - b1.Y;

            if (da * dy - db * dx == 0)
            {
                // The segments are parallel.
                //return Vector2.Zero;
                return false;
            }

            s = (dx * (b1.Y - a1.Y) + dy * (a1.X - b1.X)) / (da * dy - db * dx);
            t = (da * (a1.Y - b1.Y) + db * (b1.X - a1.X)) / (db * dx - da * dy);

            if ((s >= 0) & (s <= 1) & (t >= 0) & (t <= 1))
            {
                //return new Vector2(a1.X + t * dx, (a1.Y + t * dy));
                return true;

            }


            else
                //return Vector2.Zero;
                return false;
        }
        private static Vector2 VectLineIntersectsLine(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            float dx, dy, da, db, t, s;

            dx = a2.X - a1.X;
            dy = a2.Y - a1.Y;
            da = b2.X - b1.X;
            db = b2.Y - b1.Y;

            if (da * dy - db * dx == 0)
            {
                // The segments are parallel.
                //return Vector2.Zero;
                return Vector2.Zero;
            }

            s = (dx * (b1.Y - a1.Y) + dy * (a1.X - b1.X)) / (da * dy - db * dx);
            t = (da * (a1.Y - b1.Y) + db * (b1.X - a1.X)) / (db * dx - da * dy);

            if ((s >= 0) & (s <= 1) & (t >= 0) & (t <= 1))
            {
                return new Vector2(a1.X + t * dx, (a1.Y + t * dy));

            }


            else
                return Vector2.Zero;
        }
    }
}



