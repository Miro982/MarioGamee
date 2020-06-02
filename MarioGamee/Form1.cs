using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioGamee
{
    public partial class Form : System.Windows.Forms.Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 14;
        int score = 0;
        int obstacleSpeed = 11;

        Random rand = new Random();
        int position;
        bool isGameOver = false;

        public Form()
        {
            InitializeComponent();
            GameReset();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            mario.Top += jumpSpeed;
            txtscore.Text = "Score: " + score;
            if (jumping==true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -14;
                force -= 1;
            }
            else
            {
                jumpSpeed = 14;
            }
            if(mario.Top > 342 && jumping == false)
            {
                force = 14;
                mario.Top = 343;
                jumpSpeed = 0;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;
                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        Timer.Stop();
                        mario.Image = Properties.Resources.dead;
                        txtscore.Text += "Press N to play the game again:";
                        isGameOver = true;

                    }
                }
            }
            if(score >3)
            {
                obstacleSpeed = 17;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
                 
                    
                }
            
            }
        

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(jumping == true)
            {
                jumping = false;
            }

            if(e.KeyCode == Keys.N && isGameOver == true)
            {
                GameReset();
            }
        }
        private void GameReset()
            {
            force = 14;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 11;
            txtscore.Text = "Score: " + score;
            mario.Image = Properties.Resources.MarioRun;
            isGameOver = false;
            mario.Top = 342;

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox &&(string)x.Tag=="obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500,800) + (x.Width + 10);
                    x.Left = position;
                }
            }
            Timer.Start();
        }
    }
}
