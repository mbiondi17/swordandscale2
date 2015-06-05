using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    class Unit : GameElement
    {
        string type;
        int health;
        int str;
        int skill;
        int mvmt;
        int def;
        int mDef;
        bool team;
        bool isDead;
        bool isUsable;
        bool hasMoved;
        bool hasActed;
        bool inRiver;
        bool wasInRiver;
        int xpos;
        int ypos;

        //public delegate void UnitClicked(object unit);

        //public event UnitClicked unitClicked;

        public Unit(string textureName)
            : base(textureName)
        {
            isDead = false;
            isUsable = true;
            hasMoved = false;
            hasActed = false;
        }

        public Unit(string textureName, string type, int str, int skill, int def, int mDef, int mvmt, bool team) : base(textureName)
        {
            this.type = type;
            this.health = 10;
            this.str = str;
            this.skill = skill;
            this.def = def;
            this.mDef = mDef;
            this.mvmt = mvmt;
            this.isDead = false;
            this.isUsable = false;
            this.hasMoved = false;
            this.hasActed = false;
            this.inRiver = false;
            this.wasInRiver = false;
            this.xpos = 0;
            this.ypos = 0;
            this.team = team;
        }

        public struct option {
            public Terrain space;
            public int priority;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public string getType()
        {
            return this.type;
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

        public bool getDead()
        {
            return this.isDead;
        }

        public void setDead(bool isDead)
        {
            this.isDead = isDead;
        }

        public bool getUsable()
        {
            return this.isUsable;
        }

        public void setUsable(bool isUsable)
        {
            this.isUsable = isUsable;
        }

        public bool getHasMoved()
        {
            return this.hasMoved;
        }

        public void setHasMoved(bool hasMoved)
        {
            this.hasMoved = hasMoved;
        }

        public bool getHasActed()
        {
            return this.hasActed;
        }

        public void setHasActed(bool hasActed)
        {
            this.hasActed = hasActed;
        }

        public bool getInRiver()
        {
            return this.inRiver;
        }

        public void setInRiver(bool inRiver)
        {
            this.inRiver = inRiver;
        }

        public bool getWasInRiver()
        {
            return this.wasInRiver;
        }

        public void setWasInRiver(bool wasInRiver)
        {
            this.wasInRiver = wasInRiver;
        }

        public int getX()
        {
            return this.xpos;
        }

        public int getY()
        {
            return this.ypos;

        }

        public void setPosition(int x, int y)
        {
            this.xpos = x;
            this.ypos = y;
            this.setPixelPosition(x * 64, y * 64);
        }

        /*public void move(int x, int y,  ref Terrain[,] map) {
	        if(((x - this.xpos) + (y - this.ypos)) <= this.mvmt) {

		        if(this.team) {
			        if(!map[x,y].getRedOcc() && !map[x,y].getRedOcc()){
				        List<Square> path = pathfinder(x, y, ref map);
				        for(int i = path.Count - 1; i >= 0; i--) {
                            //Console.WriteLine("path space: " + i);
					        this.xpos = path[i].x;
					        this.ypos = path[i].y;

                            //Console.WriteLine("Space: " + this.xpos + ", " + this.ypos);
				        }
			        }
			        map[this.xpos, this.ypos].setBlueOcc(true);
                    this.hasMoved = true;
                    //Console.WriteLine("end space: " + this.xpos + " _" + this.ypos);

		        }
		        if(!this.team) {
			        if(!map[x,y].getBlueOcc() && !map[x,y].getRedOcc()){ 
				        List<Square> path = pathfinder(x, y, ref map);
                        for (int i = path.Count - 1; i >= 0; i--)
                        {
                            //Console.WriteLine("path space: " + i);
					        this.xpos = path[i].x;
                            this.ypos = path[i].y;
                            //Console.WriteLine("Space: " + this.xpos + ", " + this.ypos);
				        }
			        }
			        map[this.xpos, this.ypos].setRedOcc(true);
                    this.hasMoved = true;
                    //Console.WriteLine("end space: " + this.xpos + " _" + this.ypos);
		        }
	        }
        }*/       

//member function of unit
//dictates attack logic
//missing feedback. Should display messages:
//"Unit hit for [x] damage! Enemy has [H1] health left!" or "Unit missed! Enemy still has [H1] health!"
//"Enemy hit for [y] damage! Unit has [H2] health left!" or "Enemy missed! Unit still has [H2] health"

        public void attack(Unit enemy) {
	        Random rand = new Random();
	        int unitHit = rand.Next(1, 11);
	        int enemyHit = rand.Next(1, 11);

	        if(enemy.type != "mage" && enemy.type != "genMage") {

		        if(unitHit <= this.skill) {
			        enemy.health -= (this.str - enemy.def);
		        }

		        if(enemy.health > 0) {
			        if(enemyHit <= enemy.skill) {
				        this.health -= (enemy.str - this.def);
			        }
		        }
	        }


	        if(enemy.type == "mage" || enemy.type == "genMage") {
		        if(unitHit <= this.skill) {
			        enemy.health -= (this.str - enemy.mDef);
		        }

		        if(enemy.health > 0) {
			        if(enemyHit <= enemy.skill) {
				        this.health -= (enemy.str - this.mDef);
			        }
		        }
	        }
        }




        /*public void interact(Terrain square, ref Player player)
        {
            if (this.xpos == square.getX() && this.ypos == square.getY() && square.getInteractable())
            {
                player.setMorale(player.getMorale() - 1);
                square.setInteractable(false);
            }

            //add fire pictures and things
        }*/

        public void endTurn()
        {
            this.isUsable = false;
        }

        /*public List<Square> pathfinder(int x, int y, ref Terrain[,] map)
        {
            int distance = Math.Abs(x - this.xpos) + Math.Abs(y - this.ypos);
            Square start = new Square(this.xpos, this.ypos, 0, distance, null);
            Square end = new Square(x, y, distance, 0, null);
            Heap open = new Heap();
            List<Square> closed = new List<Square>();
            List<Square> moves = new List<Square>();

            Square current = start;
            Square next = null; //change for uncertainty
            open.push(current);

            while (open.peek() != null)
            {
                current = open.pop();
                //Console.WriteLine("score :" + current.getScore());
                //Console.WriteLine(current.x + ", " + current.y + " : " + end.x + ", " + end.y);
                if (current.equals(end))
                {
                    break;
                }

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        int ptr = current.x;
                        int ptc = current.y;

                        if (!(i == 0 && j == 0) && (i == 0 || j == 0) &&
                                (i + ptr >= 0) && (i + ptr < 24) &&
                                (j + ptc >= 0) && (j + ptc < 14))
                        {
                            //Console.WriteLine("made it: " + i + ", " + j);

                            next = new Square(ptr + i, ptc + j, 0, 0, current);
                            next.fs = current.fs + 1;
                            next.tf = Math.Abs(next.x - end.x) + Math.Abs(next.y - end.y);
                            next.score = next.fs + next.tf;
                            //change for uncertainty
                            //TODO if next is occupied, continue
                            if (map[next.x, next.y].getBlueOcc() && !this.team || map[next.x, next.y].getRedOcc() && this.team)
                            {
                                continue;
                            }

                            if (open.contains(next) || closed.Contains(next))
                            {

                            }

                            else
                            {
                                //Console.WriteLine("will add");
                                open.push(next);
                                //Console.WriteLine("added\n");
                            }
                        }
                    }
                }
                closed.Add(current);
            }
            moves.Add(current);
            Square tmp;
            while (current.last != null)
            {
                tmp = current.last;
                moves.Add(tmp);
                //Console.WriteLine(tmp.x);
                //Console.WriteLine(tmp.y);
                current = current.last;
            }
            return moves;
        }

        public Terrain SPACEfinder(ref Terrain[,] map)
        {
            Terrain corresponding = null;
            List<option> open = new List<option>();
            int i = 0;
            for(i = 0; i < 24; i++)
            {
                for(int j = 0; j < 14; j++)
                {
                    Terrain current = map[i, j];
                    if ((current.getOwner() != this.team) && current.getInteractable() || (team && current.getRedOcc()) || 
                        (!team && current.getBlueOcc()))
                    {
                        option moveable = new option();
                        moveable.space = current;
                        moveable.priority = 0;
                        if (current.getInteractable())
                        {
                            moveable.priority += 5;
                            if ((team && current.getRedOcc()) || (!team && current.getBlueOcc()))
                            {
                                moveable.priority += 3;
                            }
                        }
                        moveable.priority -= Math.Abs(current.getX() - this.xpos) + Math.Abs(current.getY() - this.ypos);
                        open.Add(moveable);
                    }
                }
            }
            for (i = 0; i < open.Count; i++)
                {
                    int best = -10000;
                    if (open[i].priority > best)
                    {
                        best = open[i].priority;
                        corresponding = open[i].space;
                    }

                }
                return corresponding;
            }*/
            //if terrain in range, go to it and burn it
            //if enemy in range, go to it and fight it
            //if neither in range, move to nearest terrain
    }
}
