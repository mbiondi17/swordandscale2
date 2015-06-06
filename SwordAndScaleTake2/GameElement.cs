using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwordAndScaleTake2

{
    public class GameElement
    {
        //going to need a texture
        private Texture2D texture;

        //for the area
        private Rectangle rect;

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
        public GameElement(string assetName)
        {
            this.assetName = assetName;
        }

        public virtual void LoadContent(ContentManager content)
        {
            //load a texture
            texture = content.Load<Texture2D>(assetName);
            //make a rectangle the size of the texture
            rect = new Rectangle(0, 0, texture.Width, texture.Height);

        }

        public virtual void Update()
        {
            if (containsPixel(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(assetName);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public void DrawAtPixel(SpriteBatch spriteBatch, int x, int y)
        {
            Rectangle temp = rect;
            temp.Offset(x - temp.X, y - temp.Y);
            spriteBatch.Draw(texture, temp, Color.White);
        }

        /*public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - (this.GUITexture.Width / 2), (height / 2) - (this.GUITexture.Height / 2), this.GUITexture.Width, this.GUITexture.Height);
        }*/

        public void setPixelPosition(int x, int y)
        {
            rect = new Rectangle(rect.X += x, rect.Y += y, rect.Width, rect.Height);
        }

        public void setPixelPosition(Vector2 position)
        {
            rect = new Rectangle(rect.X += (int)position.X, rect.Y += (int)position.Y, rect.Width, rect.Height);
        }

        public Vector2 getPixelPosition()
        {
            return new Vector2(rect.X, rect.Y);
        }

        public bool containsPixel(int x, int y)
        {
            return rect.Contains(x, y);
        }
    }
}
