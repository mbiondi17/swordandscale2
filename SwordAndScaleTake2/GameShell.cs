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

        enum GameState { mainMenu, generalChoice, generalChoiceRed, inGame, pauseMenu, gameOver} GameState gameState; 

        //All the different GUIs that will be in the game
        List<GameElement> mainMenu = new List<GameElement>();
        List<GameElement> generalChoice = new List<GameElement>();
        List<GameElement> generalChoiceRed = new List<GameElement>();

        Game1 game;
        GamePreferences gamePrefs = new GamePreferences();

        public GameShell()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1024; // set this value to the desired Height of your window
            graphics.PreferredBackBufferWidth  = 1536; // set this value to the desired width of your window
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu.Add(new GameElement("background"));
            mainMenu.Add(new GameElement("title"));
            mainMenu.Add(new GameElement("start"));
            mainMenu.Add(new GameElement("exit"));

            generalChoice.Add(new GameElement("genselect"));
            generalChoice.Add(new GameElement("blueArcherGen"));
            generalChoice.Add(new GameElement("blueMageGen"));
            generalChoice.Add(new GameElement("bluePikeGen"));
            generalChoice.Add(new GameElement("blueSwordGen"));
            generalChoice.Add(new GameElement("blueWarriorGen"));

            generalChoiceRed.Add(new GameElement("genselect"));
            generalChoiceRed.Add(new GameElement("redArcherGen"));
            generalChoiceRed.Add(new GameElement("redMageGen"));
            generalChoiceRed.Add(new GameElement("redPikeGen"));
            generalChoiceRed.Add(new GameElement("redSwordGen"));
            generalChoiceRed.Add(new GameElement("redWarriorGen"));

            //gameGUI.Add(new GUIElement("playerIcon"));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // ------------Main Menu------------
            foreach (GameElement element in mainMenu)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }

            mainMenu.Find(x => x.AssetName == "background").setPixelPosition( -50, 000);
            mainMenu.Find(x => x.AssetName == "title")     .setPixelPosition(1000, 730);
            mainMenu.Find(x => x.AssetName == "start")     .setPixelPosition(1000, 800);
            mainMenu.Find(x => x.AssetName == "exit")      .setPixelPosition(1000, 840);

            // ------------Choosing a General------------
            foreach (GameElement element in generalChoice)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }

            generalChoice.Find(x => x.AssetName == "genselect")     .setPixelPosition(-50, 000);
            generalChoice.Find(x => x.AssetName == "blueArcherGen") .setPixelPosition(500, 400);
            generalChoice.Find(x => x.AssetName == "blueMageGen")   .setPixelPosition(600, 400);
            generalChoice.Find(x => x.AssetName == "bluePikeGen")   .setPixelPosition(700, 400);
            generalChoice.Find(x => x.AssetName == "blueSwordGen")  .setPixelPosition(800, 400);
            generalChoice.Find(x => x.AssetName == "blueWarriorGen").setPixelPosition(900, 400);

            // ------------Choosing Red General------------
            foreach (GameElement element in generalChoiceRed)
            {
                element.LoadContent(Content);
                element.clickEvent += OnClick;
            }
            generalChoiceRed.Find(x => x.AssetName == "genselect").setPixelPosition(-50, 000);
            generalChoiceRed.Find(x => x.AssetName == "redArcherGen").setPixelPosition(500, 300);
            generalChoiceRed.Find(x => x.AssetName == "redMageGen").setPixelPosition(600, 300);
            generalChoiceRed.Find(x => x.AssetName == "redPikeGen").setPixelPosition(700, 300);
            generalChoiceRed.Find(x => x.AssetName == "redSwordGen").setPixelPosition(800, 300);
            generalChoiceRed.Find(x => x.AssetName == "redWarriorGen").setPixelPosition(900, 300);
        }
        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.mainMenu:
                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    //    Exit(); //TODO: Replace IsKeyDown with a rising edge trigger (prevent multipressing)
                    foreach (GameElement element in mainMenu)
                    {
                        element.Update();
                    }
                    break;
                case GameState.generalChoice:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.mainMenu;
                    foreach (GameElement element in generalChoice)
                    {
                        element.Update();
                    }
                    break;
                case GameState.generalChoiceRed:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.generalChoice;
                    foreach (GameElement element in generalChoiceRed)
                    {
                        element.Update();
                    }
                    break;
                case GameState.inGame:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.mainMenu; //TODO: Make pauseMenu and link it here
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
                    foreach (GameElement elem in mainMenu)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                case GameState.generalChoice:
                    foreach (GameElement elem in generalChoice)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                case GameState.generalChoiceRed:
                    foreach (GameElement elem in generalChoiceRed)
                    {
                        elem.Draw(spriteBatch);
                    }
                    break;
                case GameState.inGame:
                    break;
                default:
                    break;
            }

            if (gameState == GameState.inGame)
                game.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

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
                gameState = GameState.generalChoiceRed;
                //game = new Game1(gamePrefs);
                //game.LoadContent(Content);
            }
            if (button == "redArcherGen" ||
                button == "redMageGen" ||
                button == "redPikeGen" ||
                button == "redSwordGen" ||
                button == "redWarriorGen")
            {
                gamePrefs.chosenGeneralRed = button;
                gameState = GameState.inGame;
                game = new Game1(gamePrefs);
                game.LoadContent(Content);
            }
            if (button == "exit")
            {
                Exit();
            }
        }
    }
}
