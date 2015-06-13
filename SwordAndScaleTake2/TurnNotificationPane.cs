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
    class TurnNotificationPane
    {
        struct Notification
        {
            public Vector2 position;
            public string text;
            public int timer;
            public SoundEffect sound;
            public Color color;
        }
        Notification currentNotification; //needed because working with List of structs, 
        //you can't update timer in a struct without replacing it with a whole new struct
        SpriteFont courierTwo; //doesn't support unicode
        List<Notification> notificationList = new List<Notification>();

        public void AddToQueue(float x, float y, string text, int framesToDisplay, Color color, SoundEffect toPlay = null)
        {
            Notification newNotification;
            newNotification.text = text;
            newNotification.position = new Vector2(x, y);
            newNotification.timer = framesToDisplay;
            newNotification.sound = toPlay;
            newNotification.color = color;
            notificationList.Add(newNotification);
            Update(); //jumpstart
        }

        public void AddToQueue(Vector2 position, string text, int framesToDisplay, Color color, SoundEffect toPlay = null)
        {
            Notification newNotification;
            newNotification.text = text;
            newNotification.position = position;
            newNotification.timer = framesToDisplay;
            newNotification.sound = toPlay;
            newNotification.color = color;
            notificationList.Add(newNotification);
            Update(); //jumpstart
        }


        public void LoadContent(ContentManager content)
        {
            courierTwo = content.Load<SpriteFont>("Courier Two");
        }

        public void UnloadContent()
        {

        }

        public void Update()
        {
            if (currentNotification.timer > 0)
            {
                currentNotification.timer--;
            }
            if (notificationList.Count > 0 && currentNotification.timer == 0)
            {
                currentNotification = notificationList[0];
                currentNotification.sound.Play();
                notificationList.RemoveAt(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentNotification.timer > 0)
            {
                spriteBatch.DrawString(courierTwo, currentNotification.text, currentNotification.position + new Vector2(66, 2), currentNotification.color);
                //spriteBatch.DrawString(courierNew, currentNotification.text, currentNotification.position + new Vector2(65, 1), Color.Black);
                //spriteBatch.DrawString(courierNew, currentNotification.text, currentNotification.position + new Vector2(64, 0), Color.White);
            }
        }
    }
}