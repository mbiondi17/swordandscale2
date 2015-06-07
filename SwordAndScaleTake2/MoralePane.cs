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
        string color;

        public int Morale
        {
            get { return morale; }
            set { morale = value; }
        }

        public MoralePane(int morale, string color)
        {
            this.morale = morale;
            //used for coloring the text to differentiate the Morale displays!
            this.color = color;
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background-paper");
            courierNew = content.Load<SpriteFont>("Courier New");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(x, y, 192, 128), Color.White);
            //Draw the Morale Panes depending on color field (Team color)
            if (color == "red")
            {
                spriteBatch.DrawString(courierNew, "Morale: " + morale, new Vector2(x + 20, y + 42), Color.Red);
            }
            else
            {
                spriteBatch.DrawString(courierNew, "Morale: " + morale, new Vector2(x + 20, y + 42), Color.Blue);
            }
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
