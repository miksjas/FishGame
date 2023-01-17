using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FishGame
{
    public class Visualization
    {
        private List<Level> levels;
        private Texture2D circleOutline;
        private Texture2D circleFilled;
        private SpriteFont defaultFont;
        private Texture2D singlePixel;
        Texture2D neuralbackground;
        private GraphicsDevice graphics;
        private int margin = 15;
        private int top = 150;
        private float bottom = 618;
        private int middle = 384;
        private int right = 255;

        public Visualization(GraphicsDevice graphics, SpriteFont defaultFont)

        {
            this.neuralbackground = Obstacle.CreateTexture(graphics, 300, 768, pixel => Color.Black);
            this.singlePixel = Obstacle.CreateTexture(graphics, 1, 1, pixel => Color.White);
            this.graphics = graphics;
            this.circleOutline = CreateCircleOutline(15);
            this.circleFilled = CreateCircleFilled(14);
            this.defaultFont = defaultFont;
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {


        }
        public void Draw(SpriteBatch spriteBatch, List<Level> currentBrainLevels, int currentFishIndex)
        {
            this.levels = currentBrainLevels;
            spriteBatch.Draw(neuralbackground, new Vector2(0, 0), null, Color.Yellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
            drawNetwork(spriteBatch, levels);
            spriteBatch.DrawString(defaultFont, "Network of Smartfish: " + currentFishIndex, new Vector2(5, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(defaultFont, "UP", new Vector2(20, 125), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(defaultFont, "LEFT", new Vector2(89, 125), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(defaultFont, "RIGHT", new Vector2(160, 125), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(defaultFont, "DOWN", new Vector2(240, 125), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);


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
            for (int i = 0; i < levels[0].inputs.Length; i++)
            {
                float by = (float)i / (float)(levels[0].inputs.Length - 1);
                float x = Lerp(margin, right, by);
                spriteBatch.Draw(circleOutline, new Vector2(x, bottom), null, Color.Yellow , 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, bottom + 1), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, bottom + 1), null, Color.Red * (float)levels[0].inputs[i], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            for (int i = 0; i < levels[0].outputs.Length; i++)
            {
                float by = (float)i / (float)(levels[0].outputs.Length - 1);
                float x = Lerp(margin, right, by);
                spriteBatch.Draw(circleOutline, new Vector2(x, middle), null, levels[0].biases[i] > 0 ? Color.Yellow * Math.Abs(levels[0].biases[i]) : Color.Blue * Math.Abs(levels[0].biases[i]), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, middle + 1), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, middle + 1), null, Color.Red * (float)levels[0].outputs[i], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            for (int i = 0; i < levels[0].inputs.Length; i++)
            {
                for (int j = 0; j < levels[0].outputs.Length; j++)
                {
                    float by1 = (float)i / (float)(levels[0].inputs.Length - 1);
                    float x1 = Lerp(margin, right, by1);
                    float by2 = (float)j / (float)(levels[0].outputs.Length - 1);
                    float x2 = Lerp(margin, right, by2);
                    DrawLine(spriteBatch, new Vector2(x1 + 15, bottom + 15), new Vector2(x2 + 15, middle + 15), levels[0].weights[i][j] > 0 ? Color.White * Math.Abs(levels[0].weights[i][j]) : Color.Purple * Math.Abs(levels[0].weights[i][j]));
                }
            }

            for (int i = 0; i < levels[1].outputs.Length; i++)
            {
                float by = (float)i / (float)(levels[1].outputs.Length - 1);
                float x = Lerp(margin, right, by);
                spriteBatch.Draw(circleOutline, new Vector2(x, top), null, levels[1].biases[i] > 0 ? Color.Yellow *Math.Abs(levels[1].biases[i]) : Color.Blue * Math.Abs(levels[1].biases[i]), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, top + 1), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(circleFilled, new Vector2(x + 1, top + 1), null, Color.Red * (float)levels[1].outputs[i], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            for (int i = 0; i < levels[1].inputs.Length; i++)
            {
                for (int j = 0; j < levels[1].outputs.Length; j++)
                {
                    float by1 = (float)i / (float)(levels[1].inputs.Length - 1);
                    float x1 = Lerp(margin, right, by1);
                    float by2 = (float)j / (float)(levels[1].outputs.Length - 1);
                    float x2 = Lerp(margin, right, by2);
                    DrawLine(spriteBatch, new Vector2(x1 + 15, middle + 15), new Vector2(x2 + 15, top + 15), levels[1].weights[i][j] > 0 ? Color.White * Math.Abs(levels[1].weights[i][j]) : Color.Purple * Math.Abs(levels[1].weights[i][j]));
                }
            }
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(singlePixel, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0.3f);
        }

        public Texture2D CreateCircleOutline(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(graphics, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
        public Texture2D CreateCircleFilled(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(graphics, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            bool finished = false;
            int firstSkip = 0;
            int lastSkip = 0;
            for (int i = 0; i <= data.Length - 1; i++)
            {
                if (finished == false)
                {
                    //T = transparent W = White;
                    //Find the First Batch of Colors TTTTWWWTTTT The top of the circle
                    if ((data[i] == Color.White) && (firstSkip == 0))
                    {
                        while (data[i + 1] == Color.White)
                        {
                            i++;
                        }
                        firstSkip = 1;
                        i++;
                    }
                    //Now Start Filling                       TTTTTTTTWWTTTTTTTT
                    //circle in Between                       TTTTTTW--->WTTTTTT
                    //transaparent blancks                    TTTTTWW--->WWTTTTT
                    //                                        TTTTTTW--->WTTTTTT
                    //                                        TTTTTTTTWWTTTTTTTT
                    if (firstSkip == 1)
                    {
                        if (data[i] == Color.White && data[i + 1] != Color.White)
                        {
                            i++;
                            while (data[i] != Color.White)
                            {
                                //Loop to check if its the last row of pixels
                                //We need to check this because of the 
                                //int outerRadius = radius * 2 + -->'2'<--;
                                for (int j = 1; j <= outerRadius; j++)
                                {
                                    if (data[i + j] != Color.White)
                                    {
                                        lastSkip++;
                                    }
                                }
                                //If its the last line of pixels, end drawing
                                if (lastSkip == outerRadius)
                                {
                                    break;
                                    finished = true;
                                }
                                else
                                {
                                    data[i] = Color.White;
                                    i++;
                                    lastSkip = 0;
                                }
                            }
                            while (data[i] == Color.White)
                            {
                                i++;
                            }
                            i--;
                        }


                    }
                }
            }
            // Set the data when finished 
            //-- don't need to paste this part, already given up above
            texture.SetData(data);
            return texture;
        }

       public static float Lerp(float firstFloat, float secondFloat, float by)
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
