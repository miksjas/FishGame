using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public class Visualization
    {
        private List<Level> levels;
        private Texture2D circle;
        private Texture2D singlePixel;
        Texture2D neuralbackground;
        private GraphicsDevice graphics;
        private int margin = 15;
        private int top = 150;
        private float bottom = 618;
        private int middle = 384;
        private int right = 255;

        public Visualization(GraphicsDevice graphics)

        {
            this.neuralbackground = Obstacle.CreateTexture(graphics, 300, 768, pixel => Color.Black);
            this.singlePixel = Obstacle.CreateTexture(graphics,1,1, pixel => Color.White);
            this.graphics = graphics;
            this.circle = CreateCircle(15);



        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {


        }
        public void Draw(SpriteBatch spriteBatch, Network network)
        {
            this.levels = network.levels;
            spriteBatch.Draw(neuralbackground, new Vector2(0, 0), null, Color.Yellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
            drawNetwork(spriteBatch, levels);



            /*            spriteBatch.Draw(circle, new Vector2(15, 150), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+52, 150), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+105+30, 150), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(255, 150), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

                        spriteBatch.Draw(circle, new Vector2(15, 618), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+52, 618), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+105+30, 618), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(255, 618), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

                        spriteBatch.Draw(circle, new Vector2(15, 384), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+52, 384), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(45+105+30, 384), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(circle, new Vector2(255, 384), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);*/




        }
        private void drawNetwork(SpriteBatch spriteBatch, List<Level> levels)
        {
            for (int i = 0; i<levels[0].inputs.Length; i++)
            {
                float by = (float)i/(float)(levels[0].inputs.Length-1);
                float x = Lerp(margin, right, by );
                spriteBatch.Draw(circle, new Vector2(x, bottom), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            }
            for (int i = 0; i<levels[0].outputs.Length; i++)
            {
                float by = (float)i/(float)(levels[0].outputs.Length-1);
                float x = Lerp(margin, right, by);
                spriteBatch.Draw(circle, new Vector2(x, middle), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            }
             for (int i = 0; i < levels[0].inputs.Length;i++)
            {
                for (int j=0; j<levels[0].outputs.Length;j++)
                {
                    float by1 = (float)i/(float)(levels[0].inputs.Length-1);
                    float x1 = Lerp(margin, right, by1);
                    float by2 = (float)j/(float)(levels[0].outputs.Length-1);
                    float x2 = Lerp(margin, right, by2);
                    DrawLine(spriteBatch, new Vector2(x1, bottom), new Vector2(x2, middle), Color.White);

                }
            }

            for (int i = 0; i<levels[1].outputs.Length; i++)
            {
                float by = (float)i/(float)(levels[1].outputs.Length-1);
                float x = Lerp(margin, right, by);
                spriteBatch.Draw(circle, new Vector2(x, top), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            }
        }

        public void DrawLine(this SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length()+width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(singlePixel, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
        public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius*2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(graphics, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f/radius;

            for (double angle = 0; angle < Math.PI*2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }

    }
}
