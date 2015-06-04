﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwordAndScaleTake2

{
    class GUIElement
    {
        //going to need a texture
        private Texture2D GUITexture;

        //for the area
        private Rectangle GUIRect;

        //for the folder that holds the content
        private string assetName;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        //the menu itself
        public GUIElement(string assetName)
        {
            this.assetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            //load a texture
            GUITexture = content.Load<Texture2D>(assetName);
            //make a rectangle the size of the texture
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);

        }

        public void Update()
        {
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed
                )
            {
                clickEvent(assetName);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - (this.GUITexture.Width / 2), (height / 2) - (this.GUITexture.Height / 2), this.GUITexture.Width, this.GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
    }
}
