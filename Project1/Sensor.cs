using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FishGame
{
    public class Sensor : Sprite
    {
        public float collidingOffset;

        private float rayAngle;
        private bool isColliding;
        private readonly Texture2D texture;
        private readonly FishPlayer smartfish;

        public Sensor(Texture2D texture, FishPlayer smartfish, float angle) : base(texture)
        {
            this.texture = texture;
            this.smartfish = smartfish;
            rayAngle = (float)UtilityHelpers.ConvertToRadians(angle + 90);
            smartfish.Sensors.Add(this);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position = PositionVector();

            if (smartfish.IsNotAlive == true)
            {
                IsNotAlive = true;
            }

            isColliding = false;
            collidingOffset = 0;

            foreach (var sprite in sprites)
            {
                if (sprite is not Player && (sprite is not Sensor))
                {
                    if (CheckIfLineIntersects(sprite, rayAngle))
                    {
                        collidingOffset = GetIntersectionOffset(sprite, rayAngle);
                        isColliding = true;
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
            //XB = PositionVector().X + ((PositionVector().X+texture.Height) - PositionVector().X) * cos(angle) + (PositionVector().Y - PositionVector().Y) * sin(angle)
            //YB = PositionVector().Y - ((PositionVector().X+texture.Height) - PositionVector().X) * sin(angle) + (PositionVector().Y - PositionVector().Y) * cos(angle)

            //new XA = (PositionVector().X)
            //new YA = (PositionVector().Y+texture.Height)

            //new XB = PositionVector().X + ((PositionVector().X) - PositionVector().X) * cos(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * sin(angle)
            // new XY = PositionVector().Y - ((PositionVector().X) - PositionVector().X) * sin(angle) + ((PositionVector().Y+texture.Height) - PositionVector().Y) * cos(angle)
            return LineIntersectsRect(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle) + ((PositionVector().Y + texture.Height)
                - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X) * Math.Sin(angle)
                + (PositionVector().Y + texture.Height - PositionVector().Y)
                * Math.Cos(angle))), sprite.Rectangle);
        }

        private float GetIntersectionOffset(Sprite sprite, float angle)
        {
            Vector2 startingPoint;
            Vector2 intersection;
            float offset;
            startingPoint = PositionVector();
            intersection = GetIntersection(PositionVector(), new Vector2((float)(PositionVector().X + ((PositionVector().X) - PositionVector().X) * Math.Cos(angle)
                + ((PositionVector().Y + texture.Height) - PositionVector().Y) * Math.Sin(angle)), (float)(PositionVector().Y - ((PositionVector().X) - PositionVector().X)
                * Math.Sin(angle) + ((PositionVector().Y + texture.Height) - PositionVector().Y) * Math.Cos(angle))), sprite.Rectangle);
            float distance = ((startingPoint.X - intersection.X) * (startingPoint.X - intersection.X) + (startingPoint.Y - intersection.Y) * (startingPoint.Y - intersection.Y));
            offset = (float)(Math.Sqrt(distance) / texture.Height);
            decimal decimaloffset = Math.Round((decimal)offset, 3);
            return 1 - (float)decimaloffset;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (smartfish.IsCurrent)
            {
                DrawRay(spriteBatch);
            }
        }

        private void DrawRay(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, PositionVector(), null, GetColor(), -rayAngle, new Vector2(0, 0), 1, SpriteEffects.None, 1f);
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
            return new Vector2(smartfish.Position.X + smartfish.Rectangle.Width + 0, smartfish.Position.Y + smartfish.Rectangle.Height / 2 + 2);
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
                return false;
            }

            s = (dx * (b1.Y - a1.Y) + dy * (a1.X - b1.X)) / (da * dy - db * dx);
            t = (da * (a1.Y - b1.Y) + db * (b1.X - a1.X)) / (db * dx - da * dy);

            if ((s >= 0) & (s <= 1) & (t >= 0) & (t <= 1))
            {
                return true;

            }
            else
                return false;
        }

        private static Vector2 GetIntersection(Vector2 p1, Vector2 p2, Rectangle r)
        {
            Vector2 intersect = new();

            intersect = VectLineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y));
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



