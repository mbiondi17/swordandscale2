using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    class UnitInfoPane
    {
        Texture2D background;
        SpriteFont courierNew;
        Unit unit;
        int x, y;
        bool visible;

        public UnitInfoPane()
        {

        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background-paper");
            courierNew = content.Load<SpriteFont>("Courier New");
        }

        public void UnloadContent()
        {

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(visible)
            {
                spriteBatch.Draw(background, new Rectangle(x, y, 320, 320), Color.White);
                unit.DrawAtPixel(spriteBatch, x + 30, y + 5);
                spriteBatch.DrawString(courierNew,                     unit.getType(),   new Vector2(x + 124, y + 18), Color.Black);
                spriteBatch.DrawString(courierNew, "Health: "        + unit.getHealth(), new Vector2(x + 30, y + 74), Color.Black);
                spriteBatch.DrawString(courierNew, "Strength: "      + unit.getStr(),    new Vector2(x + 30, y + 114), Color.Black);
                spriteBatch.DrawString(courierNew, "Skill: "         + unit.getSkill(),  new Vector2(x + 30, y + 154),  Color.Black);
                spriteBatch.DrawString(courierNew, "Defense: "       + unit.getDef(),    new Vector2(x + 30, y + 194), Color.Black);
                spriteBatch.DrawString(courierNew, "Magic Defense: " + unit.getMDef(),   new Vector2(x + 30, y + 234), Color.Black);
                spriteBatch.DrawString(courierNew, "Movement: "      + unit.getMvmt(),   new Vector2(x + 30, y + 274), Color.Black);
            }
        }

        public void setPixelPosition(Unit unit, int x, int y)
        {
            this.unit = unit;
            this.x = x;
            this.y = y;
        }
        public void setPixelPosition(Unit unit, Vector2 vector)
        {
            this.unit = unit;
            this.x = (int)vector.X;
            this.y = (int)vector.Y;
        }

        public void Show()
        {
            visible = true;
        }

        public void Hide()
        {
            visible = false;
        }

        public bool IsVisible()
        {
            return visible;
        }
    }
}
