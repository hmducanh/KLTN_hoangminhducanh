using BO;
using DL;
using Helper;

namespace BL
{
    public class BLAuth : BLBase, IBLAuth
    {
        private IDLAuth _dlAuth;

        public BLAuth(IDLAuth dlAuth)
        {
            _dlAuth = dlAuth;
        }
        public bool CheckAccountExist(Employee employee)
        {
            if(!string.IsNullOrEmpty(employee.Password))
            {
                // ma hoa password
                employee.Password = Common.ToMD5(employee.Password);
                return _dlAuth.CheckAccountExist(employee);
            }
            else
            {
                return false;
            }
        }
    }
}
