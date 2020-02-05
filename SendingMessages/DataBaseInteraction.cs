using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;


namespace SendingMessages
{
   public static class DataBaseInteraction
    {       
        public static List<string> GetEmails(string serverAdress, string dbName, string tableName,string columnName)
        {
            List<string> emails = new List<string>();
            using (SqlConnection connection=new SqlConnection(@"Data Source = " + serverAdress + "; Initial Catalog = " + dbName + "; Integrated Security = True"))
            {
                try
                {                  
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Select "+columnName+" from " + tableName, connection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while(sqlDataReader.Read())
                    { emails.Add(sqlDataReader.GetValue(0).ToString()); }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();                   
                }
            }
            return emails;
        }


    }
}
