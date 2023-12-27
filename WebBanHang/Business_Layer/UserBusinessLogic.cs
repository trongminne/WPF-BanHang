using System.Collections.Generic;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.UserDataAccess;

namespace WebBanHang.Business_Layer
{
    public class UserBusinessLogic
    {
        private readonly UserDataAccess userDataAccess;

        public UserBusinessLogic()
        {
            userDataAccess = new UserDataAccess();
        }

        public List<User> GetUsers()
        {
            return userDataAccess.GetUsers();
        }
    }
}
