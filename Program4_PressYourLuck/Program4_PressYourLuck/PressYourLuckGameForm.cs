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
        public PressYourLuckGameForm()
        {
            InitializeComponent();
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howToPlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Once upon a time....", "How To Play");
        }
    }
}
