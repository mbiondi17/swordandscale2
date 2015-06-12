using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    class Square : IEquatable<Square>
    {
        Vector2 position;
        Vector2 parentPosition;
        bool Start;
        int cost;

        public Square(Vector2 position, Vector2 parentPosition)
        {
            this.position = position;
            this.parentPosition = parentPosition;
            Start = false;
        }

        public Square(bool isStart, Vector2 parentPosition)
        {
            this.position = parentPosition;
            this.parentPosition = parentPosition;
            Start = true;
        }

        public Vector2 getParentPosition()
        {
            return parentPosition;
        }

        public void setParentPosition(Vector2 parentPosition)
        {
            this.parentPosition = parentPosition;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public int getCost()
        {
            return cost;
        }

        public void setCost(int cost)
        {
            this.cost = cost;
        }

        public bool Equals(Square compare)
        {
            return compare.position == this.position;
        }

        public bool isStart()
        {
            return Start;
        }
    }
}
