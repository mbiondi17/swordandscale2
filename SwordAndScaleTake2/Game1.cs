using Microsoft.Xna.Framework; //Tried to avoid using this but I need the Rectangle struct. TODO: find a work around
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordAndScaleTake2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1
    {
        Texture2D mapImage;

        public Game1()
        {

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            mapImage = content.Load<Texture2D>("alphamap");
            // TODO: use content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {

            // TODO: Add your update logic here
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapImage, new Rectangle(0, 0, 1536, 896), Color.White);
            // TODO: Add your drawing code here
        }
    }
}
