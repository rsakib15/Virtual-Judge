using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using MetroFramework;
using System.Windows.Forms;

namespace VirtualJudge
{
    class Database
    {
        private string connection= "Data Source=DESKTOP-NLJHH8C;" + "Initial Catalog=OnlineJudge;" + "Integrated Security=SSPI";

        public bool AddUser(string username, string firstname, string lastname, string password, string passwordconfirm, string email)
        {
            SqlConnection thisConnection = new SqlConnection(connection);
            thisConnection.Open();
         
            SqlDataAdapter thisAdapter = new SqlDataAdapter("SELECT * FROM users", thisConnection);
            SqlCommandBuilder thisBuilder = new SqlCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "users");

            DataRow thisRow = thisDataSet.Tables["users"].NewRow();
            try
            {
                thisRow["username"] = username;
                thisRow["firstname"] = firstname;
                thisRow["lastname"] = lastname;
                thisRow["password"] = password;
                thisRow["email"] = email;

                thisDataSet.Tables["users"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "users");
                
            }
            catch (Exception ex)
            {
            }
            thisConnection.Close();
            return false;

        }

        public void UserLogin(string Username,string Password)
        {
            try
            {

                SqlConnection thisConnection = new SqlConnection(connection);
                thisConnection.Open();

                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConnection;
                thisCommand.CommandText = "SELECT * FROM users WHERE username='" + Username + "' AND password='" + Password + "'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                if (thisReader.Read())
                {
                    MessageBox.Show("username or password correct");
                    Session.setUserName(thisReader["username"].ToString());
                    MessageBox.Show(Session.getLoggedName());
                }
                else
                {
                    MessageBox.Show("username or password incorrect");
                }
                //this.Close();
                thisConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        public void GetSessionInformation()
        {

        }



        
    }
}
