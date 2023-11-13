using BO;

namespace DL
{
    public interface IDLAuth : IDLBase
    {
        public bool CheckAccountExist(Employee employee);
    }
}
