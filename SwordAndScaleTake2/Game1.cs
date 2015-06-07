﻿using Microsoft.Xna.Framework;
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
        Attacking,
        Interacting,
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
        Unit blueGeneral;
        Unit redMage;
        Unit redSword;
        Unit redWarrior;
        Unit redArcher;
        Unit redPike;
        Unit redGeneral;
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
        List<Unit> attackable = new List<Unit>();
        List<PathSprite> path = new List<PathSprite>();
        List<PathSprite> enemies = new List<PathSprite>();
        Teams activeTeam;
        bool isUnitMoving = false;
        bool isUnitAttacking = false;
        bool isUnitInteracting = false;
        UnitInfoPane blueInfoPane = new UnitInfoPane();
        UnitInfoPane redInfoPane = new UnitInfoPane();
        UnitActionPane unitActionPane = new UnitActionPane();
        bool methodCalled = false;
        GamePreferences gamePrefs;
        MoralePane blueMorale = new MoralePane(10, "black");
        MoralePane redMorale = new MoralePane(10, "black");

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

            // redGeneral = redGeneralChoice();
            // redUnits.Add(redGeneral);
            //TODO blue general

            redMorale.setPixelPosition(0, 896);
            redInfoPane.setPixelPosition(192, 896);
            blueInfoPane.setPixelPosition(768, 896);
            blueMorale.setPixelPosition(1344, 896);
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

            //blueInfoPane.Update();
            //redInfoPane.Update();
            //unitActionPane.Update();

            pressedKey = Keyboard.GetState();
            //Move Cursor (returns true if a move occurred)
            if (MoveCursor(oldState, pressedKey, ref cursorPosition))
            {
                //Update hoveredUnit
                DetectUnitHovered();
                //Update info panes
                UpdateInfoPanes();
            }
            //If the player isn't in the middle of moving a unit AND the cursor is over a unit (runs every update)
            if (!isUnitMoving && !isUnitAttacking && !isUnitInteracting && hoveredUnit != null)
            {
                //If Spacebar is pressed AND Unit is on the activeTeam AND Unit isUsable
                if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space) &&
                    hoveredUnit.getTeam() == activeTeam &&
                    hoveredUnit.getUsable() && !hoveredUnit.getDead())
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
                    if (oldState.IsKeyUp(Keys.A) && pressedKey.IsKeyDown(Keys.A) && !activeUnit.getHasActed())
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Prepare for Attack
                        CreateAttackingArea();
                        isUnitAttacking = true;
                    }
                    //If I is pressed
                    else if (oldState.IsKeyUp(Keys.I) && pressedKey.IsKeyDown(Keys.I) && !activeUnit.getHasActed())
                    {
                        //Hide UnitActionPane
                        unitActionPane.Hide();
                        //Interact
                        //TODO: Interact(GameElement other) method call goes here
                        //When done
                        //isUnitInteracting = true;
                        DeactivateUnit();
                    }
                    //If M is pressed
                    else if (oldState.IsKeyUp(Keys.M) && pressedKey.IsKeyDown(Keys.M) && !activeUnit.getHasMoved())
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
            else if (isUnitMoving)
            {
                //If spacebar is pressed AND unit can move to the cursor's location
                if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space) &&
                    CanMoveUnit())
                {
                    MoveUnit();
                    //When done
                    activeUnit.setHasMoved(true);
                    if (activeUnit.getHasActed())
                    {
                        DeactivateUnit();
                    }
                }
            }
            //If the player is attacking
            else if (isUnitAttacking)
            {
                if (oldState.IsKeyUp(Keys.Space) && pressedKey.IsKeyDown(Keys.Space) &&
                    CanAttackEnemy())
                {
                    //get enemy to attack and do so
                    Unit theEnemy = unitToAttack();
                    attack(ref theEnemy);

                    // enemies.Clear();
                    // attackable.Clear();
                    activeUnit.setHasActed(true);

                    if (activeUnit.getHasMoved())
                    {
                        DeactivateUnit();
                    }
                }
            }
            else if (isUnitInteracting)
            {
                //TODO
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

            if (attackable.Count > 0)
            {

                //Console.WriteLine(attackable.Count);
                foreach (PathSprite enemy in enemies)
                {
                    enemy.Draw(spriteBatch, blank);
                }
            }
            foreach (Unit unit in blueUnits)
            {
                if (!unit.getDead())
                {
                    unit.Draw(spriteBatch);
                }
            }
            foreach (Unit unit in redUnits)
            {
                if (!unit.getDead())
                {
                    unit.Draw(spriteBatch);
                }
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
            map[6, 1].setimpassable(true);
            map[5, 1].setimpassable(true);
            map[4, 1].setimpassable(true);
            map[3, 1].setimpassable(true);
            map[6, 2].setimpassable(true);
            map[6, 4].setimpassable(true);
            map[7, 4].setimpassable(true);
            map[8, 4].setimpassable(true);
            map[8, 5].setimpassable(true);
            map[9, 5].setimpassable(true);
            map[9, 6].setimpassable(true);
            map[9, 7].setimpassable(true);
            map[10, 7].setimpassable(true);
            map[12, 7].setimpassable(true);
            map[13, 7].setimpassable(true);
            map[13, 8].setimpassable(true);
            map[14, 8].setimpassable(true);
            map[15, 8].setimpassable(true);
            map[15, 9].setimpassable(true);
            map[15, 10].setimpassable(true);
            map[16, 10].setimpassable(true);
            map[18, 10].setimpassable(true);
            map[19, 10].setimpassable(true);
            map[20, 10].setimpassable(true);
            map[19, 11].setimpassable(true);
            map[19, 12].setimpassable(true);
            map[19, 13].setimpassable(true);
        }

        public void CreateAttackingArea()
        {
            Teams team = activeUnit.getTeam();
            string unitType = activeUnit.getType();
            List<Vector2> reachable = new List<Vector2>();
            float activeX = activeUnit.getPosition().X;
            float activeY = activeUnit.getPosition().Y;

            if (unitType.Contains("rch"))
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        reachable.Add(new Vector2(activeX + x * 64, activeY + y * 64));
                    }
                }

                reachable.Add(new Vector2(activeX + 2 * 64, activeY));
                reachable.Add(new Vector2(activeX - 2 * 64, activeY));
                reachable.Add(new Vector2(activeX, activeY + 2 * 64));
                reachable.Add(new Vector2(activeX, activeY - 2 * 64));
            }

            else
            {
                reachable.Add(new Vector2(activeX - 64, activeY));
                reachable.Add(new Vector2(activeX, activeY - 64));
                reachable.Add(new Vector2(activeX + 64, activeY));
                reachable.Add(new Vector2(activeX, activeY + 64));
            }

            Console.WriteLine("reachable size: " + reachable.Count);

            foreach (Vector2 pos in reachable)
            {
                foreach (Unit enemy in (team == Teams.Blue ? redUnits : blueUnits))
                {
                    if (!enemy.getDead() && enemy.getPosition() == pos)
                    {
                        attackable.Add(enemy);
                    }
                }
            }

            //Console.WriteLine("attackable size: " + attackable.Count);

            foreach (Unit enemy in attackable)
            {

                PathSprite square = new PathSprite(enemy.getPosition(), this);
                enemies.Add(square);
            }

            //Console.WriteLine("enemies size: " + enemies.Count);
        }


        public void attack(ref Unit enemy)
        {
            Random rand = new Random();
            int unitHit = rand.Next(1, 11);
            int enemyHit = rand.Next(1, 11);

            if (!enemy.getType().Equals("mage") && !enemy.getType().Contains("genMage") && !enemy.getType().Contains("MageGen"))
            {

                if (unitHit <= activeUnit.getSkill())
                {
                    Console.WriteLine("ATTACK");
                    enemy.setHealth(enemy.getHealth() - (activeUnit.getStr() - enemy.getDef()));
                }

                if (enemy.getHealth() > 0)
                {
                    if (enemyHit <= enemy.getSkill())
                    {
                        Console.WriteLine("COUNTERATTACK");
                        activeUnit.setHealth(activeUnit.getHealth() - (enemy.getStr() - activeUnit.getDef()));
                    }
                }
            }


            if (enemy.getType().Equals("mage") || enemy.getType().Equals("genMage"))
            {
                if (unitHit <= activeUnit.getSkill())
                {
                    enemy.setHealth(activeUnit.getHealth() - (activeUnit.getStr() - enemy.getMDef()));
                }

                if (enemy.getHealth() > 0)
                {
                    if (enemyHit <= enemy.getSkill())
                    {
                        activeUnit.setHealth(activeUnit.getHealth() - (enemy.getStr() - activeUnit.getMDef()));
                    }
                }
            }

            if (enemy.getHealth() <= 0)
            {
                enemy.setDead(true);
                enemy.setUsable(false);
                (enemy.getTeam() == Teams.Blue ? blueMorale : redMorale).Morale--;

                if (enemy.getTeam() == Teams.Blue)
                {
                    map[(int)enemy.getPosition().X / 64, (int)enemy.getPosition().Y / 64].setBlueOcc(false);
                }
                else
                {

                    map[(int)enemy.getPosition().X / 64, (int)enemy.getPosition().Y / 64].setRedOcc(false);
                }
            }

            if (activeUnit.getHealth() <= 0)
            {
                activeUnit.setDead(true);
                activeUnit.setUsable(false);
                (activeUnit.getTeam() == Teams.Blue ? blueMorale : redMorale).Morale--;
                if (activeUnit.getTeam() == Teams.Blue)
                {
                    map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setBlueOcc(false);
                }
                else
                {

                    map[(int)activeUnit.getPosition().X / 64, (int)activeUnit.getPosition().Y / 64].setRedOcc(false);
                }
                //morale
            }
            attackable.Clear();
            enemies.Clear();
            isUnitAttacking = false;
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
                if (check.getimpassable())
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

        public Unit unitToAttack()
        {
            Unit ret = null;
            foreach (Unit enemy in attackable)
            {
                if (enemy.getPosition() == cursorPosition)
                {
                    ret = enemy;
                }
            }
            return ret;
        }

        private bool CanAttackEnemy()
        {
            foreach (Unit enemy in attackable)
            {
                if (enemy.getPosition() == cursorPosition)
                {
                    return true;
                }
            }
            return false;
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

        private bool MoveCursor(KeyboardState oldState, KeyboardState newState, ref Vector2 cursorPos)
        {
            if (oldState.IsKeyUp(Keys.Left) && newState.IsKeyDown(Keys.Left) && cursorPos.X > 0)
            {
                cursorPos.X -= 64;
                return true;
            }

            if (oldState.IsKeyUp(Keys.Right) && newState.IsKeyDown(Keys.Right) && cursorPos.X < 23 * 64)
            {
                cursorPos.X += 64;
                return true;
            }

            if (oldState.IsKeyUp(Keys.Down) && newState.IsKeyDown(Keys.Down) && cursorPos.Y < 13 * 64)
            {
                cursorPos.Y += 64;
                return true;
            }
            if (oldState.IsKeyUp(Keys.Up) && newState.IsKeyDown(Keys.Up) && cursorPos.Y > 0)
            {
                cursorPos.Y -= 64;
                return true;
            }
            return false;
        }

        private void DetectUnitHovered()
        {
            //See if cursor is over any unit
            hoveredUnit = blueUnits.Concat(redUnits).FirstOrDefault(unit => unit.getPixelPosition() == cursorPosition);
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
                if (nextUnit.getUsable())
                {
                    //Move cursor to next unit
                    cursorPosition = nextUnit.getPosition();
                    DetectUnitHovered();
                }
            }
            //If there are no more usable units
            else
            {
                EndTurn();
            }
            activeUnit = null;
        }

        public Unit redGeneralChoice()
        {
            string compChoice = "";
            Random genNum = new Random();
            int compGen = genNum.Next(0, 5);
            if (compGen == 0)
            {
                compChoice = "blueMageGen";
            }
            if (compGen == 1)
            {
                compChoice = "blueArcherGen";
            }
            if (compGen == 2)
            {
                compChoice = "bluePikeGen";
            }
            if (compGen == 3)
            {
                compChoice = "blueSwordGen";
            }
            if (compGen == 4)
            {
                compChoice = "blueWarriorGen";
            }

            Unit generalR = new Unit(compChoice);
            generalR.setType("red" + compChoice.Substring(4));
            generalR.setPosition(generalRPosition);
            return generalR;
        }

        private void EndTurn()
        {
            //Move cursor to other team's unit
            cursorPosition = (activeTeam == Teams.Blue ? redUnits : blueUnits).First().getPosition();
            DetectUnitHovered();
            //Reset each unit in current team
            foreach (Unit unit in (activeTeam == Teams.Blue ? blueUnits : redUnits))
            {
                unit.setHasActed(false);
                unit.setHasMoved(false);
                unit.setUsable(true);
            }
            //Other team's turn
            activeTeam = (activeTeam == Teams.Blue ? Teams.Red : Teams.Blue);
        }
    }
}
