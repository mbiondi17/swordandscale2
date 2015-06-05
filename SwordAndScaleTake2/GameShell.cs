using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    class GameShell : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState { mainMenu, generalChoice, inGame, pauseMenu, gameOver} GameState gameState; 

        //All the different GUIs that will be in the game
        List<GUIElement> mainMenu = new List<GUIElement>();
        List<GUIElement> generalChoice = new List<GUIElement>();
        List<GUIElement> gameGUI = new List<GUIElement>();

        Game1 game;
        GamePreferences gamePrefs = new GamePreferences();

        public GameShell()
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
            this.IsMouseVisible = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu.Add(new GUIElement("background"));
            mainMenu.Add(new GUIElement("title"));
            mainMenu.Add(new GUIElement("start"));
            mainMenu.Add(new GUIElement("exit"));

            generalChoice.Add(new GUIElement("background-paper"));
            generalChoice.Add(new GUIElement("blueArcherGen"));
            generalChoice.Add(new GUIElement("blueMageGen"));
            generalChoice.Add(new GUIElement("bluePikeGen"));
            generalChoice.Add(new GUIElement("blueSwordGen"));
            generalChoice.Add(new GUIElement("blueWarriorGen"));

            //gameGUI.Add(new GUIElement("playerIcon"));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // ------------Main Menu------------
            foreach (GUIElement element in mainMenu)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }

            mainMenu.Find(x => x.AssetName == "title").MoveElement(1000, 730);
            mainMenu.Find(x => x.AssetName == "start").MoveElement(1000, 800);
            mainMenu.Find(x => x.AssetName == "exit") .MoveElement(1000, 840);

            // ------------Choosing a General------------
            foreach (GUIElement element in generalChoice)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }

            generalChoice.Find(x => x.AssetName == "blueArcherGen") .MoveElement(500, 400);
            generalChoice.Find(x => x.AssetName == "blueMageGen")   .MoveElement(600, 400);
            generalChoice.Find(x => x.AssetName == "bluePikeGen")   .MoveElement(700, 400);
            generalChoice.Find(x => x.AssetName == "blueSwordGen")  .MoveElement(800, 400);
            generalChoice.Find(x => x.AssetName == "blueWarriorGen").MoveElement(900, 400);

            // ------------Game GUI------------
            foreach (GUIElement element in gameGUI)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }

        }
        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.mainMenu:
                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    //    Exit(); //TODO: Replace IsKeyDown with a rising edge trigger (prevent multipressing)
                    foreach (GUIElement element in mainMenu)
                    {
                        element.Update();
                    }
                    break;
                case GameState.generalChoice:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.mainMenu;
                    foreach (GUIElement element in generalChoice)
                    {
                        element.Update();
                    }
                    break;
                case GameState.inGame:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.mainMenu; //TODO: Make pauseMenu and link it here
                    foreach (GUIElement element in gameGUI)
                    {
                        element.Update();
                    }

                    game.Update();
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch(gameState)
            {
                case GameState.mainMenu:
                    foreach (GUIElement elem in mainMenu)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                case GameState.generalChoice:
                    foreach (GUIElement elem in generalChoice)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                case GameState.inGame:
                    foreach (GUIElement elem in gameGUI)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }

            if (gameState == GameState.inGame)
                game.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void OnClick(string button)
        {
            if (button == "start")
            {
                gameState = GameState.generalChoice;
            }
            if (button == "blueArcherGen" || 
                button == "blueMageGen" || 
                button == "bluePikeGen" || 
                button == "blueSwordGen" || 
                button == "blueWarriorGen")
            {
                gamePrefs.chosenGeneral = button;
                gameState = GameState.inGame;
                game = new Game1();
                game.LoadContent(Content);
            }
            if (button == "exit")
            {
                Exit();
            }
        }
    }
}
