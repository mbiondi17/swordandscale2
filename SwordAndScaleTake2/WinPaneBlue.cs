using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace SwordAndScaleTake2
{
    class WinPaneBlue
    {

        SpriteFont courierTwo; //doesn't support unicode
        SpriteFont courierNew; //also doesn't support unicode
        public int timer;



        public void LoadContent(ContentManager content)
        {
            courierTwo = content.Load<SpriteFont>("Courier Two");
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
            spriteBatch.DrawString(courierTwo, "The    Blue    Team    is    Victorious!", new Vector2(192, 320), Color.Blue);
            spriteBatch.DrawString(courierNew, "Press  Escape  to  Exit  Game", new Vector2(576, 448), Color.Blue);
        }
    }
}