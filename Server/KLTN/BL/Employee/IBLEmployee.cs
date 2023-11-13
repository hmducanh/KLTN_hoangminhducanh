using BO;

namespace BL
{
    public interface IBLEmployee : IBLBase
    {
        public List<Employee> GetEmployee();
    }
}
