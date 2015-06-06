using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace SwordAndScaleTake2
{
    enum GameState
    {
        Moving,
        RedTurn,
        BlueTurn,
        Waiting
    }
    enum TurnState
    {
        RedTurn,
        BlueTurn,
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Terrain[,] map;
        List<Unit> blueUnits;
        List<Unit> redUnits;
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
        Texture2D blueteam;
        Texture2D redteam;
        //Song backgroundMusic;
        Unit blueMage;
        Unit blueSword;
        Unit blueWarrior;
        Unit blueArcher;
        Unit bluePike;
        Unit redMage;
        Unit redSword;
        Unit redWarrior;
        Unit redArcher;
        Unit redPike;
        Vector2 swordBPosition;
        Vector2 warriorBPosition;
        Vector2 mageBPosition;
        Vector2 archerBPosition;
        Vector2 pikeBPosition;
        Vector2 swordRPosition;
        Vector2 warriorRPosition;
        Vector2 mageRPosition;
        Vector2 archerRPosition;
        Vector2 pikeRPosition;
        Vector2 generalBPosition;
        Vector2 generalRPosition;
        Unit generalMageB;
        Unit generalSwordB;
        Unit generalWarriorB;
        Unit generalArcherB;
        Unit generalPikeB;
        Unit generalMageR;
        Unit generalSwordR;
        Unit generalWarriorR;
        Unit generalArcherR;
        Unit generalPikeR;
        Unit currentUnit;
        Vector2 cursorPosition;
        KeyboardState pressedKey;
        KeyboardState oldState;
        List<Vector2> moveable = new List<Vector2>();
        List<PathSprite> path = new List<PathSprite>();
        GameState gameState;
        TurnState turnState;
        bool highlight = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1024;  // set this value to the desired Height of your window
            graphics.PreferredBackBufferWidth = 1536;  // set this value to the desired width of your window
            graphics.IsFullScreen = true;
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
            blueUnits = new List<Unit>();
            redUnits = new List<Unit>();
            moveable = new List<Vector2>();
            currentUnit = null;
            //Intailize the units!
            swordBPosition = new Vector2(64 * 19, 64 * 5);
            warriorBPosition = new Vector2(64 * 16, 64 * 1);
            mageBPosition = new Vector2(64 * 19, 64 * 7);
            archerBPosition = new Vector2(64 * 21, 64 * 7);
            pikeBPosition = new Vector2(64 * 16, 64 * 4);
            swordRPosition = new Vector2(64 * 4, 64 * 6);
            warriorRPosition = new Vector2(64 * 7, 64 * 11);
            mageRPosition = new Vector2(64 * 4, 64 * 9);
            archerRPosition = new Vector2(64 * 2, 64 * 6);
            pikeRPosition = new Vector2(64 * 6, 64 * 7);
            generalBPosition = new Vector2(64 * 22, 64 * 11);
            generalRPosition = new Vector2(64 * 1, 64 * 2);
            blueMage = new Unit("mage", 10, 8, 7, 1, 4, 5, true, mageBPosition);
            blueSword = new Unit("swordmaster", 10, 7, 9, 2, 3, 5, true, swordBPosition);
            blueWarrior = new Unit("warrior", 10, 9, 6, 3, 2, 4, true, warriorBPosition);
            blueArcher = new Unit("archer", 10, 6, 9, 2, 4, 6, true, archerBPosition);
            bluePike = new Unit("pike", 10, 7, 7, 4, 1, 4, true, pikeBPosition);
            redMage = new Unit("mage", 10, 8, 7, 1, 4, 5, false, mageRPosition);
            redSword = new Unit("swordmaster", 10, 7, 9, 2, 3, 5, false, swordRPosition);
            redWarrior = new Unit("warrior", 10, 9, 6, 3, 2, 4, false, warriorRPosition);
            redArcher = new Unit("archer", 10, 6, 9, 2, 4, 6, false, archerRPosition);
            redPike = new Unit("pike", 10, 7, 7, 4, 1, 4, false, pikeRPosition);
            blueUnits.Add(blueMage);
            blueUnits.Add(blueSword);
            blueUnits.Add(blueWarrior);
            blueUnits.Add(blueArcher);
            blueUnits.Add(bluePike);
            redUnits.Add(redMage);
            redUnits.Add(redSword);
            redUnits.Add(redWarrior);
            redUnits.Add(redArcher);
            redUnits.Add(redPike);
            gameState = GameState.BlueTurn;
            turnState = TurnState.BlueTurn;
            cursorPosition = swordBPosition;
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
            mapImage = Content.Load<Texture2D>("betamap");
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
            blueteam = Content.Load<Texture2D>("blueteam");
            redteam = Content.Load<Texture2D>("redteam");
            //backgroundMusic = Content.Load<Song>("music/BackTrack");
            //MediaPlayer.Play(backgroundMusic);
            //MediaPlayer.IsRepeating = true;

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
            if (oldState.IsKeyUp(Keys.Enter) && pressedKey.IsKeyDown(Keys.Enter))
            {
                Console.WriteLine(cursorPosition.X/64 +","+ cursorPosition.Y/64);
            }

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
            if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space))
            {
                if (gameState == GameState.BlueTurn)
                {
                    for (int v = 0; v < blueUnits.Count; v++)
                    {
                        if ((int)blueUnits[v].getPosition().X/64 == (int)(cursorPosition.X / 64) && (int)blueUnits[v].getPosition().Y/64 == (int)(cursorPosition.Y / 64))
                        {
                            currentUnit = blueUnits[v];
                            if (!currentUnit.getUsable())
                            {
                                break;
                            }
                            int currentMv = currentUnit.getMvmt();
                            highlight = true;
                            for (int i = 1; i < currentUnit.getMvmt() + 1; i++)
                            {
                                if (cursorPosition.X + (64 * i) < 24 * 64)
                                {
                                    Vector2 pathCor1 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y);
                                    PathSprite path1 = new PathSprite(pathCor1, this);
                                    path.Add(path1);
                                    moveable.Add(pathCor1);
                                }
                                if (cursorPosition.X - (64 * i) >= 0)
                                {
                                    Vector2 pathCor2 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y);
                                    PathSprite path2 = new PathSprite(pathCor2, this);
                                    path.Add(path2);
                                    moveable.Add(pathCor2);
                                }
                                if (cursorPosition.Y + (64 * i) < 14 * 64)
                                {
                                    Vector2 pathCor3 = new Vector2(cursorPosition.X, cursorPosition.Y + (64 * i));
                                    PathSprite path3 = new PathSprite(pathCor3, this);
                                    path.Add(path3);
                                    moveable.Add(pathCor3);
                                }
                                if (cursorPosition.Y - (64 * i) >= 0)
                                {
                                    Vector2 pathCor4 = new Vector2(cursorPosition.X, cursorPosition.Y - (64 * i));
                                    PathSprite path4 = new PathSprite(pathCor4, this);
                                    path.Add(path4);
                                    moveable.Add(pathCor4);
                                }

                                for (int j = 1; j < currentMv; j++)
                                {
                                    if (cursorPosition.X + (64 * i) < 24 * 64 && cursorPosition.Y + (64 * j) < 14 * 64)
                                    {
                                        Vector2 pathCor11 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y + (64 * j));
                                        PathSprite path11 = new PathSprite(pathCor11, this);
                                        path.Add(path11);
                                        moveable.Add(pathCor11);
                                    }
                                    if (cursorPosition.X + (64 * i) < 24 * 64 && cursorPosition.Y - (64 * j) >= 0)
                                    {
                                        Vector2 pathCor12 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y - (64 * j));
                                        PathSprite path12 = new PathSprite(pathCor12, this);
                                        path.Add(path12);
                                        moveable.Add(pathCor12);
                                    }
                                    if (cursorPosition.X - (64 * i) >= 0 && cursorPosition.Y + (64 * j) < 14 * 64)
                                    {
                                        Vector2 pathCor21 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y + (64 * j));
                                        PathSprite path21 = new PathSprite(pathCor21, this);
                                        path.Add(path21);
                                        moveable.Add(pathCor21);
                                    }
                                    if (cursorPosition.X - (64 * i) >= 0 && cursorPosition.Y - (64 * j) >= 0)
                                    {
                                        Vector2 pathCor22 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y - (64 * j));
                                        PathSprite path22 = new PathSprite(pathCor22, this);
                                        path.Add(path22);
                                        moveable.Add(pathCor22);
                                    }
                                }
                                currentMv--;
                            }
                             List<Vector2> moveNew = highlighter(moveable, currentUnit.getPosition());
                             moveable = moveNew;
                                
                                for (int i = path.Count - 1; i >= 0; i--)
                                {
                                    bool correct = false;
                                    PathSprite sprite = path[i];
                                foreach (Vector2 item in moveable)
                                {
                                    if (sprite.getPosition() == item)
                                    {
                                        correct = true;
                                        break;
                                    }
                                }
                                if (!correct)
                                {
                                    path.RemoveAt(i);
                                }
                            }
                            
                        }

                    }
                }
                if (gameState == GameState.RedTurn)
                {
                    for (int k = 0; k < redUnits.Count; k++)
                    {
                        if (redUnits[k].getPosition().X / 64 == (int)(cursorPosition.X / 64) && redUnits[k].getPosition().Y / 64 == (int)(cursorPosition.Y / 64))
                        {
                            currentUnit = redUnits[k];
                            if (!currentUnit.getUsable())
                            {
                                break;
                            }
                            int currentMv = currentUnit.getMvmt();
                            highlight = true;
                            for (int i = 1; i < currentUnit.getMvmt() + 1; i++)
                            {
                                if (cursorPosition.X + (64 * i) < 24 * 64)
                                {
                                    Vector2 pathCor1 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y);
                                    PathSprite path1 = new PathSprite(pathCor1, this);
                                    path.Add(path1);
                                    moveable.Add(pathCor1);
                                }
                                if (cursorPosition.X - (64 * i) >= 0)
                                {
                                    Vector2 pathCor2 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y);
                                    PathSprite path2 = new PathSprite(pathCor2, this);
                                    path.Add(path2);
                                    moveable.Add(pathCor2);
                                }
                                if (cursorPosition.Y + (64 * i) < 14 * 64)
                                {
                                    Vector2 pathCor3 = new Vector2(cursorPosition.X, cursorPosition.Y + (64 * i));
                                    PathSprite path3 = new PathSprite(pathCor3, this);
                                    path.Add(path3);
                                    moveable.Add(pathCor3);
                                }
                                if (cursorPosition.Y - (64 * i) >= 0)
                                {
                                    Vector2 pathCor4 = new Vector2(cursorPosition.X, cursorPosition.Y - (64 * i));
                                    PathSprite path4 = new PathSprite(pathCor4, this);
                                    path.Add(path4);
                                    moveable.Add(pathCor4);
                                }

                                for (int j = 1; j < currentMv; j++)
                                {
                                    if (cursorPosition.X + (64 * i) < 24 * 64 && cursorPosition.Y + (64 * j) < 14 * 64)
                                    {
                                        Vector2 pathCor11 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y + (64 * j));
                                        PathSprite path11 = new PathSprite(pathCor11, this);
                                        path.Add(path11);
                                        moveable.Add(pathCor11);
                                    }
                                    if (cursorPosition.X + (64 * i) < 24 * 64 && cursorPosition.Y - (64 * j) >= 0)
                                    {
                                        Vector2 pathCor12 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y - (64 * j));
                                        PathSprite path12 = new PathSprite(pathCor12, this);
                                        path.Add(path12);
                                        moveable.Add(pathCor12);
                                    }
                                    if (cursorPosition.X - (64 * i) >= 0 && cursorPosition.Y + (64 * j) < 14 * 64)
                                    {
                                        Vector2 pathCor21 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y + (64 * j));
                                        PathSprite path21 = new PathSprite(pathCor21, this);
                                        path.Add(path21);
                                        moveable.Add(pathCor21);
                                    }
                                    if (cursorPosition.X - (64 * i) >= 0 && cursorPosition.Y - (64 * j) >= 0)
                                    {
                                        Vector2 pathCor22 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y - (64 * j));
                                        PathSprite path22 = new PathSprite(pathCor22, this);
                                        path.Add(path22);
                                        moveable.Add(pathCor22);
                                    }
                                }
                                currentMv--;
                            }
                            List<Vector2> moveNew = highlighter(moveable, currentUnit.getPosition());
                            moveable = moveNew;
                            for (int i = path.Count - 1; i >= 0; i--)
                            {
                                bool correct = false;
                                PathSprite sprite = path[i];
                                foreach (Vector2 item in moveable)
                                {
                                    if (sprite.getPosition() == item)
                                    {
                                        correct = true;
                                        break;
                                    }
                                }
                                if (!correct)
                                {
                                    path.RemoveAt(i);
                                }
                            }
                        }
                    }
                }

                if (gameState == GameState.Moving)
                {
                    bool move = false;
                    foreach (Vector2 pos in moveable)
                    {
                        if (pos == cursorPosition)
                        {
                            move = true;
                        }
                    }
                    if (move)
                    {
                        if(currentUnit.getTeam()) 
                        {
                        map[(int)currentUnit.getPosition().X/64, (int)currentUnit.getPosition().Y/64].setBlueOcc(false);
                        }
                        if (!currentUnit.getTeam())
                        {
                            map[(int)currentUnit.getPosition().X / 64, (int)currentUnit.getPosition().Y / 64].setRedOcc(false);
                        }
                        currentUnit.setPosition(cursorPosition);
                        if (currentUnit.getTeam())
                        {
                            map[(int)currentUnit.getPosition().X / 64, (int)currentUnit.getPosition().Y / 64].setBlueOcc(true);
                        }
                        if (!currentUnit.getTeam())
                        {
                            map[(int)currentUnit.getPosition().X / 64, (int)currentUnit.getPosition().Y / 64].setRedOcc(true);
                        }
                        currentUnit.setUsable(false);
                        path.Clear();
                        moveable.Clear();
                           
                        //reset the gamestate to allow play to continue.
                        if (currentUnit.getTeam())
                        {
                            gameState = GameState.BlueTurn;
                        }
                        else
                        {
                            gameState = GameState.RedTurn;
                            
                        }

                        currentUnit = null;
                        highlight = false;

                        //begin turn logic
                        //check if red turn is over. If so, set gameState to blue turn.
                        if (gameState == GameState.BlueTurn)
                        {
                            bool blueTurnOver = true;
                            foreach (Unit checkUnit in blueUnits)
                            {
                                if (checkUnit.getUsable())
                                {
                                    blueTurnOver = false;
                                    break;
                                }
                            }
                            if (blueTurnOver)
                            {
                                gameState = GameState.RedTurn;
                                turnState = TurnState.RedTurn;
                                cursorPosition = redSword.getPosition();
                                foreach (Unit usable in redUnits)
                                {
                                    usable.setUsable(true);
                                }
                            }
                        }

                        //check if the blue turn is over
                        //if so, set GameState to red turn.
                        if (gameState == GameState.RedTurn)
                        {
                            bool redTurnOver = true;
                            foreach (Unit checkUnit in redUnits)
                            {
                                if (checkUnit.getUsable())
                                {
                                    redTurnOver = false;
                                    break;
                                }
                            }
                            if (redTurnOver)
                            {
                                gameState = GameState.BlueTurn;
                                turnState = TurnState.BlueTurn;
                                cursorPosition = blueSword.getPosition();
                                foreach (Unit usable in blueUnits)
                                {
                                    usable.setUsable(true);
                                }
                            }
                        }
                    }
                    //end turn logic!
                }
                if (highlight)
                {
                    gameState = GameState.Moving;
                }
            }

            oldState = pressedKey;  // set the new state as the old state for next time 
           // Console.WriteLine(gameState);
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
            if (path.Count > 0)
            {
                foreach (PathSprite space in path)
                {
                    space.LoadContent();
                    space.Draw(spriteBatch);
                }
            }
            if (turnState == TurnState.RedTurn)
            {
                spriteBatch.Draw(redteam, cursorPosition, Color.White);
            }
            if (turnState == TurnState.BlueTurn)
            {
                spriteBatch.Draw(blueteam, cursorPosition, Color.White);
            }
            spriteBatch.Draw(swordImageB, blueSword.getPosition(), Color.White);
            spriteBatch.Draw(warriorImageB, blueWarrior.getPosition(), Color.White);
            spriteBatch.Draw(mageImageB, blueMage.getPosition(), Color.White);
            spriteBatch.Draw(archerImageB, blueArcher.getPosition(), Color.White);
            spriteBatch.Draw(pikeImageB, bluePike.getPosition(), Color.White);
            spriteBatch.Draw(swordImageR, redSword.getPosition(), Color.White);
            spriteBatch.Draw(warriorImageR, redWarrior.getPosition(), Color.White);
            spriteBatch.Draw(mageImageR, redMage.getPosition(), Color.White);
            spriteBatch.Draw(archerImageR, redArcher.getPosition(), Color.White);
            spriteBatch.Draw(pikeImageR, redPike.getPosition(), Color.White);

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

            map[19, 5].setBlueOcc(true);
            map[16, 1].setBlueOcc(true);
            map[19, 7].setBlueOcc(true);
            map[21, 7].setBlueOcc(true);
            map[16, 4].setBlueOcc(true);
            map[22, 11].setBlueOcc(true);
            map[4, 6].setRedOcc(true);
            map[7, 11].setRedOcc(true);
            map[4, 9].setRedOcc(true);
            map[2, 6].setRedOcc(true);
            map[6, 7].setRedOcc(true);
            map[1, 2].setRedOcc(true);
            map[6, 1].setImpassible(true);
            map[5, 1].setImpassible(true);
            map[4, 1].setImpassible(true);
            map[3, 1].setImpassible(true);
            map[6, 2].setImpassible(true);
            map[6, 4].setImpassible(true);
            map[7, 4].setImpassible(true);
            map[8, 4].setImpassible(true);
            map[8, 5].setImpassible(true);
            map[9, 5].setImpassible(true);
            map[9, 6].setImpassible(true);
            map[9, 7].setImpassible(true);
            map[10, 7].setImpassible(true);
            map[12, 7].setImpassible(true);
            map[13, 7].setImpassible(true);
            map[13, 8].setImpassible(true);
            map[14, 8].setImpassible(true);
            map[15, 8].setImpassible(true);
            map[15, 9].setImpassible(true);
            map[15, 10].setImpassible(true);
            map[16, 10].setImpassible(true);
            map[18, 10].setImpassible(true);
            map[19, 10].setImpassible(true);
            map[20, 10].setImpassible(true);
            map[19, 11].setImpassible(true);
            map[19, 12].setImpassible(true);
            map[19, 13].setImpassible(true);
        }

        //algorithm for finding highlightable Terrain squares

      public List<Vector2> highlighter(List<Vector2> moveable, Vector2 origin) {

	// immediately remove any Terrain squares that cannot be moved into 
	// (Those already occupied and river spaces)

          for (int i = moveable.Count - 1; i >= 0; i--)
          {
              Vector2 checkValid = moveable[i];
              Terrain check = map[(int)checkValid.X / 64, (int)checkValid.Y / 64];

              if (check.getRedOcc() || check.getBlueOcc())
              {
                  moveable.RemoveAt(i);
              }
              if (check.getImpassible())
              {
                  moveable.RemoveAt(i);
              }
          }

	//This might be kind of clever. Remove any squares from the list that are not adjacent to
	//The group that is adjacent to the origin.

	List<Vector2> contiguous = new List<Vector2>();
	contiguous.Add(origin);

	int added = 1;

	while(added != 0) 
	{
		added = 0;

        for (int j = moveable.Count - 1; j >= 0; j--)
        {
            for (int k = 0; k < contiguous.Count; k++)
            {
                Vector2 test = moveable[j];
                Vector2 cont = contiguous[k];
                if ((test.X == cont.X && test.Y == (cont.Y - 64)) ||
                    (test.X == cont.X && test.Y == (cont.Y + 64)) ||
                    (test.X == (cont.X + 64) && test.Y == cont.Y) ||
                    (test.X == (cont.X - 64) && test.Y == cont.Y))
                {
                    if(!contiguous.Contains(test))
                    {
                    contiguous.Add(test);
                    added++;
                    }
                }
            }
        }
	}


	return contiguous;

}
    }
}
