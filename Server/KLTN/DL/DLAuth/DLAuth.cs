using BO;

namespace DL
{
    public class DLAuth : DLBase, IDLAuth
    {
        public DLAuth() { }

        public bool CheckAccountExist(Employee employee)
        {
            string query = $"SELECT * FROM employee WHERE UserName = '{employee.Username}' AND Password = '{employee.Password}'";
            return GetList<Employee>(query).Count == 1;
        }
    }
}
