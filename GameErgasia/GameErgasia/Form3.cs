using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameErgasia
{
    public partial class Form3 : Form
    {
        

    static int max, index,lvltime,x1,y1,currscore,hscore,back=1,currlvl;
        static bool flagclose;
        static string namemax,name1, texttoshow;
        private int direction = -1,speed=5;

        void ClosingTheGame()
        {

                Form2 myForm2 = new Form2(name1);
                myForm2.Show();
                this.Close();
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (lvltime > 0)
            {
                texttoshow = "Do you really want to restart the game? The current progression will be lost.";
            }
            else if (lvltime == 0)
            {
                texttoshow = "Do you really want to restart the game?";
            }
            if (MessageBox.Show(texttoshow, "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form3 myForm3 = new Form3(name1, currlvl.ToString(), hscore.ToString());
                myForm3.Show();
                flagclose = false;
                this.Close();
                flagclose = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagclose)
            {
                if (lvltime > 0)
                {
                    texttoshow = "Do you really want to exit the game? The current progression will be lost.";
                }
                else if (lvltime == 0)
                {
                    texttoshow = "Do you really want to exit the game?";
                }
                if (MessageBox.Show(texttoshow, "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    flagclose = false;
                    ClosingTheGame();
                }
            }
        }

        

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            if (lvltime > 0)
            {
                texttoshow = "Do you really want to exit the game? The current progression will be lost.";
            }
            else if(lvltime == 0)
            {
                texttoshow = "Do you really want to exit the game?";
            }
                if (MessageBox.Show(texttoshow, "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                      flagclose = false;
                      ClosingTheGame();
                 }
          }

        private void backchange_Tick(object sender, EventArgs e)
        {
            if (back % 2 == 0)
            {
                panel1.BackgroundImage = Properties.Resources.sky4;
                pictureBox1.Image = Properties.Resources.cloudss;
                pictureBox5.Image = Properties.Resources.cloudss;
                pictureBox3.Image = Properties.Resources.cloudss;
                pictureBox7.Image = Properties.Resources.cloudss;
                pictureBox6.Image = Properties.Resources.cloudss;
                pictureBox2.Image = Properties.Resources.cloudss;
                pictureBox4.Image = Properties.Resources.cloudss;
            }
            else
            {
                panel1.BackgroundImage = Properties.Resources.night2;
                pictureBox1.Image = Properties.Resources.asteroid2;
                pictureBox5.Image = Properties.Resources.asteroid2;
                pictureBox3.Image = Properties.Resources.asteroid2;
                pictureBox7.Image = Properties.Resources.asteroid2;
                pictureBox6.Image = Properties.Resources.asteroid2;
                pictureBox2.Image = Properties.Resources.asteroid2;
                pictureBox4.Image = Properties.Resources.asteroid2;
            }
            back++;
        }

        private void movingcloud_Tick(object sender, EventArgs e)
        {
            PictureBox[] boxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };
            for (int j = 0; j < 7; j++)
            {
                boxes[j].Left += direction - speed;
                if (boxes[j].Left < panel1.Left - pictureBox1.Width)
                {
                    Random rand = new Random();
                    int h = rand.Next(panel1.Height - pictureBox8.Height);
                    //boxes[j].Left = panel1.Right-50;
                    boxes[j].Location = new Point(panel1.Right + 50, h);
                }
            }


        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            currscore += 5;
            label13.Text = currscore.ToString();
            if (currscore > max)
            {
                label11.Text = name1;
                label12.Text = currscore.ToString();
            }
            if (currscore > hscore)
            {
                label9.Text = currscore.ToString();
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
       
        }

        private void helichange_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int x = r.Next(panel1.Width-pictureBox8.Width);
            int y = r.Next(panel1.Height-pictureBox8.Height);
            pictureBox8.Location = new Point(x, y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        void BestPlayer(string lvl)
        {
            max = 0;
            if(lvl == "1")
            {
                index = 1;
            }else if(lvl == "2")
            {
                index = 2;
            }
            else
            {
                index = 3;
            }
            foreach (var line in File.ReadLines("GameDataBase.txt"))
            {
                if (line != null)
                {
                    string[] tmp = new string[5];
                    tmp = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); ;
                    if (int.Parse(tmp[index]) > max)
                    {
                        max = int.Parse(tmp[index]);
                        namemax = tmp[0];
                    }
                }
            }
            label11.Text = namemax;
            label12.Text = max.ToString();
           

        }

        private void reverse_Tick(object sender, EventArgs e)
        {
            lvltime -= 1;
            label14.Text = lvltime.ToString();
            if(lvltime == 0)
            {
                reverse.Enabled = false;
                movingcloud.Enabled = false;
                helichange.Enabled = false;
                pictureBox8.Enabled = false;
                MessageBox.Show("Game Over");
                backchange.Enabled = false;
                 string[] lines2 = File.ReadAllLines("GameDataBase.txt");
                for (int i = 0; i < lines2.Length; i++)
                {
                        string[] tmp2;
                        tmp2 = lines2[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); ;
                         string lastplayed = tmp2[4];
                        if (tmp2[0] == name1)
                        {
                             lastplayed = DateTime.Now.ToString();
                             if (currscore > hscore)
                         {
                            hscore = currscore;
                                if (currlvl == 1)
                                {
                                    tmp2[1] = currscore.ToString();
                               }else if (currlvl == 2)
                               {
                                 tmp2[2] = currscore.ToString();
                               }else if (currlvl == 3)
                              {
                                    tmp2[3] = currscore.ToString();
                             }
                         }

                            
                              
                            
                       }

                           lines2[i] = tmp2[0] + " " + tmp2[1] + " " + tmp2[2] + " " + tmp2[3] + " " + lastplayed;
                          
                    
                }

                    File.WriteAllLines("GameDataBase.txt", lines2);
               
            }
        }


        public Form3(string user,string level,string score)
        {
            InitializeComponent();
            try
            {
                Cursor myCursor = new Cursor("scope3.cur");
                panel1.Cursor = myCursor;
                currscore = 0;
                flagclose = true;
                hscore = int.Parse(score);
                name1 = user;
                currlvl = int.Parse(level);
                label8.Text = user;
                label9.Text = score;
                label10.Text = level;
                label13.Text = 0.ToString();
                BestPlayer(level);
            }
            catch (FormatException)
            {
                MessageBox.Show("Error:Missing Informations Please Check the GameDataBase.txt");
                flagclose = false;
                Application.Exit();
            }
            if (level == "1")
            {
                lvltime = 60;
                helichange.Enabled = false;
                helichange.Interval = 1000;
                helichange.Enabled = true;
                panel1.BackgroundImage = Properties.Resources.sky4;
                pictureBox1.Image = Properties.Resources.cloudss;
                pictureBox5.Image = Properties.Resources.cloudss;
                pictureBox3.Image = Properties.Resources.cloudss;
                pictureBox7.Image = Properties.Resources.cloudss;
                pictureBox6.Image = Properties.Resources.cloudss;
                pictureBox2.Image = Properties.Resources.cloudss;
                pictureBox4.Image = Properties.Resources.cloudss;
            }
            else if (level == "2")
            {
                lvltime = 45;
                helichange.Enabled = false;
                helichange.Interval = 700;
                helichange.Enabled = true;
                speed = 10;
                panel1.BackgroundImage = Properties.Resources.night2;
                pictureBox1.Image = Properties.Resources.asteroid2;
                pictureBox5.Image = Properties.Resources.asteroid2;
                pictureBox3.Image = Properties.Resources.asteroid2;
                pictureBox7.Image = Properties.Resources.asteroid2;
                pictureBox6.Image = Properties.Resources.asteroid2;
                pictureBox2.Image = Properties.Resources.asteroid2;
                pictureBox4.Image = Properties.Resources.asteroid2;

            }
            else
            {
                lvltime = 30;
                helichange.Enabled = false;
                helichange.Interval = 400;
                helichange.Enabled = true;
                speed = 15;
        
                backchange.Enabled = true;
               

            }
            label14.Text = lvltime.ToString();
            pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            label13.Text = 0.ToString();


        }


    }
}
