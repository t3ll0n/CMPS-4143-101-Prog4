//Tellon Smith and Johann Redhead
//GameTrivia.cs class
//information for trivia portion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program4_PressYourLuck
{
    public partial class GameTrivia : Form
    {
        string file_path;
        private string user_answer = "";
        private string correct_answer = "";
        private const int MAXQUESTIONS = 100;
        //variable to store number of questions from file
        private int numques = 0;

        private static QuestionAnswer[] questionanswer = new QuestionAnswer[MAXQUESTIONS];

        //number of questions answered since last startQuestioning call
        private static int questionCount = 0;

        //used to index into question and answers array
        private static int questionIndex = 0;

        //number of correct answers since startQuestioning was called
        private int correctAnswers = 0;

        //maximum questions to ask player at a time
        private const int MAX_QUESTIONS = 3;

        //default
        public GameTrivia()
        {
            InitializeComponent();
            //limit user interaction with form until trivia begins
            Submit.Enabled = false;
            next.Enabled = false;
            Questions.ReadOnly = true;
        }

        //setter for file path
        public string File_Path
        {
            get
            {
                return file_path;
            }
            set
            {
                this.file_path = value;
            }
        }

        //Purpose: gets file path
        //Requires: file path
        //Returns: none
        public void getFilePath(string file)
        {
            numques = readFile(file);
            if (numques < 1)
            {
                MessageBox.Show("No data present in file",
                    "invalid input file path supplied",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            //randomize questions stored in array
            else
            {
                randomizeQuestions();
            }
        }

        //Purpose: reads in the data from the input file, i.e questions/answer
        //Requires: input file location
        //Returns: count of question/answers in file
        public int readFile(string filename)
        {
            StreamReader filereader;
            //try/catch to ensure that file is open to be read
            try
            {
                filereader = new StreamReader(filename);

                //accumulator to store count of questions/answers
                int count;
                for (count = 0; !filereader.EndOfStream && count
                    < MAXQUESTIONS; count++)
                {
                    questionanswer[count] = new QuestionAnswer();
                    questionanswer[count].Question
                    = filereader.ReadLine();
                    questionanswer[count].Answer
                    = filereader.ReadLine();
                }
                return count;
            }

            catch (Exception ex)
            {
                //invalid input file
                MessageBox.Show("Error. Input file could not be read." +
                    " Ensure that it is the correct file. The program will now" +
                    " exit." + ex.Data);
                Application.Exit();
                return -1;
            }
        }

        //Purpose: randomizes question order in array
        //Requires: none
        //Returns: none
        private void randomizeQuestions()
        {
            Random rand = new Random();
            int index = 0;
            QuestionAnswer temp; //create temporary instance of class
            for (int i = 0; i < numques; i++)
            {
                index = rand.Next() % numques;
                temp = questionanswer[i];
                questionanswer[i] = questionanswer[index];
                questionanswer[index] = temp;
            }
        }

        //get function for corrct answers
        public string Correct_Answers
        {
            get
            {
                return correct_answer;
            }
        }

        public int CorrectAnswers
        {
            get
            {
                return correctAnswers;
            }
        }

        //Purpose: starts asking questions to the players
        //Requires: none
        //Returns: none
        public void startQuestions()
        {
            Submit.Enabled = false;
            next.Enabled = false;
            next.Text = "Next Question";
            Answers.ReadOnly = true;
            Trivia.Enabled = true;
            Questions.Clear();
            correctAnswers = 0;
            questionCount = 0;
            Answers.Clear();
        }

        //Purpose: calls function that begins questioning user
        //Requires: object sender, EventArgs e
        //Returns: none
        private void Trivia_Click(object sender, EventArgs e)
        {
            Answers.ReadOnly = false;
            Answers.Clear();
            questionUser();
            next.Text = "Next Question";
            Trivia.Enabled = false;
        }

        //Purpose: asks user the questions from the file
        //Requires: none
        //Returns: none
        private void questionUser()
        {
            Questions.Text = questionanswer[questionIndex].Question;
            Answers.ReadOnly = false;
            Submit.Enabled = true;
            next.Enabled = false;
        }

        //Purpose: reads in user's input and compares it to file answers
        //Requires: object sender, EventArgs e
        //Returns: none
        private void Submit_Click(object sender, EventArgs e)
        {
            Submit.Enabled = false;
            next.Enabled = true;

            //change both user answer and file answer to lowercase
            //to avoid checking dificulty
            user_answer = Answers.Text.ToLower();
            correct_answer = questionanswer[questionIndex].Answer.ToLower();

            //check to see if the same
            if (user_answer == correct_answer)
            {
                Result.ForeColor = Color.LawnGreen;
                Result.Text = "Correct!!!";
                ++correctAnswers; //increment numbers of correct answers
            }

            else
            {
                Result.ForeColor = Color.Red;
                Result.Text = "Incorrect :(";
            }

            if (questionCount == MAX_QUESTIONS - 1)
                next.Text = "Finish";

            questionIndex = (questionIndex + 1) % numques;
            next.Enabled = true;
        }


        //Purpose: moves on to the next question
        //Requires: object sender, EventArgs e
        //Returns: none
        private void next_Click(object sender, EventArgs e)
        {
            Answers.Clear();
            Result.Text = "";
            if (next.Text != "Finish")
            {
                questionIndex = (questionIndex + 1) % numques;
                ++questionCount;
                if (questionCount == MAX_QUESTIONS)
                {
                    next.Text = "Finish";
                    Submit.Enabled = false;
                }

                else
                {
                    questionUser();
                }
            }

            else
            {
                setDialogResults();//stores results of rounds in user fields
                //refresh form for new user-and not to lose info
                Questions.Clear();
                next.Text = "Next Question";
                next.Enabled = false;
                Submit.Enabled = false;
                this.Close();
            }
        }

        private void QuestionAnswerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            setDialogResults();
        }

        private void setDialogResults()
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
