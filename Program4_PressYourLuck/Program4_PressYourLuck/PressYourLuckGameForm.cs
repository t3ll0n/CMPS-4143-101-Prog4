//-----------------------------------------------------------------------------------------------------
//
// Name: Tellon Smith and Johann Redhead
//
// Course: CS 4143 - Contemporary Programming Lang, Fall 16, Dr. Stringfellow
//
// Program Assignment : #4
//
// Due Date: Thursday, Oct. 6th, 2016, 11AM
//
// Purpose: This program is an interactive game based on the Press Your Luck game show. The game allows
//          up to 3 players to collect spins by answering triva questions and then use the spins on the
//          game board to win cash prizes.
//
//-----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;

namespace Program4_PressYourLuck
{
    public partial class PressYourLuckGameForm : Form
    {
        private const int NUM_SPACES = 18;

        //class variables        
        int numplayers = 0;
        int numrounds = 1;
        bool endround = false;
        int pictureIndex = 0;
        bool stop = false;
        Setup setup = new Setup(); //create instance of setup form

        Player player1 = new Player(), player2 = new Player(),
        player3 = new Player(), who; //create 3 instances for 3 players and 
                                     //instance to track active player


        //create instance of gameTrivia form
        GameTrivia gametrivia = new GameTrivia();

        //Image directory
        private string imageDir = @"..\..\images\gameboard";
        private string gamebordCenterImg = @"..\..\images\PressYourLuck.PNG";
        private string whammyImg = @"..\..\images\whammy.gif";
        //random number generator 
        private Random rand = new Random();
        //list to store all the picture boxes
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        //list to store all the images
        private List<Image> imageList = new List<Image>();
        //list to store image file names
        private List<String> imageNames = new List<String>();
        //stores assembly 
        private Assembly assembly;
        //audio file
        private String audioFile = "";
        //audio file path
        private String audioPath = @"..\..\sounds";
        //regular expression which determines the value from the image names
        private Regex regValue = new Regex(@"([0-9][0-9][0-9][0-9])");
        //current picture box
        PictureBox currentPic;


        public PressYourLuckGameForm()
        {
            InitializeComponent();

            addPictureBoxes();

            //get the assembly
            assembly = Assembly.GetExecutingAssembly();

            //store all resource names in the assembly which end in .png
            imageNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.EndsWith(".png")).ToList();

            //add images to image list
            imageList = Directory.GetFiles(imageDir, "*.png", SearchOption.AllDirectories).Select(Image.FromFile).ToList();

            //randomly populate picture boxes with images
            populatePictureBoxes();

            //play intro sound
            audioFile = (audioPath + "\\" + "intro.wav");
            playSound(audioFile);
            Shown += Form1_Shown;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {

            MessageBox.Show("Welcome to the Press Your Luck Game." +
                " Please run the set-up and fill out all fields.", "Welcome");

        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howToPlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select the number of users to play the game. Each user answers 3" +
                " questions; with each correct answer having 3 spins After the trivia section" +
                " is complete, users will use their spins on the spinning gameboard. The user" +
                " with the most money at the end o the 2 rounds, will be declared the winner" +
                " and move on to the bonus round. All cash values won are strictly fictional" +
                " and not subject to real life.", "How To Play");
        }

        //Purpose: clear data fiels in main form
        //Requires: none
        //Returns: none
        private void clearData()
        {
            player1SpinsTextBox.Text = "";
            player2SpinsTextBox.Text = "";
            player3SpinsTextBox.Text = "";
            player1ScoreTextBox.Text = "";
            player2ScoreTextBox.Text = "";
            player3ScoreTextBox.Text = "";
            player1.resetPlayerData();
            player2.resetPlayerData();
            player3.resetPlayerData();
            current_spinner.Text = "";
            round_label.Text = "";
            winner_label.Text = "";
            numrounds = 1;
            endround = false;
            pictureIndex = 0;
            playerInit();
            stop = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            pictureBox19.ImageLocation = gamebordCenterImg;
            if (startButton.Text == "START GAME")
            {
                gametrivia.getFilePath(setup.File_Path);
                playerInit();
                startButton.Text = "PLAY AGAIN";
                startButton.Enabled = false;
            }
            else if (startButton.Text == "PLAY AGAIN")
            {
                startButton.Text = "START GAME";
                clearData();
                startButton.Enabled = false;
            }
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

        //Purpose: randomly populate picture boxes with images 
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
            //get number of players from set-up form
            setup.getNumPlayers(ref numplayers);
            gametrivia.File_Path = setup.File_Path;
            //error checking to ensure that user went through set-up
            if (setup.File_Path == "")
            {
                MessageBox.Show("Error, please run the set-up and" +
                    " select an input file. The program will now exit");
                Application.Exit(); //close the program
            }

            //activate score menu or corresponding number of players
            if (numplayers == 1)
            {
                player1GroupBox.Visible = true;
            }

            else if (numplayers == 2)
            {
                player1GroupBox.Visible = true;
                player2GroupBox.Visible = true;
            }

            else if (numplayers == 3)
            {
                player1GroupBox.Visible = true;
                player2GroupBox.Visible = true;
                player3GroupBox.Visible = true;
            }
            updateCash();
            updateSpins();
            if (!this.Visible)
                this.Show();
            questionPlayers();
            startRound();
        }

        //Purpose: starts a round in the game
        //Requires: none
        //Returns: none
        private void startRound()
        {
            startstop_spin.Enabled = true;
            //disable pass spins button if only one player
            if (numplayers == 1)
            {
                pass_spins.Enabled = false;
            }
            else
                pass_spins.Enabled = true;

            if (numrounds < 3)
            {
                round_label.Text = "Round " + numrounds;
            }
            else
                round_label.Text = "Bonus Round";

            //statements to update spin label and to determine 
            //who has to spin
            if (numplayers == 1)
            {
                if (player1.Spins != 0)
                {
                    who = player1;
                    current_spinner.Text = "Player 1 to Spin!";
                }
                else if (player2.Spins != 0)
                {
                    who = player2;
                    current_spinner.Text = "Player 2 to Spin";
                }
                else if (player3.Spins != 0)
                {
                    who = player3;
                    current_spinner.Text = "Player 3 to Spin";
                }
            }

            else if (numplayers == 2)
            {
                if (player1.Spins != 0)
                {
                    who = player1;
                    current_spinner.Text = "Player 1 to Spin!";
                }
                else if (player2.Spins != 0)
                {
                    who = player2;
                    current_spinner.Text = "Player 2 to Spin!";
                }
            }

            else
            {
                if (player1.Spins != 0)
                {
                    who = player1;
                    current_spinner.Text = "Player 1 to Spin!";
                }
                else if (player2.Spins != 0)
                {
                    who = player2;
                    current_spinner.Text = "Player 2 to Spin!";
                }
                else
                {
                    who = player3;
                    current_spinner.Text = "Player 3 to Spin!";
                }
            }
        }

        private void settingbutton_Click_1(object sender, EventArgs e)
        {
            setup.ShowDialog();
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
            {
                if (player1GroupBox.Visible == true)
                    player1ScoreTextBox.Text = player1.Cash.ToString("C");
                else if (player2GroupBox.Visible == true)
                    player2ScoreTextBox.Text = player2.Cash.ToString("C");
                else if (player3GroupBox.Visible == true)
                    player3ScoreTextBox.Text = player3.Cash.ToString("C");
            }
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

            else if (player1GroupBox.Visible == true)
                player1SpinsTextBox.Text = player1.Spins.ToString();
            else if (player2GroupBox.Visible == true)
                player2SpinsTextBox.Text = player2.Spins.ToString();
            else if (player3GroupBox.Visible == true)
                player3SpinsTextBox.Text = player3.Spins.ToString();
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
            if (numplayers == 1)
            {
                if (player1GroupBox.Visible == true)
                    getplayerSpins(player1);
                else if (player2GroupBox.Visible == true)
                    getplayerSpins(player2);
                else if (player3GroupBox.Visible == true)
                    getplayerSpins(player3);
            }
            //donate spins to players if they leave 
            //trivia section without spins
            if (numplayers == 1)
            {
                if (player1.Spins == 0 && player1GroupBox.Visible == true)
                {
                    player1.Spins = 3;
                    updateSpins();
                }
                else if (player2.Spins == 0 && player2GroupBox.Visible == true)
                {
                    player2.Spins = 3;
                    updateSpins();
                }
                else if (player3.Spins == 0 && player3GroupBox.Visible == true)
                {
                    player3.Spins = 3;
                    updateSpins();
                }
            }
            else
            {
                if (player1.Spins == 0 &&
                player2.Spins == 0 || (player1.Spins == 0 &&
                player2.Spins == 0 && player3.Spins == 0))
                {
                    player1.Spins = 3;
                    player2.Spins = 3;
                    player3.Spins = 3;
                    updateSpins();
                }
            }
            updateCash();
            updateSpins();
        }

        //Purpose: announces the winner after 2 rounds
        //Requires: none
        //Return: none
        private void announceWinner()
        {
            if (numplayers == 3)
            {
                if (player1.Spins == 0 && player2.Spins == 0 &&
                    player3.Spins == 0)
                {
                    if (player1.Cash > player2.Cash && player1.Cash >
                       player3.Cash)
                    {
                        winner_label.Text = "Player1 Wins!!!";
                        who = player1;
                    }
                    else if (player2.Cash > player1.Cash && player2.Cash >
                           player3.Cash)
                    {
                        winner_label.Text = "Player2 Wins!!!";
                        who = player2;
                    }
                    else if (player3.Cash > player1.Cash && player3.Cash >
                            player2.Cash)
                    {
                        winner_label.Text = "Player3 Wins!!!";
                        who = player3;
                    }
                    else
                        winner_label.Text = "Draw";
                }
            }
            else if (numplayers == 2)
            {
                if (player1.Spins == 0 && player2.Spins == 0)
                {
                    if (player1.Cash > player2.Cash)
                    {
                        winner_label.Text = "Player1 Wins!!!";
                        who = player1;
                    }
                    else if (player2.Cash > player1.Cash)
                    {
                        winner_label.Text = "Player2 Wins!!!";
                        who = player2;
                    }

                    else
                        winner_label.Text = "Draw";
                }
            }

            else
            {
                if (player1.Spins == 0 && player1GroupBox.Visible == true)
                {
                    winner_label.Text = "Thanks for playing player1";
                    pass_spins.Enabled = false;
                    startButton.Enabled = true;
                    startstop_spin.Enabled = false;
                }
                else if (player2.Spins == 0 && player2GroupBox.Visible == true)
                {
                    winner_label.Text = "Thanks for playing player2";
                    pass_spins.Enabled = false;
                    startButton.Enabled = true;
                    startstop_spin.Enabled = false;
                }
                else if (player3.Spins == 0 && player3GroupBox.Visible == true)
                {
                    winner_label.Text = "Thanks for playing player3";
                    pass_spins.Enabled = false;
                    startButton.Enabled = true;
                    startstop_spin.Enabled = false;
                }
            }
        }

        //Purpose: Question players
        //Requires: instance of player class
        //Returns: none
        private void getplayerSpins(Player player)
        {
            gametrivia.startQuestions();
            gametrivia.ShowDialog(this);

            //gives player 3 spins per correct answer
            player.updateSpins(gametrivia.CorrectAnswers * 3);
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
                    current_spinner.Text = "Player 2 to Spin!";
                }
                else
                {
                    passSpin(player2, player1);
                    current_spinner.Text = "Player 1 to Spin!";
                }
            }
            else if (numplayers == 3)
            {
                if (who == player2)
                {
                    passSpin(player2, player3);
                    current_spinner.Text = "Player 3 to Spin!";
                }
                else if (who == player3)
                {
                    passSpin(player3, player1);
                    current_spinner.Text = "Player 1 to Spin!";
                }
                else
                {
                    passSpin(player1, player3);
                    current_spinner.Text = "Player 1 to Spin!";
                }
            }
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
            pass_spins.Enabled = false;
        }


        //Purpose: randomly moves cursor around board to higlight images
        //Requires: none
        //Returns: none
        private void randomCursor()
        {
            int cursorChanges = 0;

            do
            {
                cursorChanges++;

                //randomly selects a picturebox on the gameboard
                currentPic = pictureBoxes[rand.Next(0, pictureBoxes.Count)];

                //highlight the selected image by changing the background image to yellow
                currentPic.BackColor = Color.Yellow; 

                //randomizes images in pictureboxes after two cursor changese
                if (cursorChanges == 2)
                {
                    cursorChanges = 0;

                    foreach (PictureBox p in pictureBoxes)
                    {
                        if (IsHandleCreated)
                        {
                            p.Invoke((Action)(() =>
                            {
                                p.Image = imageList.ElementAt(rand.Next(0,
                                    imageList.Count));
                            }));
                        }
                    }
                }
                //Pause
                System.Threading.Thread.Sleep(400);
                //return background color to normal
                currentPic.BackColor = Color.Transparent; 
            } while (!stop);

            //keep the current picture box highlighted
            currentPic.BackColor = Color.Yellow;

            //get value of picture box
            addCashValue(currentPic.Image);

            //keep track of current picutre index
            pictureIndex = pictureBoxes.IndexOf(currentPic);
        }


        //Purpose: adds the equivalent cash value to the player
        //Requires: image of current picture box
        //Returns: none
        private void addCashValue(Image i)
        {
            //if image on board is a whammy
            if (Int32.Parse(regValue.Match(imageNames.ElementAt
                (imageList.IndexOf(i)).ToString()).ToString()) < 10)
            {
                if (who == player1)
                {
                    player1.updateCash(0);
                }
                else if (who == player2)
                {
                    player2.updateCash(0);
                }
                else if (who == player3)
                {
                    player3.updateCash(0);
                }

                //show whammy gif
                pictureBox19.ImageLocation = whammyImg;

                //get file corresponding to board value
                audioFile = (audioPath + "\\" + Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()) + ".wav");

                playSound(audioFile);

                //pause to play annimation
                Thread.Sleep(5000);

                //return to regular gameboard image
                pictureBox19.ImageLocation = gamebordCenterImg;
            }
            //if regular money image
            else
            {
                if (who == player1)
                {
                    player1.updateCash(Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()));

                    ///get file corresponding to board value
                    audioFile = (audioPath + "\\" + Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()) + ".wav");

                    playSound(audioFile);

                }
                else if (who == player2)
                {
                    player2.updateCash(Int32.Parse(regValue.Match
                     (imageNames.ElementAt(imageList.IndexOf(i)).
                     ToString()).ToString()));

                    //get file corresponding to board value
                    audioFile = (audioPath + "\\" + Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()) + ".wav");

                    playSound(audioFile);
                }
                else if (who == player3)
                {
                    player3.updateCash(Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()));

                    //get file corresponding to board value
                    audioFile = (audioPath + "\\" + Int32.Parse(regValue.Match
                    (imageNames.ElementAt(imageList.IndexOf(i)).
                    ToString()).ToString()) + ".wav");

                    playSound(audioFile);
                }
            }
        }

        private void startstop_spin_Click(object sender, EventArgs e)
        {
            endround = false;

            //start the gameboard animation when user starts to spin
            if (startstop_spin.Text == "START SPIN")
            {
                audioFile = (audioPath + "\\" + "board.wav");
                playSound(audioFile);
                startRound();
                pictureBoxes.ElementAt(pictureIndex).BackColor = Color.Transparent;
                //change button text to stop for user to stop spinning board
                startstop_spin.Text = "STOP SPIN";

                //start thread for cursor randomization
                System.Threading.Thread random_cursorThread;
                random_cursorThread = new System.Threading.
                Thread(new System.Threading.ThreadStart(randomCursor));
                //start thread
                random_cursorThread.Start();
            }

            //if user stops spinning the board
            else
            {
                startstop_spin.Text = "START SPIN";
                stop = true;
                Thread.Sleep(400);
                stop = false;
                who.Spins -= 1; //decrement player spins
                updateSpins();
                updateCash();
                if (numplayers == 3)
                {
                    if (player1.Spins == 0 && player2.Spins == 0 &&
                        player3.Spins == 0)
                    {
                        endround = true;
                        numrounds++;
                    }
                }
                else if (numplayers == 2)
                {
                    if (player1.Spins == 0 && player2.Spins == 0)
                    {
                        endround = true;
                        numrounds++;
                    }
                }
                else
                {
                    if (player1.Spins == 0 && player1GroupBox.Visible == true)
                    {
                        endround = true;
                        numrounds++;
                    }
                    else if (player2.Spins == 0 && player2GroupBox.Visible == true)
                    {
                        endround = true;
                        numrounds++;
                    }
                    else if (player3.Spins == 0 && player3GroupBox.Visible == true)
                    {
                        endround = true;
                        numrounds++;
                    }
                }
                //if player(s) run out of spins and 2 rounds
                if (endround && numrounds <= 2)
                {
                    round_label.Text = "Round " + numrounds;
                    current_spinner.Text = "";

                    questionPlayers();
                }
                else
                {
                    announceWinner();
                }
                if (numrounds == 3 & setup.Numpeople > 1)
                {
                    numplayers = 1;
                    round_label.Text = "Bonus Round";
                    endround = false;
                    //disable all textboxes but winner's for bonus round
                    if (who == player1)
                    {
                        player2GroupBox.Visible = false;
                        player3GroupBox.Visible = false;
                    }
                    else if (who == player2)
                    {
                        player1GroupBox.Visible = false;
                        player3GroupBox.Visible = false;
                    }
                    else if (who == player3)
                    {
                        player1GroupBox.Visible = false;
                        player2GroupBox.Visible = false;
                    }
                    questionPlayers();
                    numrounds++; //won't question user in bonus round 
                    //multiple times
                }
            }
        }

        //Purpose: plays a sound 
        //Requires: path to sound
        //Returns: sound
        private void playSound(string path)
        {
            SoundPlayer player = new SoundPlayer(path);
            player.Load();
            player.Play();
        }

    }
}
