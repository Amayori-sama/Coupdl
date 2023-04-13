using System;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lvl();
        }

        private void game1_Click(object sender, MouseEventArgs e)
        {
            game1.onMouseClick(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lvl();
            game1.EndProcess();
            game1.StarProcess();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "EndGame";
            game1.EndProcess();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            game1.EndProcess();
            game1.gameLevel = 2;
            lvl();
            game1.StarProcess();
        }
        private void lvl() 
        {
            label1.Text = "Lvl. " + game1.gameLevel.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            game1.EndProcess();
            game1.gameLevel = 3; 
            lvl();
            game1.StarProcess();
        }
    }
}