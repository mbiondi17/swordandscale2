using System.Collections.Generic;

using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    
    public class Game1
    {
        Terrain[,] map;
        Texture2D mapImage;
        Unit exampleUnit;
        List<Unit> exampleUnitList = new List<Unit>();
        UnitInfoPane unitInfo;
        Vector2 cursorPosition;
        KeyboardState pressedKey;
        KeyboardState oldState;

        public Game1()
        {
            exampleUnit = new Unit("blueArcher", "archer", 6, 9, 2, 4, 6, true);
            exampleUnitList.Add(exampleUnit);
            unitInfo = new UnitInfoPane();
        }

        public void LoadContent(ContentManager content)
        {
            loadMap();
            cursorPosition = new Vector2(0, 0);
            mapImage = content.Load<Texture2D>("alphamap");

            foreach(Unit unit in exampleUnitList)
            {
                unit.LoadContent(content);
                unit.clickEvent += UnitClicked;            
        }
            exampleUnit.setPosition(4, 4);

            unitInfo.LoadContent(content);
        }

        public void UnloadContent()
        {

        }

        public void Update()
        {
            exampleUnit.Update();
            unitInfo.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.B))
                if (unitInfo.IsVisible())
                    unitInfo.Hide();

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapImage, new Rectangle(0, 0, 1536, 896), Color.White);
            exampleUnit.Draw(spriteBatch);
            unitInfo.Draw(spriteBatch);
        }

        public void UnitClicked(string unitType, int x, int y)
        {
            unitInfo.setPixelPosition(exampleUnit, x + 64, y);
            unitInfo.Show();
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
