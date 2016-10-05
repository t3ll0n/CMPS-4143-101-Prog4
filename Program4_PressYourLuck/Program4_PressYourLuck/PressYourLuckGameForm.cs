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
        int roundNum = 1;
        int maxrounds = 2;
        Setup setup = new Setup(); //create instance of setup form

        private Player player1 = new Player(), player2 = new Player(),
        player3 = new Player(); //create 3 instances for 3 players


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

            //gametrivia.FilePath = setup.File_Path;

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

            if (roundNum <= maxrounds)
            {

                questionPlayers();
            }
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
    }
}
