using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails
{
    class User
    {
        private static string myConn = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        private const string ValidateQuery = "Select UserID from dbo.Users where Username=@Username and Password=@Password";
        private const string InsertQuery = "Insert Into dbo.Users(Username, Password) Values(@Username, @Password)";

        // Returns UserID on success, 0 on failure
        public int ValidateLogin(string username, string password)
        {
            int userId = 0;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(ValidateQuery, con))
                {
                    com.Parameters.AddWithValue("@Username", username);
                    com.Parameters.AddWithValue("@Password", password);
                    var result = com.ExecuteScalar();
                    if (result != null)
                        userId = Convert.ToInt32(result);
                }
            }
            return userId;
        }

        public bool RegisterUser(string username, string password)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(InsertQuery, con))
                {
                    com.Parameters.AddWithValue("@Username", username);
                    com.Parameters.AddWithValue("@Password", password);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }
    }
}