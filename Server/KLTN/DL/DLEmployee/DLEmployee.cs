using BO;
using System.Data.Common;
using System.Data;
using MySqlConnector;

namespace DL
{
    public class DLEmployee : DLBase, IDLEmployee
    {
        public DLEmployee()
        {

        }
        public List<Employee> GetEmployee()
        {
            string query = "SELECT * FROM employee";
            return GetList<Employee>(query);
        }
    }
}
