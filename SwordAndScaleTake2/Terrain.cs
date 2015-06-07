using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    public class Terrain
    {
        bool impassible;
        bool blueOccupied;
        bool redOccupied;
        bool owner;
        public bool isInteractable;
        Vector2 position;

        public Terrain(int x, int y)
        {
            this.position.X = x * 64;
            this.position.Y = y * 64;
            impassible = false;
            blueOccupied = false;
            redOccupied = false;
            owner = false;
            isInteractable = false;

        }

        public Terrain(bool isInteractable, bool redOccupied, bool owner)
        {
            this.owner = owner;
            this.redOccupied = redOccupied;
            this.blueOccupied = !redOccupied;
            this.isInteractable = isInteractable;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public bool getImpassible()
        {
            return this.impassible;
        }

        public void setImpassible(bool impassible)
        {
            this.impassible = impassible;
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
    }
}
