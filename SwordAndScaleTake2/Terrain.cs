using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordandScale
{
    class Terrain
    {
        bool isRiver;
        bool blueOccupied;
        bool redOccupied;
        bool owner;
        bool isInteractable;
        Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Terrain(int x, int y)
        {
            isRiver = false;
            blueOccupied = false;
            redOccupied = false;
            owner = false;
            isInteractable = false;
            position = new Vector2(x*64 , y*64);

        }

        public Terrain(bool isInteractable, bool redOccupied, bool owner)
        {
            this.owner = owner;
            this.redOccupied = redOccupied;
            this.blueOccupied = !redOccupied;
            this.isInteractable = isInteractable;
        }


        public bool getRiver()
        {
            return this.isRiver;
        }

        public void setRiver(bool isRiver)
        {
            this.isRiver = isRiver;
        }

        public bool getInteractable()
        {
            return this.isInteractable;
        }

        public void setInteractable(bool isInteractable)
        {
            this.isInteractable = isInteractable;
        }

        public bool getBlueOcc()
        {
            return this.blueOccupied;
        }

        public void setBlueOcc(bool blueOccupied)
        {
            this.blueOccupied = blueOccupied;
        }

        public bool getRedOcc()
        {
            return this.redOccupied;
        }

        public void setRedOcc(bool redOccupied)
        {
            this.redOccupied = redOccupied;
        }

        public bool getOwner()
        {
            return this.owner;
        }

        public void animate(int x, int y)
        {

        }

    }
}
