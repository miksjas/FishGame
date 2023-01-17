using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;


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
        private SpriteFont defaultFont;
        public Dictionary<string, Texture2D> textureDict;
        public float currentTime;
        float countDuration = 1.2f;
        private int _numberOfFishes = 50;
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
            Texture2D sensortexture = Obstacle.CreateTexture(GraphicsDevice, 3, 100, pixel => Microsoft.Xna.Framework.Color.White);
            textureDict = new Dictionary<string, Texture2D>();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteFont defaultFont = Content.Load<SpriteFont>("defaultFont");

            this.defaultFont = defaultFont;

            this.visualization = new Visualization(GraphicsDevice, defaultFont);

            var playerTexture = Content.Load<Texture2D>("fish-smallest");

            textureDict.Add(playerTexture.Name, playerTexture);


            Texture2D border = Obstacle.CreateTexture(GraphicsDevice, 1024, 100, pixel => Microsoft.Xna.Framework.Color.Red);
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
                        Left = Microsoft.Xna.Framework.Input.Keys.A,
                        Right = Microsoft.Xna.Framework.Input.Keys.D,
                        Up = Microsoft.Xna.Framework.Input.Keys.W,
                        Down = Microsoft.Xna.Framework.Input.Keys.S,
                    },
                    Position = new Vector2(400, 250),
                    speedHorizontal = 5,
                    Colour = Microsoft.Xna.Framework.Color.Yellow,
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
            Texture2D texture2D = Obstacle.CreateTexture(GraphicsDevice, dimensions[0], dimensions[1], pixel => Microsoft.Xna.Framework.    Color.Red);
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
                    this.Exit();
                }

        }


        protected override void UnloadContent()
        {
            
        }


        KeyboardState kbState;
        private float elapsedTime;
        private bool m_IsGameOver;

        protected override void Update(GameTime gameTime)
        {
            //skip fish if dead
            int leftRightKeyStatus = 0;
            KeyboardState lastkbState = kbState;
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && !lastkbState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                leftRightKeyStatus = -1;
            }
            if (kbState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && !lastkbState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
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

            foreach (var sprite in _sprites)
            {

                sprite.Update(gameTime, _sprites);

            }
            _sprites.RemoveAll(sprite => sprite.Position.X < 0);
            _sprites.RemoveAll(sprite => sprite.IsNotAlive == true);
            bool contains = _smartFishes.Any(p => p.IsNotAlive == false);
            if (!contains)
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
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            FishPlayer currentFish = _smartFishes.Where(fish => fish.IsCurrent).FirstOrDefault();

            visualization.Draw(spriteBatch, currentFish.Brain.Levels, _smartFishes.IndexOf(currentFish));
            spriteBatch.DrawString(defaultFont, "Score: " +  (int)elapsedTime, new Vector2(5, 10), Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0.1f);

            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}