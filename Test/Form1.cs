using System;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(lvl);
            timer.Interval = 100;
            timer.Start();
        }
        Timer timer = new Timer();
        private void game1_Click(object sender, MouseEventArgs e)
        {
            game1.onMouseClick(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game1.StarProcess();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "EndGame";
            game1.EndProcess();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            game1.AllClear();
            game1.gameLevel = 2;
            game1._score = 1;
            game1.StarProcess();
        }
        private void lvl(object sender, EventArgs e)
        {
            label1.Text = "Lvl. " + game1.gameLevel.ToString();
            label2.Text = "Score: " + game1._score; 
        }
        private void button4_Click(object sender, EventArgs e)
        {
            game1.AllClear();
            game1.gameLevel = 3;
            game1._score = 1;
            game1.StarProcess();
        }
    }
}