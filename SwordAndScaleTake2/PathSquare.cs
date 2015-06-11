using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    class PathSquare
    {
        public PathSquare last;   //last square in path
        public int x;
        public int y;
        public int fs;  //distance from start to current
        public int tf;  //distance from current to finish (Manhattan)

        public PathSquare(int x, int y, int fs, int tf, PathSquare last)
        {
            this.x = x;
            this.y = y;
            this.fs = fs;
            this.tf = tf;
            this.last = last;
        }

        public int getScore()
        {
            return this.tf + this.fs;
        }

        public bool equals(PathSquare n)
        {
            return this.x == n.x && this.y == n.y;
        }
    }
}