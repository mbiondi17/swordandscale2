using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

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
        BlueTurn
    }
    public enum Teams
    {
        Red,
        Blue
    }
    public class Game1
    {
        Terrain[,] map;
        List<Unit> blueUnits;
        List<Unit> redUnits;
        Texture2D blank;
        Texture2D yellow;
        Texture2D mapImage;
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
        Vector2 cursorPosition;
        Unit activeUnit;
        Unit hoveredUnit;
        KeyboardState pressedKey;
        KeyboardState oldState;
        List<Vector2> moveable = new List<Vector2>();
        List<PathSprite> path = new List<PathSprite>();
        Teams activeTeam;
        bool isUnitMoving = false;
        UnitInfoPane blueInfoPane = new UnitInfoPane();
        UnitInfoPane redInfoPane = new UnitInfoPane();
        UnitActionPane unitActionPane = new UnitActionPane();
        bool methodCalled = false;
        GamePreferences gamePrefs;
        MoralePane blueMorale = new MoralePane(10);
        MoralePane redMorale = new MoralePane(10);

        public Game1(GamePreferences gamePrefs)
        {
            //exampleUnit = new Unit("blueArcher", "archer", 6, 9, 2, 4, 6, true);
            //exampleUnitList.Add(exampleUnit);
            //unitInfo = new UnitInfoPane();
            this.gamePrefs = gamePrefs;
            loadMap();
            blueUnits = new List<Unit>();
            redUnits = new List<Unit>();
            moveable = new List<Vector2>();
            activeUnit = null;
            hoveredUnit = null;
            activeUnit = null;
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
            blueMage = new Unit("blueMage", "mage", 10, 8, 7, 1, 4, 5, Teams.Blue, mageBPosition);
            blueSword = new Unit("blueSword", "swordmaster", 10, 7, 9, 2, 3, 5, Teams.Blue, swordBPosition);
            blueWarrior = new Unit("blueWarrior", "warrior", 10, 9, 6, 3, 2, 4, Teams.Blue, warriorBPosition);
            blueArcher = new Unit("blueArcher", "archer", 10, 6, 9, 2, 4, 6, Teams.Blue, archerBPosition);
            bluePike = new Unit("bluePike", "pike", 10, 7, 7, 4, 1, 4, Teams.Blue, pikeBPosition);
            redMage = new Unit("redMage", "mage", 10, 8, 7, 1, 4, 5, Teams.Red, mageRPosition);
            redSword = new Unit("redSword", "swordmaster", 10, 7, 9, 2, 3, 5, Teams.Red, swordRPosition);
            redWarrior = new Unit("redWarrior", "warrior", 10, 9, 6, 3, 2, 4, Teams.Red, warriorRPosition);
            redArcher = new Unit("redArcher", "archer", 10, 6, 9, 2, 4, 6, Teams.Red, archerRPosition);
            redPike = new Unit("redPike", "pike", 10, 7, 7, 4, 1, 4, Teams.Red, pikeRPosition);
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
            redMorale   .setPixelPosition(   0, 896);
            redInfoPane .setPixelPosition( 192, 896);
            blueInfoPane.setPixelPosition( 768, 896);
            blueMorale  .setPixelPosition(1344, 896);
            activeTeam = Teams.Blue;
            cursorPosition = swordBPosition;
            hoveredUnit = blueSword;
        }

        public void LoadContent(ContentManager content)
        {
            mapImage = content.Load<Texture2D>("BetaMap");
            blank = content.Load<Texture2D>("blanks");
            yellow = content.Load<Texture2D>("yellow");

            foreach (Unit unit in blueUnits)
            {
                unit.LoadContent(content);
                unit.unitClickEvent += UnitClicked;
            }

            foreach (Unit unit in redUnits)
            {
                unit.LoadContent(content);
                unit.unitClickEvent += UnitClicked;
            }

            blueteam = content.Load<Texture2D>("blueteam");
            redteam = content.Load<Texture2D>("redteam");
            //backgroundMusic = Content.Load<Song>("Sounds/BackTrack");
            //MediaPlayer.Play(backgroundMusic);
            //MediaPlayer.IsRepeating = true;
            //space.LoadContent();

            blueInfoPane.LoadContent(content);
            redInfoPane.LoadContent(content);
            UpdateInfoPanes();
            unitActionPane.LoadContent(content);
            blueMorale.LoadContent(content);
            redMorale.LoadContent(content);
        }

        public void UnloadContent()
        {

        }

        public void Update()
        {
            foreach (Unit unit in blueUnits.Concat(redUnits))
            {
                unit.Update();
            }

            pressedKey = Keyboard.GetState();
            //Move Cursor (returns true if a move occurred)
            if (MoveCursor())
            {
                //Update hoveredUnit
                DetectUnitHovered();
                //Update info panes
                UpdateInfoPanes();
            }
            //If the player isn't in the middle of moving a unit AND the cursor is over a unit (runs every update)
            if (!isUnitMoving && hoveredUnit != null)
            {
                //If Spacebar is pressed AND Unit is on the activeTeam AND Unit isUsable
                if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space) &&
                    hoveredUnit.getTeam() == activeTeam &&
                    hoveredUnit.getUsable())
                {
                    //Select that unit
                    activeUnit = hoveredUnit;
                    //Show UnitActionPane
                    unitActionPane.setPixelPosition(hoveredUnit, cursorPosition + new Vector2(64, 0));
                    unitActionPane.Show();
                }
                //If a unit is active (runs every update)
                if (activeUnit != null)
                {
                    //If A is pressed
                    if (oldState.IsKeyUp(Keys.A) && pressedKey.IsKeyDown(Keys.A))
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Attack
                        //TODO: Attack(Unit other) method call goes here
                        //When done
                        DeactivateUnit();
                    }
                    //If I is pressed
                    else if (oldState.IsKeyUp(Keys.I) && pressedKey.IsKeyDown(Keys.I))
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Interact
                        //TODO: Interact(GameElement other) method call goes here
                        //When done
                        DeactivateUnit();
                    }
                    //If M is pressed
                    else if (oldState.IsKeyUp(Keys.M) && pressedKey.IsKeyDown(Keys.M))
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Move
                        CreatePathingArea();
                        isUnitMoving = true;
                    }
                    //If W is pressed
                    else if (oldState.IsKeyUp(Keys.W) && pressedKey.IsKeyDown(Keys.W))
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Wait
                        //When done
                        DeactivateUnit();
                    }
                }
            }
            //If the player is moving a unit
            else
            {
                //If spacebar is pressed AND unit can move to the cursor's location
                if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space) &&
                    CanMoveUnit())
                {
                    MoveUnit();
                    //When done
                    DeactivateUnit();
                    UpdateInfoPanes();
                }
            }
            //If E is pressed, end turn (deactivateUnit has it's own end of turn checks)
            if (oldState.IsKeyUp(Keys.E) && pressedKey.IsKeyDown(Keys.E))
            {
                EndTurn();
            }
            // set the new state as the old state for next time 
            oldState = pressedKey;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapImage, new Rectangle(0, 0, 1536, 896), Color.White);
            if (path.Count > 0)
            {
                foreach (PathSprite space in path)
                {
                    space.Draw(spriteBatch, blank);
                }
            }
            foreach (Unit unit in blueUnits)
            {
                unit.Draw(spriteBatch);
            }
            foreach (Unit unit in redUnits)
            {
                unit.Draw(spriteBatch);
            }

            if (activeTeam == Teams.Red)
            {
                spriteBatch.Draw(redteam, cursorPosition, Color.White);
            }
            else if (activeTeam == Teams.Blue)
            {
                spriteBatch.Draw(blueteam, cursorPosition, Color.White);
            }
            blueInfoPane.Draw(spriteBatch);
            redInfoPane.Draw(spriteBatch);
            unitActionPane.Draw(spriteBatch);
            blueMorale.Draw(spriteBatch);
            redMorale.Draw(spriteBatch);
        }

        public void UnitClicked(Unit unit, int x, int y)
        {
            unitActionPane.setPixelPosition(unit, x + 64, y);
            unitActionPane.Show();
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

        private void CreatePathingArea()
        {
            int currentMv = activeUnit.getMvmt();
            for (int i = 1; i < activeUnit.getMvmt() + 1; i++)
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
            List<Vector2> moveNew = highlighter(moveable, activeUnit.getPosition());
            methodCalled = false;
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

        //algorithm for finding highlightable Terrain squares
        public List<Vector2> highlighter(List<Vector2> moveable, Vector2 origin)
        {

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

            while (added != 0)
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
                            if (!contiguous.Contains(test))
                            {
                                contiguous.Add(test);
                                added++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < contiguous.Count; i++)
            {
                Vector2 moveItem = contiguous[i];
                if (moveItem.X == 17 * 64 && moveItem.Y == 10 * 64 && !methodCalled)
                {
                    methodCalled = true;
                    contiguous = reHighlight(origin, moveItem, 2, contiguous);
                }
            }

            return contiguous;

        }

        public List<Vector2> reHighlight(Vector2 playerOrigin, Vector2 origin, int Mvmt, List<Vector2> moveable)
        {
            List<Vector2> bridge = new List<Vector2>();
            for (int i = 1; i < Mvmt + 1; i++)
            {
                if (cursorPosition.X + (64 * i) < 24 * 64)
                {
                    Vector2 pathCor1 = new Vector2(cursorPosition.X + (64 * i), cursorPosition.Y);
                    moveable.Add(pathCor1);
                }
                if (cursorPosition.X - (64 * i) >= 0)
                {
                    Vector2 pathCor2 = new Vector2(cursorPosition.X - (64 * i), cursorPosition.Y);
                    moveable.Add(pathCor2);
                }
                if (cursorPosition.Y + (64 * i) < 14 * 64)
                {
                    Vector2 pathCor3 = new Vector2(cursorPosition.X, cursorPosition.Y + (64 * i));
                    moveable.Add(pathCor3);
                }
                if (cursorPosition.Y - (64 * i) >= 0)
                {
                    Vector2 pathCor4 = new Vector2(cursorPosition.X, cursorPosition.Y - (64 * i));
                    moveable.Add(pathCor4);
                }

                for (int j = 1; j < Mvmt; j++)
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
                Mvmt--;
            }
            bridge = highlighter(moveable, origin);
            return bridge;
        }

        private bool CanMoveUnit()
        {
            foreach (Vector2 pos in moveable)
            {
                if (pos == cursorPosition)
                {
                    return true;
                }
            }
            return false;
        }

        private void MoveUnit()
        {
            if (activeUnit.isBlue())
            {
                map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setBlueOcc(false);
            }
            if (!activeUnit.isBlue())
            {
                map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setRedOcc(false);
            }
            activeUnit.setPosition(cursorPosition);
            if (activeUnit.isBlue())
            {
                map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setBlueOcc(true);
            }
            if (!activeUnit.isBlue())
            {
                map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setRedOcc(true);
            }
            path.Clear();
            moveable.Clear();
            isUnitMoving = false;
        }

        private bool MoveCursor()
        {
            if (oldState.IsKeyUp(Keys.Left) && pressedKey.IsKeyDown(Keys.Left) && cursorPosition.X > 0)
            {
                cursorPosition.X -= 64;
                return true;
            }

            if (oldState.IsKeyUp(Keys.Right) && pressedKey.IsKeyDown(Keys.Right) && cursorPosition.X < 23 * 64)
            {
                cursorPosition.X += 64;
                return true;
            }

            if (oldState.IsKeyUp(Keys.Down) && pressedKey.IsKeyDown(Keys.Down) && cursorPosition.Y < 13 * 64)
            {
                cursorPosition.Y += 64;
                return true;
            }
            if (oldState.IsKeyUp(Keys.Up) && pressedKey.IsKeyDown(Keys.Up) && cursorPosition.Y > 0)
            {
                cursorPosition.Y -= 64;
                return true;
            }
            return false;
        }

        private void DetectUnitHovered()
        {
            //See if cursor is over any unit
            hoveredUnit = blueUnits.Concat(redUnits).FirstOrDefault(unit => unit.getPixelPosition() == cursorPosition);

            //If the cursor is not over the unit, then hide the action pane
            if (hoveredUnit == null)
            {
                unitActionPane.Hide();

                if (!isUnitMoving)
                {
                    activeUnit = null;
                }
            }
        }

        private void UpdateInfoPanes()
        {
            blueInfoPane.setUnit(null);
            redInfoPane.setUnit(null);
            if (hoveredUnit != null)
            {
                //Show Unit stats on its team's side
                (hoveredUnit.getTeam() == Teams.Blue ? blueInfoPane : redInfoPane).setUnit(hoveredUnit);
            }

            if (activeUnit != null)
            {
                //Show Unit stats on its team's side
                (activeTeam == Teams.Blue ? blueInfoPane : redInfoPane).setUnit(activeUnit);
            }

        }

        private void DeactivateUnit()
        {
            activeUnit.setUsable(false);
            //Get next usable unit
            Unit nextUnit = (activeTeam == Teams.Blue ? blueUnits : redUnits).FirstOrDefault(next => next.getUsable());
            //If there is a next unit
            if (nextUnit != null)
            {
                //Move cursor to next unit
                cursorPosition = nextUnit.getPosition();
                DetectUnitHovered();
            }
            //If there are no more usable units
            else
            {
                EndTurn();
            }
            activeUnit = null;
        }

        private void EndTurn()
        {
            //Move cursor to other team's unit
            cursorPosition = (activeTeam == Teams.Blue ? redUnits : blueUnits).First().getPosition();
            DetectUnitHovered();
            //Reset each unit in current team
            foreach (Unit unit in (activeTeam == Teams.Blue ? blueUnits : redUnits))
            {
                unit.setUsable(true);
            }
            //Other team's turn
            activeTeam = (activeTeam == Teams.Blue ? Teams.Red : Teams.Blue);
        }
    }
}
