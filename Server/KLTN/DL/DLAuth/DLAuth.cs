using BO;
using MySqlConnector;

namespace DL
{
    public class DLAuth : DLBase, IDLAuth
    {
        public DLAuth() { }

        public bool CheckAccountExist(Employee employee)
        {
            string query = $"SELECT * FROM employee WHERE UserName = '{employee.Username}' AND Password = '{employee.Password}'";
            if(!string.IsNullOrEmpty(employee.Email))
            {
                query += $"AND Email = '{employee.Email}'";
            }
            return GetList<Employee>(query).Count == 1;
        }

        public bool AddUser(Employee employee)
        {
            con.Open();
            try
            {
                MySqlCommand comm = con.CreateCommand();
                comm.CommandText = "INSERT INTO employee(username,password, email) VALUES(@username, @password, @email)";
                comm.Parameters.AddWithValue("@username", employee.Username);
                comm.Parameters.AddWithValue("@password", employee.Password);
                comm.Parameters.AddWithValue("@email", employee.Email);
                comm.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                con.Close();
                return false;
            }
        }
    }
}
