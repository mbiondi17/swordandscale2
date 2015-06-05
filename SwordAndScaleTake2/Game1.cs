using System.Collections.Generic;

using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    
    public class Game1
    {
        Texture2D mapImage;
        Unit exampleUnit;
        List<Unit> exampleUnitList = new List<Unit>();
        UnitInfoPane unitInfo;

        public Game1()
        {
            exampleUnit = new Unit("blueArcher", "archer", 6, 9, 2, 4, 6, true);
            exampleUnitList.Add(exampleUnit);
            unitInfo = new UnitInfoPane();
        }

        public void LoadContent(ContentManager content)
        {
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
    }
}
