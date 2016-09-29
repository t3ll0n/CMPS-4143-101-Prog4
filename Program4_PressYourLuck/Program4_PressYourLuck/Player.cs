using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program4_PressYourLuck
{
    class Player : IPlayer
    {
        public Player()
        {
            cash = 0;
            spins = 0;
        }

        public void updateCash(int x)
        {
            //if player got a whammy
            if (x == 0)
            {
                cash = 0;
            }
            else
            {
                cash += x;
            }
            
        }

        public void updateSpins(int x)
        {
            spins += x;
        }

        public int getCash()
        {
            return cash;
        }

        public int getSpins()
        {
            return spins;
        }

        //variables
        private int cash;
        private int spins;
    }
}
