using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    class MoralePane
    {
        Texture2D background;
        SpriteFont courierNew; //doesn't support unicode
        int x, y;
        int morale;

        public int Morale
        {
            get { return morale; }
            set { morale = value; }
        }

        public MoralePane(int morale)
        {
            this.morale = morale;
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background-paper");
            courierNew = content.Load<SpriteFont>("Courier New");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(x, y, 192, 128), Color.White);
            spriteBatch.DrawString(courierNew, "Morale: " + morale,   new Vector2(x + 20, y + 42), Color.Black);
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
