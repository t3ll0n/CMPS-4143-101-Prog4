//Tellon Smith & Johann Redhead
//File: IPlayer.cs
//Player Interface File

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program4_PressYourLuck
{
    interface IPlayer
    {
        //Purpose: updates the player's cash
        //Requires: int, the number to increment by
        //Returns: none
        void updateCash(int x);

        //Purpose: updates the player's spins
        //Requires: int, number to spins to increment by
        //Returns: none
        void updateSpins(int x);

        //Purpose: returns the amount of cash the player has 
        //Requires: none
        //Returns: int, the amount of cash
        int getCash();

        //Purpose: returns the number of spins the player has
        //Requires: none
        //Returns: int, the number of spins
        int getSpins();
    }
}
