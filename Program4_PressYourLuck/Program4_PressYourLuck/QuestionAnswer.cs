//Tellon Smith and Johann Redhead
//File: QuestionAnswer.cs
//information for question/answer structure

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program4_PressYourLuck
{
    class QuestionAnswer 
    {
        //private variables
        private string answer;
        private string question;

        //default
        public QuestionAnswer()
        {
            answer = "N/A";
            question = "N/A";
        }

        //parameterized
        public QuestionAnswer(string answer, string question)
        {
            this.answer = answer;
            this.question = question;
        }

        //gets and sets for answers and questions


        public string Answer
        {
            //returns users answer
            get
            {
                return answer;
            }

            set
            {
                this.answer = value;
            }
        }

        public string Question
        {
            //returns question to ask user
            get
            {
                return question;
            }

            set
            {
                this.question = value;
            }
        }

    }
}
