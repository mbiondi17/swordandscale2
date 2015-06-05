using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    public class Unit
    {
        string type;
        int health;
        int str;
        int skill;
        int mvmt;
        int def;
        int mDef;
        bool team;
        bool dead;
        bool usable;
        bool hasMoved;
        bool hasActed;
        Vector2 position;

        public Unit(string general)
        {
            Vector2 pos = new Vector2(22 * 64, 11 * 64);
            switch (general)
            {
                case "blueArcherGen":
                    this.position = pos;
                    this.health = 15;
                    this.str = 8;
                    this.skill = 9;
                    this.def = 3;
                    this.mDef = 4;
                    this.mvmt = 5;
                    this.team = true;
                    this.dead = false;
                    this.usable = true;
                    this.hasMoved = false;
                    this.hasActed = false;
                    break;
                case "blueMageGen":
                    this.position = pos;
                    this.health = 15;
                    this.str = 8;
                    this.skill = 9;
                    this.def = 2;
                    this.mDef = 5;
                    this.mvmt = 4;
                    this.team = true;
                    this.dead = false;
                    this.usable = true;
                    this.hasMoved = false;
                    this.hasActed = false;
                    break;
                case "bluePikeGen":
                    this.position = pos;
                    this.health = 15;
                    this.str = 9;
                    this.skill = 7;
                    this.def = 5;
                    this.mDef = 2;
                    this.mvmt = 3;
                    this.team = true;
                    this.dead = false;
                    this.usable = true;
                    this.hasMoved = false;
                    this.hasActed = false;
                    break;
                case "blueSwordGen":
                    this.position = pos;
                    this.health = 15;
                    this.str = 9;
                    this.skill = 9;
                    this.def = 2;
                    this.mDef = 4;
                    this.mvmt = 4;
                    this.team = true;
                    this.dead = false;
                    this.usable = true;
                    this.hasMoved = false;
                    this.hasActed = false;
                    break;
                case "blueWarriorGen":
                    Console.WriteLine("PICKED BLUE WARRIOR");
                    this.position = pos;
                    this.health = 15;
                    this.str = 9;
                    this.skill = 8;
                    this.def = 4;
                    this.mDef = 2;
                    this.mvmt = 3;
                    this.team = true;
                    this.dead = false;
                    this.usable = true;
                    this.hasMoved = false;
                    this.hasActed = false;
                    Console.WriteLine("Blue Pos Set");
                    break;
            }
        }

        public Unit(string type, int health, int str, int skill, int def, int mDef, int mvmt, bool team, Vector2 position)
        {
            this.type = type;
            this.health = health;
            this.str = str;
            this.skill = skill;
            this.mvmt = mvmt;
            this.def = def;
            this.mDef = mDef;
            this.team = team;
            this.position = position;
            this.dead = false;
            this.usable = true;
            this.hasMoved = false;
            this.hasActed = false;
        }

        public int getHealth()
        {
            return this.health;
        }

        public int getStr()
        {
            return this.str;
        }

        public int getSkill()
        {
            return this.skill;
        }

        public int getMvmt()
        {
            return this.mvmt;
        }

        public int getDef()
        {
            return this.def;
        }

        public int getMDef()
        {
            return this.mDef;
        }

        public bool getTeam()
        {
            return this.team;
        }

        public bool getUsable()
        {
            return this.usable;
        }

        public bool getHasMoved()
        {
            return this.hasMoved;
        }

        public bool getHasActed()
        {
            return this.hasActed;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public void setDead(bool dead)
        {
            this.dead = dead;
        }

        public void setUsable(bool usable)
        {
            this.usable = usable;
        }

        public void setHasActed(bool hasActed)
        {
            this.hasActed = hasActed;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}