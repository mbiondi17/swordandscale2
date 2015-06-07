using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    class UnitActionPane
    {
        Texture2D background;
        SpriteFont courierNew; //doesn't support unicode
        Unit unit;
        int x, y;
        bool visible;

        public UnitActionPane()
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
                spriteBatch.Draw(background, new Rectangle(x, y, 124, 124), Color.White);
                spriteBatch.DrawString(courierNew, "Attack"  , new Vector2(x + 5, y + 0),  Color.Black);
                spriteBatch.DrawString(courierNew, "Move"    , new Vector2(x + 5, y + 32), Color.Black);
                spriteBatch.DrawString(courierNew, "Interact", new Vector2(x + 5, y + 64), Color.Black);
                spriteBatch.DrawString(courierNew, "Wait"    , new Vector2(x + 5, y + 96), Color.Black);
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
