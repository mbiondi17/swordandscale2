using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
    //This is basically just a struct that can be passed from GameShell to Game
    //It can be expanded to hold more preferences
    public class GameInfo
    {
        public String chosenGeneral;
        public bool hasRedWon;
        public bool hasBlueWon;
    }
}
