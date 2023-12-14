using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAKE_Mračanský
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rnd = new Random();

        bool goLeft, goRight, goDown, goUp;
        bool isGameStopped;
        public Form1()
        {
            InitializeComponent();
            new Setting();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && Setting.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Setting.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Setting.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Setting.directions != "up")
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void stopGame(object sender, EventArgs e)
        {            
            gameTimer.Stop();         
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            if(goLeft)
            {
                Setting.directions = "left";
            }
            if (goRight)
            {
                Setting.directions = "right";
            }
            if (goDown)
            {
                Setting.directions = "down";
            }
            if (goUp)
            {
                Setting.directions = "up";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Setting.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;                           
                            break;
                    }

                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }


                    if(Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }

                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }                
            }
            picCanvas.Invalidate();

            if(score >= 10 && score < 20)
            {
                gameTimer.Interval = 85;
            }
            if (score >= 20 && score < 30)
            {
                gameTimer.Interval = 70;
            }
            if (score >= 30 && score < 40)
            {
                gameTimer.Interval = 55;
            }
            if (score >= 40 && score < 50)
            {
                gameTimer.Interval = 40;
            }
            if(score == 50)
            {
                MessageBox.Show("Vyhrál jsi!");
                RestartGame();
            }
        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics kp = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.Red;
                }

                kp.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Setting.Width,
                    Snake[i].Y * Setting.Height,
                    Setting.Width,
                    Setting.Height                    
                    ));
                    
            }
           
                kp.FillEllipse(Brushes.Green, new Rectangle
                   (
                   food.X * Setting.Width,
                   food.Y * Setting.Height,
                   Setting.Width,
                   Setting.Height
                   ));
                       
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Setting.Width - 1;
            maxHeight = picCanvas.Height / Setting.Height - 1;

            Snake.Clear();

            startButton.Enabled = false;
            startButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 10, Y = 5};
            Snake.Add(head);

            for (int i = 0; i < 3; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }
           
            food = new Circle { X = rnd.Next(2, maxWidth), Y = rnd.Next(2, maxHeight) };          

            gameTimer.Start();
        }

        private void EatFood()
        {
            score += 1;

            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);

            food = new Circle { X = rnd.Next(2, maxWidth), Y = rnd.Next(2, maxHeight) };
        }

        private void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            stopButton.Enabled = true;
            MessageBox.Show("Prohrál jsi");

            if(score > highScore)
            {
                highScore = score;

                txtHighScore.Text = "High Score: " + highScore;
            }
        }
    }
}
