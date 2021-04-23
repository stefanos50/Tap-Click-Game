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

namespace GameErgasia
{

    public partial class Form2 : Form
    {
        static string score1,score2,score3,usrname,lvl,sc;
        static bool close;
        public Form2(string name)
        {
            InitializeComponent();
            close = true;
            label18.Text = name;
            loadUserInfo(name);
        }
        void loadUserInfo(string searchname)
        {
            string[] lines2 = File.ReadAllLines("GameDataBase.txt");
            foreach (string line in lines2)
            {
                string[] tmp2;
                tmp2 = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); 
                if(tmp2[0] == searchname)
                {
                    label2.Text = tmp2[0];
                    if(tmp2[4] == "Never_Played")
                    {
                        label8.Text = tmp2[4];
                    }
                    else
                    {
                        label8.Text = tmp2[4] + " " + tmp2[5];
                    }
                   
                    score1 = tmp2[1];
                    score2 = tmp2[2];
                    score3 = tmp2[3];
                    usrname =tmp2[0];
                    break;
                }
            }

        }
        void TopPlayers(int clvl)
        {
            string[] lines3 = File.ReadAllLines("GameDataBase.txt");
            string[] tmp5;
            var myList = new List<KeyValuePair<int, string>>();
           
            for (int i = 0; i < lines3.Length; i++)
            {
                tmp5 = lines3[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if(clvl == 1)
                {
                    try
                    {
                        myList.Add(new KeyValuePair<int, string>(int.Parse(tmp5[1]), tmp5[0]));
                    }
                    catch (System.FormatException)
                    {

                    }


                   }
                else if (clvl == 2)
                {
                    try
                    {
                        myList.Add(new KeyValuePair<int, string>(int.Parse(tmp5[2]), tmp5[0]));
                    }catch(System.FormatException){
                    }
                 }
                else
                {
                    try
                    {
                        myList.Add(new KeyValuePair<int, string>(int.Parse(tmp5[3]), tmp5[0]));
                    }
                    catch (System.FormatException) { }


                 }
            }

           myList.Sort((x, y) => x.Key.CompareTo(y.Key));
             myList.Reverse();
            int stop = 0;
            foreach (KeyValuePair<int, string> pair in myList)
            {
                if (stop == 0)
                {
                    label10.Text = pair.Value;
                    label11.Text = pair.Key.ToString();
                }else if(stop == 1)
                {
                    
                    label12.Text = pair.Value;
                    label13.Text = pair.Key.ToString();
                }else if(stop == 2)
                {
                    label14.Text = pair.Value;
                    label15.Text = pair.Key.ToString();
                }
                else
                {
                   break;
                }
                stop++;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label6.Text = "2";
            label7.Text = score2;
            label9.Text = "Level2: Round last:45sec Heli change possition every 700 ms,background objects move faster and is night time";
            TopPlayers(2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label6.Text = "3";
            label7.Text = score3;
            label9.Text = "Level3: Round last:30sec Heli change possition every 400 ms ,background objects move faster and background is changing(day/night)";
            TopPlayers(3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you really want logout from this account?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                    close = false;
                    Form1 myForm1 = new Form1();
                    myForm1.Show();
                    this.Close();
                
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                Application.Exit();

             }
    }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                lvl = "1";
                sc = score1;
            }
            else if (radioButton2.Checked == true)
            {
                lvl = "2";
                sc = score2;
            }
            else if (radioButton3.Checked == true)
            {
                lvl = "3";
                sc = score3;
            }
            Form3 myForm4 = new Form3(usrname,lvl,sc);
            myForm4.Show();
            close = false;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label6.Text = "1";
            label7.Text = score1;
            label9.Text = "Level1: Round last:60sec Heli change possition every 1000 ms and is day time";
            TopPlayers(1);
        }
    }
}
