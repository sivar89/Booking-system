using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewBokningSystem
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        MainPage ms;
       static List<string> yourTimes;
        private static string username;

        public string Username
        {
            get
            {
                return username;
            }
        }

        public List<string> YourTimes
        {
            get
            {
                return yourTimes;
            }

            set
            {
                yourTimes = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void login_Click(object sender, EventArgs e)
        {
            this.Hide();
            MySqlConnection connetionString = new MySqlConnection("datasource=localhost; database=bokning;port=3306;username=root;password=666666");
            MySqlDataAdapter sda= new MySqlDataAdapter("select name, password from newsystem where name='" + textBox1.Text + "' and password='" + textBox2.Text + "'", connetionString);
                        
            sda.Fill(dt);
                       
            if (dt.Rows.Count==1)
            {
               
                
               
                    foreach (DataRow ss in dt.Rows)
                {                                                        
                        ms = new MainPage(ss["name"].ToString(), ss["password"].ToString());

                    }                                                                       
            }
            else
            {
                MessageBox.Show("Please check your username and password!");
            }
            ms.Show();
            username = textBox1.Text;
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
