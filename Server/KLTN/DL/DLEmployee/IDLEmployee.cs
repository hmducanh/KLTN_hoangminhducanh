using BO;

namespace DL
{
    public interface IDLEmployee : IDLBase
    {
        public List<Employee> GetEmployee();
    }
}
