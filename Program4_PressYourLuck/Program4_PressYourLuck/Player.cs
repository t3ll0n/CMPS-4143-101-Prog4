using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program4_PressYourLuck
{
    class Player
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

        public int Cash
        {
            //returns the amount of cash the user has
            get
            {
                return cash;
            }

            //Sets cash to vlaue if greater than 0, else do not change cash
            set
            {
                this.cash = value < 0? this.cash:value;
            }
        }

        public int Spins
        {
            //returns number of spins
            get
            {
                return spins;
            }

            //Sets cash to vlaue if greater than 0, else do not change spins
            set
            {
                this.spins = value < 0? this.spins:value;
            }
            
        }

        //variables
        private int cash;
        private int spins;
    }
}
