using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program4_PressYourLuck
{
    public partial class PressYourLuckGameForm : Form
    {
        private const int NUM_SPACES = 18;

        //class variables

        int numplayers = 0;
        int numrounds = 1;
        int maxrounds = 2;
        Setup setup = new Setup(); //create instance of setup form

        Player player1 = new Player(), player2 = new Player(),
        player3 = new Player(), who; //create 3 instances for 3 players and 
        //instance to track active player

        //create instance of gameTrivia form
        GameTrivia gametrivia = new GameTrivia();

        //Image directory
        private string imageDir = @"..\..\images\gameboard";
        //random number generator 
        private Random rand = new Random();
        //list to hold all picture boxes****
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        //list to hold all images****
        private List<Image> imageList = new List<Image>();
        //list to contain image file names (runs parallel to imageList)****
        private List<String> imageNames = new List<String>();
        //assembly reference****
        private Assembly assembly;
        //integer to store the index of the currently selected picturebox****
        private int pictureBoxIndex = 0;
        //declaring thread for cursor randomization*******
        //System.Threading.Thread randomCursorThread; //*******
        //variable to control spins
        private bool spin = false;

        //regular expression to be used in deciding value of image
        private Regex regValue = new Regex(@"[0-9]*0", RegexOptions.IgnoreCase);


        public PressYourLuckGameForm()
        {
            InitializeComponent();
            MessageBox.Show("Please run the set-up and fill out all fields.");

            addPictureBoxes();

            //get the current assembly****
            assembly = Assembly.GetExecutingAssembly();

            //store all resource names associated with the executing assembly which end in "_GameBoardImage.png" in a list****
            imageNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.EndsWith(".png")).ToList();

            //add images to image list
            imageList = Directory.GetFiles(imageDir, "*.png", SearchOption.AllDirectories).Select(Image.FromFile).ToList();

            //randomly populate gameboard picture boxes with images on form load****
            populatePictureBoxes();
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

        //Purpose: add game board picture boxes to a list(except the center picture box) 
        //Requires: picture box list
        //Returns: picture box list with picture boxes
        private void addPictureBoxes()
        {
            for (int i = 1; i <= NUM_SPACES; i++)
            {
                pictureBoxes.Add((PictureBox)Controls.Find("pictureBox" + i, true)[0]);
            }
        }

        //Purpose: randomly populate gameboard picture boxes with images***** 
        //Requires: picture box list and image list
        //Returns: populated picuture boxes on game board
        private void populatePictureBoxes()
        {
            foreach (PictureBox p in pictureBoxes)
            {
                p.Image = imageList.ElementAt(rand.Next(0, imageList.Count));
            }
        }

        //Purpose: initialises game with players entered in setup form
        //Requires: none
        //Returns: none
        public void playerInit()
        {
            //get number o players from set-up form
            setup.getNumPlayers(ref numplayers);

            //error checking to ensure that user went through set-up
            if (setup.File_Path == "")
            {
                MessageBox.Show("Error, please run the set-up and" +
                    " select an input file. The program will now exit");
                Application.Exit(); //close the program
            }

            //activate score menu or correspondiing number of players
            if (numplayers == 2)
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
                startStopSpinButton.Enabled = true;
                passSpinsButton.Enabled = true;
                round_label.Text = "Round " + numrounds;
                questionPlayers();

                //donate spins to players if they leave 
                //trivia section without spins
                if (player1.Spins == 0 || (player1.Spins == 0 &&
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

                if (numplayers == 3)
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
                    passSpinsButton.Enabled = false;
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

        private void startStopSpinButton_Click(object sender, EventArgs e)
        {
            //after the start button is pressed, the text will be changed to "Stop!"*********
            if (startStopSpinButton.Text == "START SPIN")
            {
                pictureBoxes.ElementAt(pictureBoxIndex).BackColor = Color.Transparent;
                startStopSpinButton.Text = "STOP SPIN!";

                //declaring thread for cursor randomization
                System.Threading.Thread randomCursorThread;
                randomCursorThread = new System.Threading.Thread(new System.Threading.ThreadStart(RandomCursor));
                randomCursorThread.Start(); //begin randomCursorThread
            }
            else //StartStopButton.Text = "Stop!"*******
            {
                startStopSpinButton.Text = "START SPIN";
                spin = true;
                System.Threading.Thread.Sleep(400); //give the thread time to get the message
                spin = false;
                //players.ElementAt(getPlayer() - 1).decrementSpins();
                // updatePlayerTurnLabel();
                // updatePlayerScoreLabels();
                //if no players have any remaining spins, announce the winner; game over
                // if (getPlayer() < 0)
                //{
                //     announceWinner();
                //    this.Close();
                // }
                //}
            }
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
            if (numplayers == 2)
            {
                if (who == player1)
                {
                    passSpin(player1, player2);
                }
                else
                    passSpin(player2, player1);
            }
            else if (numplayers == 3)
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

        //Purpose: 
        //Requires: none
        //Returns: none
        private void RandomCursor()//******
        {
            int numOfCursChanges = 0;

            PictureBox currPic = pictureBoxes[rand.Next(0, pictureBoxes.Count)]; //set currPic to a random picturebox

            while (!spin)
            {
                numOfCursChanges++;

                currPic = pictureBoxes[rand.Next(0, pictureBoxes.Count)]; //randomly select a picturebox on the board

                currPic.BackColor = Color.Red; //highlight image

                //randomize images in pictureboxes every other cursor change
                if (numOfCursChanges == 2)
                {
                    numOfCursChanges = 0;

                    foreach (PictureBox p in pictureBoxes)
                    {
                        if (IsHandleCreated)
                        {
                            p.Invoke((Action)(() => { p.Image = imageList.ElementAt(rand.Next(0, imageList.Count)); }));
                        }
                    }
                }

                System.Threading.Thread.Sleep(350); //Pause

                currPic.BackColor = Color.Transparent; //de-highlight image
            }

            //keep current picture highlighted
            currPic.BackColor = Color.Red;

            //get chosen picture's value
            addValue(currPic.Image);

            //addSpin(currPic.Image);
            //add value to player's scrore...

            //saving currPic index
            pictureBoxIndex = pictureBoxes.IndexOf(currPic);
        }


        //Purpose: calls function to start asking questions
        //Requires: none
        //Returns: none
        private void addValue(Image i)
        {
            //if-condition interpretation: get the image name by taking advantage of the fact that imageList and imageNames run parallel to each other
            //use regular expression to get the part of the string associated with the value of the image
            //parse the resulting string into an integer to get the value
            //if the value is less than 10, it's a whammy
            if (Int32.Parse(regValue.Match(imageNames.ElementAt(imageList.IndexOf(i)).ToString()).ToString()) < 10)
            {
                player1.updateCash(0);
                //player1ScoreTextBox.Text = Convert.ToString(player.getCash());
            }
            else
            {
                player1.updateCash(Int32.Parse(regValue.Match(imageNames.ElementAt(imageList.IndexOf(i)).ToString()).ToString()));
                //player1ScoreTextBox.Text = Convert.ToString(player.getCash());
            }
        }
    }
}
