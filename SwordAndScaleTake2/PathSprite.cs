using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    class PathSprite
    {
        Vector2 position;
        Texture2D blue;
        Game1 game;

        public PathSprite(Game1 game)
        {
            position = new Vector2();
            this.game = game;
        }

        public PathSprite(Vector2 currentPosition, Game1 game)
        {
            position = currentPosition;
            this.game = game;
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void LoadContent()
        {
            blue = game.Content.Load<Texture2D>("blanks");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blue, position, Color.White);
        }
    }
}
