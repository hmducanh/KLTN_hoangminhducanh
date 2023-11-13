using BO;
using DL;

namespace BL
{
    public class BLEmployee : BLBase, IBLEmployee
    {
        private IDLEmployee _dlEmployee;

        public BLEmployee(IDLEmployee dlEmployee)
        {
            _dlEmployee = dlEmployee;
        }

        public List<Employee> GetEmployee()
        {
            return _dlEmployee.GetEmployee();
        }
    }
}
