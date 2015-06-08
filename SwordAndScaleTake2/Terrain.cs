using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

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

        
        public void Draw(SpriteBatch spriteBatch, Texture2D fire, Texture2D gate, Texture2D redCastle, Texture2D blueCastle, Texture2D poison, SoundEffect cow, SoundEffect burn, SoundEffect castle, SoundEffect river) 
        {

            if (!this.isInteractable)
            {
                //red houses
               if( this.getPosition() == new Vector2(3*64, 4*64)    || 
			        this.getPosition() == new Vector2(1*64, 12*64)  ||
			        this.getPosition() == new Vector2(6*64, 12*64)  ||
			        this.getPosition() == new Vector2(9*64, 10*64)  )
		    {
			    spriteBatch.Draw(fire, this.getPosition(), Color.White);
		    }

		       //red livestock
		        else if( this.getPosition() == new Vector2(5*64, 8*64)  ||
			        this.getPosition() == new Vector2(1*64, 6*64)       )
		        {
                    spriteBatch.Draw(gate, this.getPosition(), Color.White);
		        }

		        //red fields
		        else if( this.getPosition() ==new Vector2(0*64, 4*64)   ||
			        this.getPosition() == new Vector2(1*64, 10*64)      ||
			        this.getPosition() == new Vector2(4*64, 12*64)      )
		        {
                    spriteBatch.Draw(fire, this.getPosition(), Color.White);
		        }

		        //red poisonable river
		        else if( this.getPosition() == new Vector2(3*64, 0*64)  ||
				        this.getPosition() == new Vector2(4*64, 0*64)   ||
				        this.getPosition() == new Vector2(5*64, 0*64)   ||
				        this.getPosition() == new Vector2(3*64, 2*64)   ||
				        this.getPosition() == new Vector2(4*64, 2*64)   ||
				        this.getPosition() == new Vector2(5*64, 2*64)   )
		        {
                    spriteBatch.Draw(poison, new Vector2(3*64, 1*64), Color.White);
                    spriteBatch.Draw(poison, new Vector2(4 * 64, 1 * 64), Color.White);
                    spriteBatch.Draw(poison, new Vector2(5 * 64, 1 * 64), Color.White);
		        }

		        //red castle
		        else if(this.getPosition() == new Vector2(1*64, 1*64)   ||
			        this.getPosition() == new Vector2(2*64, 1*64)       ||
			        this.getPosition() == new Vector2(1*64, 2*64)       ||
			        this.getPosition() == new Vector2(2*64, 2*64)       )
		        {
                    spriteBatch.Draw(blueCastle, new Vector2(64,64), Color.White);
		        }

		        //blue houses
		        else if( this.getPosition() == new Vector2(12*64, 1*64) || 
			        this.getPosition() == new Vector2(17*64, 2*64)      ||
			        this.getPosition() == new Vector2(17*64, 6*64)      ||
			        this.getPosition() == new Vector2(19*64, 8*64)      )
		        {
                    spriteBatch.Draw(fire, this.getPosition(), Color.White);
		        }

		        //blue livestock
		        else if( this.getPosition() == new Vector2(14*64, 3*64) ||
			        this.getPosition() == new Vector2(22*64, 5*64)      ) 
		        {
                    spriteBatch.Draw(gate, this.getPosition(), Color.White);
		        }

		        //blue fields
		        else if( this.getPosition() == new Vector2(23*64, 8*64) ||
			        this.getPosition() == new Vector2(22*64, 2*64)      ||
			        this.getPosition() == new Vector2(19*64, 1*64)      )
		        {
                    spriteBatch.Draw(fire, this.getPosition(), Color.White);
		        }

		        //blue poisonable river
		        else if( this.getPosition() == new Vector2(18*64, 9*64) ||
				         this.getPosition() == new Vector2(19*64, 9*64) ||
			 	         this.getPosition() == new Vector2(20*64, 9*64) ||
			  	         this.getPosition() == new Vector2(18*64, 11*64)||
			   	         this.getPosition() == new Vector2(20*64, 11*64) )
		        {
                    spriteBatch.Draw(poison, new Vector2(18 * 64, 10 * 64), Color.White);
                    spriteBatch.Draw(poison, new Vector2(19 * 64, 10 * 64), Color.White);
                    spriteBatch.Draw(poison, new Vector2(20 * 64, 10 * 64), Color.White);
		        }

		        //blue castle
		        else if(this.getPosition() == new Vector2(21*64, 10*64) ||
			        this.getPosition() == new Vector2(22*64, 10*64)     ||
			        this.getPosition() == new Vector2(21*64, 11*64)     ||
			        this.getPosition() == new Vector2(22*64, 11*64)      )
		        {
                    spriteBatch.Draw(redCastle, new Vector2(21, 10), Color.White);
		        }

		        else
		        {

		        }
	        }
        }
    }
}
