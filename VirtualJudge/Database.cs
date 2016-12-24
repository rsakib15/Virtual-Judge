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

        public void createAccount(string s)
        {
            SqlConnection thisConnection = new SqlConnection(connection);
            thisConnection.Open();

            SqlDataAdapter thisAdapter = new SqlDataAdapter("SELECT * FROM accounts", thisConnection);
            SqlCommandBuilder thisBuilder = new SqlCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "accounts");

            DataRow thisRow = thisDataSet.Tables["accounts"].NewRow();
            try
            {
                thisRow["username"] = s;
                thisDataSet.Tables["accounts"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "accounts");
            }
            catch (Exception ex)
            {
            }
            thisConnection.Close();
        }

        public bool UserLogin(string Username,string Password)
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
                    return true;
                    
                }
                else
                {
                    MessageBox.Show("username or password incorrect");
                    thisConnection.Close();
                    return false;

                }

                thisConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
             
            }
            
            return false;
        }


        public bool CheckAvailableUser(string Username)
        {
            SqlConnection thisConnection = new SqlConnection(connection);
            thisConnection.Open();

            SqlCommand thisCommand = new SqlCommand();
            thisCommand.Connection = thisConnection;
            thisCommand.CommandText = "SELECT * FROM users WHERE username='" + Username +  "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();

            if (thisReader.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public void getUserInfo(ref string userid, ref string FirstName, ref string lastName,ref string email,ref string sp,ref string cf,ref string uva)
        {
            try
            {

                SqlConnection thisConnection = new SqlConnection(connection);
                thisConnection.Open();

                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConnection;
                thisCommand.CommandText = "SELECT * FROM users WHERE username='" + Session.getLoggedName() + "'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                if (thisReader.Read())
                {
                    userid = (thisReader["username"].ToString());
                    FirstName =(thisReader["firstname"].ToString());
                    lastName= (thisReader["lastname"].ToString());
                    email= (thisReader["email"].ToString());
                }
                thisConnection.Close();
                thisConnection.Open();
                thisCommand.CommandText = "SELECT * FROM accounts WHERE username='" + Session.getLoggedName() + "'";
                thisReader = thisCommand.ExecuteReader();

                if (thisReader.Read())
                {
                    sp = (thisReader["spojID"].ToString());
                    cf = (thisReader["codeforcesID"].ToString());
                    uva = (thisReader["UVAID"].ToString());
                }

                thisConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        public bool updateInformation(string Firstname,string Lastname,string PassOne,string PassTwo,string Email)
        {
            try
            {
                SqlConnection thisConnection = new SqlConnection(connection);
                thisConnection.Open();

                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConnection;

                if (PassOne==PassTwo && PassTwo!="" && PassOne != "")
                {
                    
                    using (SqlCommand cmd = new SqlCommand("UPDATE users SET password=@pass,firstname=@fn, lastname=@ln,email=@em" +
                        " WHERE username=@un", thisConnection))
                    {
                        cmd.Parameters.AddWithValue("@un", Session.getLoggedName());
                        cmd.Parameters.AddWithValue("@pass", PassOne);
                        cmd.Parameters.AddWithValue("@fn", Firstname);
                        cmd.Parameters.AddWithValue("@ln", Lastname);
                        cmd.Parameters.AddWithValue("@em", Email);
                        cmd.ExecuteNonQuery();
                    }
                    thisConnection.Close();
                    return true;
                }
                else if(PassOne == PassTwo && PassTwo == "" && PassOne == "")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE users SET firstname=@fn, lastname=@ln,email=@em" +
                        " WHERE username=@un", thisConnection))
                    {
                        cmd.Parameters.AddWithValue("@un", Session.getLoggedName());
                        cmd.Parameters.AddWithValue("@fn", Firstname);
                        cmd.Parameters.AddWithValue("@ln", Lastname);
                        cmd.Parameters.AddWithValue("@em", Email);
                        cmd.ExecuteNonQuery();
                    }
                    thisConnection.Close();
                    return true;
                }
                else
                {
                    return false;
                }             

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void updateJudgeAccount(string sp,string cf,string uv)
        {
            SqlConnection thisConnection = new SqlConnection(connection);
            thisConnection.Open();

            SqlCommand thisCommand = new SqlCommand();
            thisCommand.Connection = thisConnection;
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE accounts SET username=@un, codeforcesID=@c,UVAID=@u,spojID=@s" +
                         " WHERE username=@un", thisConnection))
                {
                    cmd.Parameters.AddWithValue("@un", Session.getLoggedName());
                    cmd.Parameters.AddWithValue("@s", sp);
                    cmd.Parameters.AddWithValue("@c", cf);
                    cmd.Parameters.AddWithValue("@u", uv);
                    cmd.ExecuteNonQuery();
                }
                thisConnection.Close();

            }
            catch (Exception ex)
            {
               
            }          
            
        }
    }
}
