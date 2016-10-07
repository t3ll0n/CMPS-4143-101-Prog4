//Tellon Smith and Johann Redhead
//Setup.cs
//holds information for the setup form

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program4_PressYourLuck
{
    public partial class Setup : Form
    {
       private int numpeople;
       private string file_path;

        public Setup()
        {
            InitializeComponent();
            numpeople = 1;
            file_path = "";
        }


        private void choosefile_Click(object sender, EventArgs e)
        {

            // Displays an OpenFileDialog so the user can select a text file.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files|*.txt";
            openFileDialog1.Title = "Select a File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .txt file was selected, open it.
            if (openFileDialog1.ShowDialog() == System.Windows.
                Forms.DialogResult.OK)
            {
                // place file path to text file in a variable
                file_path = openFileDialog1.FileName;
                path_label.Text = file_path;
            }
        }

        public string File_Path
        {
            //returns the filepath of the text file
            get
            {
                return file_path;
            }
        }

        private void numplayer_SelectedValueChanged(object sender, EventArgs e)
        {
            numpeople = Convert.ToInt32(numplayer.Text);
        }

        public void getNumPlayers(ref int numplayers)
        {
            numplayers =  numpeople;
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            getNumPlayers(ref numpeople);
            this.Close();
        }
    }
}
