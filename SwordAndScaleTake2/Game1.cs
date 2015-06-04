using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwordAndScaleTake2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Terrain[,] map;
        Texture2D blank;
        Texture2D yellow;
        Texture2D mapImage;
        Texture2D swordImageB;
        Texture2D mageImageB;
        Texture2D warriorImageB;
        Texture2D archerImageB;
        Texture2D pikeImageB;
        Texture2D swordImageR;
        Texture2D mageImageR;
        Texture2D warriorImageR;
        Texture2D archerImageR;
        Texture2D pikeImageR;
        Vector2 cursorPosition;
        KeyboardState pressedKey;
        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 960;  // set this value to the desired Height of your window
            graphics.PreferredBackBufferWidth = 1536;  // set this value to the desired width of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            loadMap();
            cursorPosition = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapImage = Content.Load<Texture2D>("alphamap");
            swordImageB = Content.Load<Texture2D>("blueSword");
            warriorImageB = Content.Load<Texture2D>("blueWarrior");
            mageImageB = Content.Load<Texture2D>("blueMage");
            archerImageB = Content.Load<Texture2D>("blueArcher");
            pikeImageB = Content.Load<Texture2D>("bluePike");
            swordImageR = Content.Load<Texture2D>("redSword");
            warriorImageR = Content.Load<Texture2D>("redWarrior");
            mageImageR = Content.Load<Texture2D>("redMage");
            archerImageR = Content.Load<Texture2D>("redArcher");
            pikeImageR = Content.Load<Texture2D>("redPike");
            blank = Content.Load<Texture2D>("blanks");
            yellow = Content.Load<Texture2D>("yellow");

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            pressedKey = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.Left) && pressedKey.IsKeyDown(Keys.Left) && cursorPosition.X > 0)
            {
                cursorPosition.X -= 64;
            }

            if (oldState.IsKeyUp(Keys.Right) && pressedKey.IsKeyDown(Keys.Right) && cursorPosition.X < 23 * 64)
            {
                cursorPosition.X += 64;
            }

            if (oldState.IsKeyUp(Keys.Down) && pressedKey.IsKeyDown(Keys.Down) && cursorPosition.Y < 13 * 64)
            {
                cursorPosition.Y += 64;
            }
            if (oldState.IsKeyUp(Keys.Up) && pressedKey.IsKeyDown(Keys.Up) && cursorPosition.Y > 0)
            {
                cursorPosition.Y -= 64;
            }
            oldState = pressedKey;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(mapImage, new Rectangle(0, 0, 1536, 896), Color.White);
            spriteBatch.Draw(yellow, cursorPosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
                        //24*14
            //64x64 tiles
            //1536x896
        }

        public void loadMap()
        {
            Terrain square = null;
            map = new Terrain[24, 14];
            for (int x = 0; x < 24; x++)
                for (int y = 0; y < 14; y++)
                {
                    {
                        square = new Terrain(x, y);
                        map[x, y] = square;
                    }
                }
        }
    }
}
