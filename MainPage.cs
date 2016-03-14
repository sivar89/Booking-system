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
    public partial class MainPage : Form
    {

        //https://msdn.microsoft.com/en-us/library/bb397914.aspx
        DataTable MyDateT = new DataTable();
        private string username;
        private string pass;
        LinqStatement ls = new LinqStatement();

        public MainPage(String username, String pass)
        {
            InitializeComponent();
            this.username = username;
            this.pass = pass;

            compareLists();           
            label3.Text = "Välkommen " + username;
            label2.Text = "Dina bokade tider:";

            fillTheList();
            deleteLastYear();
        }

        public MainPage()
        {

        }

        private void monthCAL_DateChanged(object sender, DateRangeEventArgs e)
        {
            compareLists();
        }

        public void deleteLastYear()
        {
            DateTime dtm = DateTime.Now;            
            dtm = dtm.AddYears(-1);
            Console.WriteLine(dtm.Year.ToString());
            //MySqlConnection connetionString = new MySqlConnection("datasource=localhost; database=bokning;port=3306;username=root;password=666666");
            //string test = ("select name, day, time from system where name='" + username + "' and password='" + pass + "'");
            //MySqlCommand cmd = new MySqlCommand(test, connetionString);

            //MySqlDataReader MyReader2;
            //try
            //{
            //    connetionString.Open();
            //    MyReader2 = cmd.ExecuteReader();
            //    while (MyReader2.Read())
            //    {
            //        lastYear = MyReader2.GetString("day");
            //        dtm = Convert.ToDateTime(lastYear);
            //        dtm = dtm.AddYears(-1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //connetionString.Close();

            //try
            //{
            //    string query = "delete from system where YEAR(day)='" + dtm.Year.ToString() + "'";
            //    MySqlCommand MyCommand2 = new MySqlCommand(query, connetionString);
            //    MySqlDataReader MyReader3;
            //    connetionString.Open();
            //    MyReader3 = MyCommand2.ExecuteReader();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //connetionString.Close();
        }

        public void fillTheList()
        {
            DataClasses1DataContext dcc = new DataClasses1DataContext();
            var test = from user in dcc.systems
                       where user.name == username && user.password == pass
                       select user;
            foreach (var meep in test)
            {
                string listItem = meep.day + " - " + meep.time;
                listBox2.Items.Add(listItem);
            }


        }

        public void compareLists()
        {
            string date = monthCAL.SelectionStart.Date.ToString("yyyy-MM-dd");
            
             var queryDB =  ls.compareTime(date);
            if (queryDB.Count() > 0)
            {
                addToList();
                foreach (var dr in queryDB)
                {
                    for (int i = 0; i < listB1.Items.Count; i++)
                    {
                        if (listB1.Items[i].ToString().Equals(dr.time))
                        {
                            listB1.Items.Remove(listB1.Items[i].ToString());
                            break;
                        }
                    }
                }
            }
            else
            {
                addToList();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void addToList()
        {
            listB1.Items.Clear();
            listB1.Items.Add("08:00-12:00");
            listB1.Items.Add("12:00-16:00");
            listB1.Items.Add("16:00-20:00");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listB1.SelectedIndex == -1)
            {
                if (listB1.Items.Count > 0)
                {
                    Console.WriteLine(MessageBox.Show("Välj en tid i listan!"));
                }
                else {
                    Console.WriteLine(MessageBox.Show("Det finns inga tid tillgängliga"));
                }
            }
            else {               
                    string selectedTime = listB1.SelectedItem.ToString();
                    string date = monthCAL.SelectionStart.Date.ToString("yyyy-MM-dd");
                    DialogResult dialogResult = MessageBox.Show("Vill du boka den " + monthCAL.SelectionStart.Date.ToString("yyyy-MM-dd") + " kl: " + selectedTime, "Bekräfta tid", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                    else {

                        ls.insertStatement(username, pass, date, selectedTime);                       
                        MessageBox.Show("Tid bokad");
                        listB1.Items.Remove(selectedTime);
                        listBox2.Items.Add(selectedTime);
                        listBox2.Items.Clear();
                        fillTheList();
                    }                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (listBox2.SelectedIndex == -1)
            {
                if (listBox2.Items.Count > 0)
                {
                    Console.WriteLine(MessageBox.Show("Välj en tid i listan!"));
                }
                else {
                    Console.WriteLine(MessageBox.Show("Det finns inga tid tillgängliga"));
                }
            }
            else {               
                    string input = listBox2.SelectedItem.ToString();
                    string daySub = input.Substring(0, 10);
                    string timeSub = input.Substring(13, 11);

                    ls.deleteStatement(daySub, timeSub);
                    MessageBox.Show("Tid är avbokad");

                    if (daySub.Equals(monthCAL.SelectionStart.Date.ToString("yyyy-MM-dd")))
                    {
                        listB1.Items.Add(timeSub);
                    }
                    listBox2.Items.Remove(listBox2.SelectedItem.ToString());
                    listBox2.Items.Clear();
                    fillTheList();               
            }

        }
    }

}