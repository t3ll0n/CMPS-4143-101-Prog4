using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program4_PressYourLuck
{
    public partial class PressYourLuckGameForm : Form
    {
        int numplayers = 0;
        int numrounds = 1;
        int maxrounds = 2;
        Setup setup = new Setup(); //create instance of setup form

        Player player1 = new Player(), player2 = new Player(),
        player3 = new Player(),who; //create 3 instances for 3 players and 
        //instance to track active player

        //create instance of gameTrivia form
        GameTrivia gametrivia = new GameTrivia(); 

        public PressYourLuckGameForm()
        {
            InitializeComponent();
            MessageBox.Show("Please run the set-up and fill out all fields.");
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howToPlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Once upon a time....", "How To Play");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            playerInit();
        }

        //Purpose: initialises game with players entered in setup form
        //Requires: none
        //Returns: none
        public void playerInit()
        {
            //get number o players from set-up form
            setup.getNumPlayers(ref numplayers);

            //error checking to ensure that user went through set-up
            if(setup.File_Path == "")
            {
                MessageBox.Show("Error, please run the set-up and" +
                    " select an input file. The program will now exit");
                Application.Exit(); //close the program
            }

            //activate score menu or correspondiing number of players
            if(numplayers == 2)
            {
                player2GroupBox.Visible = true;
            }

            else if (numplayers == 3)
            {
                player2GroupBox.Visible = true;
                player3GroupBox.Visible = true;
            }
            updateCash();
            updateSpins();
            startRound();
        }

        //Purpose: starts a round in the game
        //Requires: none
        //Returns: none
        private void startRound()
        {
            if (!this.Visible)
                this.Show();

            do
            {
                pass_spins.Enabled = true;
                round_label.Text = "Round " + numrounds;
                questionPlayers();
             
                //donate spins to players if they leave 
                //trivia section without spins
                if(player1.Spins == 0 || (player1.Spins == 0 && 
                    player2.Spins == 0) || (player1.Spins == 0 && 
                    player2.Spins == 0 && player3.Spins == 0))
                {
                    player1.Spins = 1;
                    player2.Spins = 1;
                    player3.Spins = 1;
                    updateSpins();
                }

                //see who's turn it is to spin
                if (player1.Spins != 0)
                {
                    who = player1;
                }
                else if (player2.Spins != 0)
                {
                    who = player2;
                }
                else
                    who = player3;
    
                if(numplayers == 3)
                {
                    if (player1.Cash > player2.Cash && player1.Cash >
                        player3.Cash)
                    {
                        winner_label.Text = "Player1 Wins!!!";
                    }
                    else if (player2.Cash > player1.Cash && player2.Cash >
                        player3.Cash)
                    {
                        winner_label.Text = "Player2 Wins!!!";
                    }
                    else if (player3.Cash > player1.Cash && player3.Cash >
                        player2.Cash)
                    {
                        winner_label.Text = "Player3 Wins!!!";
                    }
                    else
                        winner_label.Text = "Draw";
                }
                else if (numplayers == 2)
                {
                    if (player1.Cash > player2.Cash)
                    {
                        winner_label.Text = "Player1 Wins!!!";
                    }
                    else
                        winner_label.Text = "Player2 Wins!!!";
                }
                else
                {
                    winner_label.Text = "Thanks for playing player1";
                    pass_spins.Enabled = false; 
                }
               numrounds++;
             }
             while (numrounds < maxrounds);
        }

        private void settingbutton_Click_1(object sender, EventArgs e)
        {
            setup.Show();
            startButton.Enabled = true;
        }

        //Purpose: accumulates the players' cash winnigns
        //Requires: none
        //Returns: none
        private void updateCash()
        {
            if (player1GroupBox.Visible == true &&
                player2GroupBox.Visible == true && 
                player3GroupBox.Visible == false)
            {
                player1ScoreTextBox.Text = player1.Cash.ToString("C");
                player2ScoreTextBox.Text = player2.Cash.ToString("C");
            }

            else if (player2GroupBox.Visible == true &&
                player3GroupBox.Visible == true)
            {
                player1ScoreTextBox.Text = player1.Cash.ToString("C");
                player2ScoreTextBox.Text = player2.Cash.ToString("C");
                player3ScoreTextBox.Text = player3.Cash.ToString("C");
            }

            else
                player1ScoreTextBox.Text = player1.Cash.ToString("C");
        }

        //Purpose: accumulates the players' spins
        //Requires: none
        //Returns: none
        private void updateSpins()
        {
            if (player1GroupBox.Visible == true &&
                player2GroupBox.Visible == true &&
                player3GroupBox.Visible == false)
            {
                player1SpinsTextBox.Text = player1.Spins.ToString();
                player2SpinsTextBox.Text = player2.Spins.ToString();
            }

            else if (player2GroupBox.Visible == true &&
                player3GroupBox.Visible == true)
            {
                player1SpinsTextBox.Text = player1.Spins.ToString();
                player2SpinsTextBox.Text = player2.Spins.ToString();
                player3SpinsTextBox.Text = player3.Spins.ToString();
            }

            else
                player1SpinsTextBox.Text = player1.Spins.ToString();
        }

        //Purpose: calls function to start asking questions
        //Requires: none
        //Returns: none
        private void questionPlayers()
        {
            if (player1GroupBox.Visible == true &&
                player2GroupBox.Visible == true &&
                player3GroupBox.Visible == false)
            {
                getplayerSpins(player1);
                getplayerSpins(player2);
            }

            else if (player2GroupBox.Visible == true &&
                 player3GroupBox.Visible == true)
            {
                getplayerSpins(player1);
                getplayerSpins(player2);
                getplayerSpins(player3);
            }

            else
                getplayerSpins(player1);
            updateCash();
            updateSpins();
        }

        //Purpose: Question players
        //Requires: instance of player class
        //Returns: none
        private void getplayerSpins(Player player)
        {
            gametrivia.startQuestions();
            gametrivia.ShowDialog(this);
            player.Spins += gametrivia.CorrectAnswers;
        }

        //Purpose: pass a player's spins to another player
        //Requires: object sender, EventArgs e
        //Returns: none
        private void pass_spins_Click(object sender, EventArgs e)
        {
            if(numplayers == 2)
            {
                if (who == player1)
                {
                    passSpin(player1, player2);
                }
                else
                    passSpin(player2, player1);
            }
            else if(numplayers == 3)
            {
                if (who == player2)
                    passSpin(player2, player3);
                else if (who == player3)
                    passSpin(player3, player1);
                else
                    passSpin(player1, player3);
            }
            //else
            updateSpins();
        }

        //Purpose: passes one player's spins to another
        //Requires: 2 players
        //Returns: none
        void passSpin(Player p1, Player p2)
        {
            p2.Spins += p1.Spins;
            p1.Spins = 0;
            who = p2;
        }
    }
}
