
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;

namespace FishGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FishGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D redRectangle;

        private List<Sprite> _sprites;
        public Dictionary<string, Texture2D> textureDict;
        public float currentTime;
        float countDuration = 2f;
        int counter = 1;
        public FishGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024+300;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            IsMouseVisible= true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D sensortexture = Obstacle.CreateTexture(GraphicsDevice, 3, 250, pixel => Color.White);
            Texture2D neuralbackground = Obstacle.CreateTexture(GraphicsDevice, 300, 768, pixel => Color.Black);
            textureDict = new Dictionary<string, Texture2D>();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("fish-smallest");
            textureDict.Add(playerTexture.Name, playerTexture);

            SmartPlayer x = new SmartPlayer(playerTexture,sensortexture, 3)
            {
                Input = new Input()
                {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Up,
                    Down = Keys.Down,
                },
                Position = new Vector2(300, 100),
                speedHorizontal = 5,
                Colour= Color.Yellow,
            };
            Sprite neuralback = new Sprite(neuralbackground)
            {
                Position = new Vector2(0, 0)
        };

            _sprites = new List<Sprite>
            {
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    },
                    Position = new Vector2(100, 100),
                    speedHorizontal = 5,
                },
                x,neuralback
            };

        }

        public void CreateObstacle()
        {

            List<int> dimensions = Obstacle.CalculateRandomRectangleParameters();
            Texture2D texture2D = Obstacle.CreateTexture(GraphicsDevice, dimensions[0], dimensions[1], pixel => Color.Red);
            redRectangle =  texture2D;
            _sprites.Add(
                new Obstacle(redRectangle)
                {
                    speedHorizontal = dimensions[2],
                    Position = new Vector2(dimensions[3], dimensions[4])
                }
                );

        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            base.Update(gameTime);

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= countDuration)
            {
                CreateObstacle();
                counter++;
                currentTime -= countDuration;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}