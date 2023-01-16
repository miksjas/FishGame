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
        private int margin = 50;

        public Visualization(GraphicsDevice graphics)

        {
            this.neuralbackground = Obstacle.CreateTexture(graphics, 300, 768, pixel => Color.Black);

            this.graphics = graphics;
            this.circle = CreateCircle(15);


        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {


        }

        public  void Draw(SpriteBatch spriteBatch, Network network)
        {
            spriteBatch.Draw(neuralbackground, new Vector2(0, 0), null, Color.Yellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);

            spriteBatch.Draw(circle, new Vector2(15, 150), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
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
            spriteBatch.Draw(circle, new Vector2(255, 384), null, Color.Yellow*0.8f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);




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

    }
}
