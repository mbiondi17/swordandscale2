using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    class combatNotificationPane
    {
        SpriteFont courierNew; //doesn't support unicode
        float x, y;
        bool visible;
        string display;
        int timer;

        public combatNotificationPane()
        {
        
        }

        public combatNotificationPane (float x, float y, string display, int framesToDisplay)
        {
            this.display = display;
            this.x = x;
            this.y = y;
            this.timer = framesToDisplay; 
        }

        public void LoadContent(ContentManager content)
        {
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
                spriteBatch.DrawString(courierNew, display , new Vector2(x, y + 64),  Color.White);
                spriteBatch.DrawString(courierNew, display, new Vector2(x + 1, y + 63), Color.Black);
                spriteBatch.DrawString(courierNew, display, new Vector2(x + 1, y + 62), Color.Black);
            }
            if (timer == 0)
            {
                this.visible = false;
            }

            timer--;
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
