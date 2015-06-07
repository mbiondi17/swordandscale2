using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    public class Terrain
    {
        bool impassable;
        bool blueOccupied;
        bool redOccupied;
        bool owner;
        public bool isInteractable;
        Vector2 position;

        public Terrain(int x, int y)
        {
            this.position.X = x * 64;
            this.position.Y = y * 64;
            impassable = false;
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

        public bool getimpassable()
        {
            return this.impassable;
        }

        public void setimpassable(bool impassable)
        {
            this.impassable = impassable;
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

        
        public void Draw()   //renamed for compatibility errors
        {

            if (!this.isInteractable)
            {
                //red houses
               if( this.getPosition() == new Vector2(3*64, 4*64)    || 
			        this.getPosition() == new Vector2(1*64, 12*64)  ||
			        this.getPosition() == new Vector2(6*64, 12*64)  ||
			        this.getPosition() == new Vector2(9*64, 10*64)  )
		    {
			
		    }

		       //red livestock
		        else if( this.getPosition() == new Vector2(5*64, 8*64)  ||
			        this.getPosition() == new Vector2(1*64, 6*64)       )
		        {
		
		        }

		        //red fields
		        else if( this.getPosition() ==new Vector2(0*64, 4*64)   ||
			        this.getPosition() == new Vector2(1*64, 10*64)      ||
			        this.getPosition() == new Vector2(4*64, 12*64)      )
		        {

		        }

		        //red poisonable river
		        else if( this.getPosition() == new Vector2(3*64, 0*64)  ||
				        this.getPosition() == new Vector2(4*64, 0*64)   ||
				        this.getPosition() == new Vector2(5*64, 0*64)   ||
				        this.getPosition() == new Vector2(3*64, 2*64)   ||
				        this.getPosition() == new Vector2(4*64, 2*64)   ||
				        this.getPosition() == new Vector2(5*64, 2*64)   )
		        {

		        }

		        //red castle
		        else if(this.getPosition() == new Vector2(1*64, 1*64)   ||
			        this.getPosition() == new Vector2(2*64, 1*64)       ||
			        this.getPosition() == new Vector2(1*64, 2*64)       ||
			        this.getPosition() == new Vector2(2*64, 2*64)       )
		        {

		        }

		        //blue houses
		        else if( this.getPosition() == new Vector2(12*64, 1*64) || 
			        this.getPosition() == new Vector2(17*64, 2*64)      ||
			        this.getPosition() == new Vector2(17*64, 6*64)      ||
			        this.getPosition() == new Vector2(19*64, 8*64)      )
		        {

		        }

		        //blue livestock
		        else if( this.getPosition() == new Vector2(14*64, 3*64) ||
			        this.getPosition() == new Vector2(22*64, 5*64)      ) 
		        {

		        }

		        //blue fields
		        else if( this.getPosition() == new Vector2(23*64, 8*64) ||
			        this.getPosition() == new Vector2(22*64, 2*64)      ||
			        this.getPosition() == new Vector2(19*64, 1*64)      )
		        {

		        }

		        //blue poisonable river
		        else if( this.getPosition() == new Vector2(18*64, 9*64) ||
				         this.getPosition() == new Vector2(19*64, 9*64) ||
			 	         this.getPosition() == new Vector2(20*64, 9*64) ||
			  	         this.getPosition() == new Vector2(18*64, 11*64)||
			   	         this.getPosition() == new Vector2(20*64, 11*64) )
		        {

		        }

		        //blue castle
		        else if(this.getPosition() == new Vector2(21*64, 10*64) ||
			        this.getPosition() == new Vector2(22*64, 10*64)     ||
			        this.getPosition() == new Vector2(21*64, 11*64)     ||
			        this.getPosition() == new Vector2(22*64, 11*64)      )
		        {

		        }

		        else
		        {

		        }
	        }
        }
    }
}
