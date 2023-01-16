using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private List<FishPlayer> _smartFishes;
        private int counter = 1;
        private int index = 1;

        public Dictionary<string, Texture2D> textureDict;
        public float currentTime;
        float countDuration = 1.2f;
        private int _numberOfFishes = 10;

        public FishGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024 + 300;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            SpriteFont defaultFont = Content.Load<SpriteFont>("defaultFont");
            this.visualization = new Visualization(GraphicsDevice, defaultFont);

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

            _smartFishes = new List<FishPlayer>();

            for (int i = 0; i < _numberOfFishes; i++)
            {
                var fish = new FishPlayer(playerTexture)
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
                    Colour = Color.Yellow,
                };
                _sprites.Add(fish);
                _smartFishes.Add(fish);

                for (int sensor = 0; sensor < fish.NumberOfSensors; sensor++)
                {
                    var sens = new Sensor(sensortexture, fish, Visualization.Lerp(90, -90, (float)sensor / (float)(fish.NumberOfSensors - 1)));
                    _sprites.Add(sens);
                }
            }
            _smartFishes[_numberOfFishes - 1].IsCurrent = true;
        }

        public void CreateObstacle()
        {

            List<int> dimensions = Obstacle.CalculateRandomRectangleParameters();
            Texture2D texture2D = Obstacle.CreateTexture(GraphicsDevice, dimensions[0], dimensions[1], pixel => Color.Red);
            redRectangle = texture2D;
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
            //skip fish if dead
            int leftRightKeyStatus = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) leftRightKeyStatus = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) leftRightKeyStatus = -1;

            if (leftRightKeyStatus != 0)
            {
                int currentFishIndex = 0;
                for (int i = 0; i < _smartFishes.Count; i++)
                {
                    if (_smartFishes[i].IsCurrent)
                    {
                        currentFishIndex = i;
                        break;
                    }
                }
                
                _smartFishes[currentFishIndex].IsCurrent = false;

                int fishCounter = _smartFishes.Count;
                while (fishCounter > 0)
                {
                    currentFishIndex += leftRightKeyStatus;
                    
                    if (currentFishIndex < 0)
                        currentFishIndex = (_smartFishes.Count - 1);
                    
                    if (currentFishIndex > (_smartFishes.Count - 1))
                        currentFishIndex = 0;

                    if (!_smartFishes[currentFishIndex].IsNotAlive)
                    {
                        _smartFishes[currentFishIndex].IsCurrent = true;
                        break;
                    }

                    fishCounter--;
                }
            }

            foreach (var sprite in _sprites)
            {

                sprite.Update(gameTime, _sprites);

            }
            _sprites.RemoveAll(sprite => sprite.Position.X < 0);
            _sprites.RemoveAll(sprite => sprite.IsNotAlive == true);

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

            FishPlayer currentFish = _smartFishes.Where(fish => fish.IsCurrent).FirstOrDefault();

            visualization.Draw(spriteBatch, currentFish.Brain.Levels, _smartFishes.IndexOf(currentFish));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}