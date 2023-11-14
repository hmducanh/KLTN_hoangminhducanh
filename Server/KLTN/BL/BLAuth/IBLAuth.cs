using BO;

namespace BL
{
    public interface IBLAuth : IBLBase
    {
        public bool CheckAccountExist(Employee employee);

        public bool AddUser(Employee employee);
    }
}
