using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameErgasia
{
    public partial class Form1 : Form
    {
        public object ParseException { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "GameDataBase.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose(); 
            }
             listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");

            
        }
        bool notexist(string nameadd)
        {
            bool flag = true;
            string[] tmp = new string[5];
            foreach (var line in File.ReadLines("GameDataBase.txt"))
            {
                tmp = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); 
               if(nameadd == tmp[0])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != "")
            {
                if (notexist(textBox1.Text))
                {
                    File.AppendAllText("GameDataBase.txt",
                        textBox1.Text + " " + 0 + " " + 0 + " " + 0 + " " + "Never_Played" + Environment.NewLine);
                    listBox1.DataSource = null;
                    listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");
                    textBox1.Focus();
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Name Already Exists", "Error", MessageBoxButtons.OK);
                    textBox1.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter a name to add.", "Error", MessageBoxButtons.OK);
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = listBox1.Items.IndexOf(listBox1.SelectedItem);
                string item = listBox1.Items[idx].ToString();
                string[] tmp; 
                tmp = item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if ((tmp.Length == 5 || tmp.Length == 6) && item != " ")
                {
                    Form2 myForm2 = new Form2(tmp[0]);
                    myForm2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error: Missing Informations Please Check the GameDataBase.txt", "Error", MessageBoxButtons.OK);
                }
            }catch (Exception ex) when (ex is ArgumentNullException || ex is IndexOutOfRangeException)
            {
                MessageBox.Show("Error: Missing Informations", "Error", MessageBoxButtons.OK);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
 
                Application.Exit();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idx2 = listBox1.Items.IndexOf(listBox1.SelectedItem);
                string item2 = listBox1.Items[idx2].ToString();
                string[] tmp2;
               tmp2 = item2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string[] lines2 = File.ReadAllLines("GameDataBase.txt");
                string[] newArray = new string[lines2.Length - 1];
                int indexx = 0;
                for (int i = 0; i < lines2.Length; i++)
                {
                    string[] tmp3;
                    tmp3 = lines2[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); ;
                    if (tmp2[0] != tmp3[0])
                    {
                        newArray[indexx] = lines2[i];
                        indexx++;
                    }
                }
                File.WriteAllLines("GameDataBase.txt", newArray);
                listBox1.DataSource = null;
                listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");
            }catch(ArgumentException et)
            {
                MessageBox.Show("Cant Delete an empty profile", "Error", MessageBoxButtons.OK);
            }
            
        }
         void FileIsValid()
        {
            bool flag = true;
            string[] lines3 = File.ReadAllLines("GameDataBase.txt");
            string[] allnames = new string[lines3.Length];
            int kk = 0;
            foreach (string line in lines3)
    
            {
                string[] tmp3;
                tmp3 = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); ;
                allnames[kk] = tmp3[0];
                if((tmp3.Length != 5 && tmp3.Length !=6 ) || line == " ")
                {
                    flag = false;
                }
                kk += 1;
            }
            for(int j = 0; j < allnames.Length-1; j++)
            {
                string temp = allnames[j];
                for(int g = j + 1; g < allnames.Length - j; g++)
                {
                    if(temp == allnames[g])
                    {
                        flag = false;
                    }
                }
            }
            if(flag == false)
            {
                 File.Delete("GameDataBase.txt");
                 File.Create("GameDataBase.txt").Dispose();
                 listBox1.DataSource = null;
                 listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Warning:You can only modify the variables(name must be unique) and delete the last line anything else will cause to loss of the data", "Warning", MessageBoxButtons.OK);
            var process =  Process.Start("GameDataBase.txt");
            process.WaitForExit();
            listBox1.DataSource = null;
            listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");
            FileIsValid();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete all the profiles?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete("GameDataBase.txt");
                File.Create("GameDataBase.txt").Dispose();
                listBox1.DataSource = null;
                listBox1.DataSource = File.ReadAllLines("GameDataBase.txt");
            }
        }
    }
}
