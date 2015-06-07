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
        SpriteFont courierNew; //doesn't support unicode
        Unit unit;
        int x, y;

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background-paper");
            courierNew = content.Load<SpriteFont>("Courier New");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(x, y, 576, 128), Color.White);
            if(unit != null)
            { 
                unit.DrawAtPixel(spriteBatch, x + 32, y + 32);
                //spriteBatch.DrawString(courierNew,                     unit.AssetName,   new Vector2(x + 124, y + 18), Color.Black);
                spriteBatch.DrawString(courierNew, "Health: "     + unit.getHealth(), new Vector2(x + 128, y + 10), Color.Black);
                spriteBatch.DrawString(courierNew, "Strength: "   + unit.getStr(),    new Vector2(x + 128, y + 42), Color.Black);
                spriteBatch.DrawString(courierNew, "Skill: "      + unit.getSkill(),  new Vector2(x + 128, y + 74), Color.Black);
                spriteBatch.DrawString(courierNew, "Defense: "    + unit.getDef(),    new Vector2(x + 330, y + 10), Color.Black);
                spriteBatch.DrawString(courierNew, "M. Defense: " + unit.getMDef(),   new Vector2(x + 330, y + 42), Color.Black);
                spriteBatch.DrawString(courierNew, "Movement: "   + unit.getMvmt(),   new Vector2(x + 330, y + 74), Color.Black);
            }
        }

        public void setUnit(Unit unit)
        {
            this.unit = unit;
        }

        public void setPixelPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void setPixelPosition(Vector2 vector)
        {
            this.x = (int)vector.X;
            this.y = (int)vector.Y;
        }
    }
}
