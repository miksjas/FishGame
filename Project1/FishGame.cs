using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace FishGame
{
    public class FishGame : Game
    {
        public int FishAmount { get; set; } = 50;
        public int HiddenNeuronAmount { get; set; } = 6;
        public int SensorsAmount { get; set; } = 6;
        public RunMode RunMode { get; set; } = RunMode.Simulation;

        public float simulationSpeed = 1;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D redRectangle;
        private Visualization visualization;
        private List<Sprite> _sprites;
        private List<FishPlayer> _smartFishes;
        private SpriteFont defaultFont;
        private Dictionary<string, Texture2D> textureDict;
        private float currentTime;
        private float countDuration = 1.2f;
        private int counter = 1;
        private Player player;

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
            Texture2D sensortexture = UtilityHelpers.CreateTexture(GraphicsDevice, 3, 100, pixel => Color.White);
            textureDict = new Dictionary<string, Texture2D>();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteFont defaultFont = Content.Load<SpriteFont>("defaultFont");

            this.defaultFont = defaultFont;

            visualization = new Visualization(GraphicsDevice, defaultFont);

            var playerTexture = Content.Load<Texture2D>("fish-smallest");

            textureDict.Add(playerTexture.Name, playerTexture);


            Texture2D border = UtilityHelpers.CreateTexture(GraphicsDevice, 1024, 100, pixel => Color.Red);
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

            if (RunMode == RunMode.Solo)
            {
                var leftbordertexture = UtilityHelpers.CreateTexture(GraphicsDevice, 300, 768, pixel => Color.Black);
                Obstacle leftborder = new Obstacle(leftbordertexture)
                {
                    speedHorizontal = 0,
                    Position = new Vector2(0, 0)
                };
                _sprites.Add(leftborder);
                var fish = new Player(playerTexture)
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
                };
                player = fish;

                _sprites.Add(fish);
            }
            else
            {
                for (int i = 0; i < FishAmount; i++)
                {
                    var fish = new FishPlayer(playerTexture, HiddenNeuronAmount, SensorsAmount)
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

                    for (int sensor = 0; sensor < fish.SensorsAmount; sensor++)
                    {
                        var sens = new Sensor(sensortexture, fish, UtilityHelpers.Lerp(90, -90, (float)sensor / (float)(fish.SensorsAmount - 1)));
                        _sprites.Add(sens);
                    }
                }
                _smartFishes[FishAmount - 1].IsCurrent = true;
            }
        }

        public void CreateObstacle()
        {
            List<int> dimensions = Obstacle.CalculateRandomRectangleParameters();
            Texture2D texture2D = UtilityHelpers.CreateTexture(GraphicsDevice, dimensions[0], dimensions[1], pixel => Color.Red);
            redRectangle = texture2D;
            _sprites.Add(
                new Obstacle(redRectangle)
                {
                    speedHorizontal = dimensions[2],
                    Position = new Vector2(dimensions[3], dimensions[4])
                }
                );
        }

        private void gameOver()
        {
            string message = String.Format("Simulation over. {0} Final Score {1}",
                        Environment.NewLine, (int)elapsedTime);
            if (MessageBox.Show(message, "GameOver", MessageBoxButtons.OK) == DialogResult.OK)
            {
                Exit();
            }
        }

        protected override void UnloadContent()
        {

        }

        KeyboardState kbState;
        private float elapsedTime;

        protected override void Update(GameTime gameTime)
        {
            if (RunMode == RunMode.Solo)
            {
                if (player.IsNotAlive)
                {
                    gameOver();
                }
            }
            //skip fish if dead
            if (RunMode == RunMode.Simulation)
            {

                int leftRightKeyStatus = 0;
                KeyboardState lastkbState = kbState;
                int upDownKeyStatus = 0;
                kbState = Keyboard.GetState();
                if (kbState.IsKeyDown(Keys.Up) && !lastkbState.IsKeyDown(Keys.Up))
                {
                    upDownKeyStatus = 1;
                }
                if (kbState.IsKeyDown(Keys.Down) && !lastkbState.IsKeyDown(Keys.Down))
                {
                    upDownKeyStatus = -1;
                }
                if (upDownKeyStatus == -1)
                {
                    simulationSpeed = simulationSpeed - 0.1f;
                }
                if (upDownKeyStatus == 1)
                {
                    simulationSpeed = simulationSpeed + 0.1f;
                }
                if (kbState.IsKeyDown(Keys.Left) && !lastkbState.IsKeyDown(Keys.Left))
                {
                    leftRightKeyStatus = -1;
                }
                if (kbState.IsKeyDown(Keys.Right) && !lastkbState.IsKeyDown(Keys.Right))
                {
                    leftRightKeyStatus = 1;
                }

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

            }


            foreach (var sprite in _sprites)
            {

                sprite.Update(gameTime, _sprites, simulationSpeed);

            }
            _sprites.RemoveAll(sprite => sprite.Position.X < 0);
            _sprites.RemoveAll(sprite => sprite.IsNotAlive == true);
            bool contains = _smartFishes.Any(p => p.IsNotAlive == false);
            if (!contains && _smartFishes.Any())
            {
                gameOver();
            }

            base.Update(gameTime);

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= countDuration)
            {
                CreateObstacle();
                counter++;
                currentTime -= countDuration;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            FishPlayer currentFish = _smartFishes.Where(fish => fish.IsCurrent).FirstOrDefault();

            if (currentFish != null) visualization.Draw(spriteBatch, currentFish.Brain.Levels, _smartFishes.IndexOf(currentFish));
            spriteBatch.DrawString(defaultFont, "Score: " + (int)elapsedTime, new Vector2(5, 10), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0.1f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}