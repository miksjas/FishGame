
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;

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
        private Visualization visualization;
        private List<Sprite> _sprites;
        private List<SmartPlayer> _smartFishes;
        private int index = 0;

        public Dictionary<string, Texture2D> textureDict;
        public float currentTime;
        public Network currentNetwork;
        float countDuration = 1.2f;
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
            Texture2D sensortexture = Obstacle.CreateTexture(GraphicsDevice, 3, 100, pixel => Color.White);
            textureDict = new Dictionary<string, Texture2D>();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.visualization = new Visualization(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("fish-smallest");
            textureDict.Add(playerTexture.Name, playerTexture);
            Texture2D border = Obstacle.CreateTexture(GraphicsDevice, 1024, 100, pixel => Color.Red);
            Obstacle topborder = new Obstacle(border)
            {
                speedHorizontal = 0,
                Position = new Vector2(300, 0)
            };
            Obstacle bottomborder = new Obstacle(border)
            {
                speedHorizontal = 0,
                Position = new Vector2(300, 668)
            };




            _sprites = new List<Sprite>
            {
                topborder,bottomborder
            };
            _smartFishes = new List<SmartPlayer>
            {
              
            };

            for (int i = 0; i<100; i++)
            {
                var fish = new SmartPlayer(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    },
                    Position = new Vector2(400, 250),
                    speedHorizontal = 5,
                    Colour= Color.Yellow,
                };
                _sprites.Add(fish);
                _smartFishes.Add(fish);
                currentNetwork = fish.brain;

                for (int b = 0; b<6; b++)
                {
                    var sens = new Sensor(sensortexture, fish, -90+36*b);
                    _sprites.Add(sens);
                }
            }
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
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentNetwork = _smartFishes[index+1].brain;
                _smartFishes[index].isCurrent = false;
                _smartFishes[index=1].isCurrent = true;
            }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentNetwork = _smartFishes[index-1].brain;
                _smartFishes[index].isCurrent = false;
                _smartFishes[index-1].isCurrent = true;

            }

            foreach (var sprite in _sprites)
            {

                sprite.Update(gameTime, _sprites);

            }
                _sprites.RemoveAll(sprite => sprite.Position.X < 0);
                _sprites.RemoveAll(sprite => sprite.gameOver == true);
            _smartFishes.RemoveAll(smartplayer => smartplayer.gameOver == true);



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

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            
            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);
            visualization.Draw(spriteBatch, currentNetwork);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}