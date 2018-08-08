using HelloCore.DomainModel;
using HelloCore.Interface.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Manager
{
    public class UserManager : IUserManager
    {
        public async System.Threading.Tasks.Task<ResultEntity<User>> GetAsync(int id)
        {
            ResultEntity<User> result = new ResultEntity<User>();
            if (id == 1)
            {
                result.Entity = new User()
                {
                    Email = "v.w@ef.com",
                    UserId = 1,
                    UserName = "vincent",
                    UserRoleId = 1
                };
            }
            else
            {

                result.Entity = new User()
                {
                    Email = "r.w@ef.com",
                    UserId = 2,
                    UserName = "Roger",
                    UserRoleId = 2
                };
            }
            return result;
        }

        public ResultEntity<User> ValidateUser(string email, string password)
        {
            ResultEntity<User> result = new ResultEntity<User>();
            if (email == "v.w@ef.com" && password == "123")
            {
                result.Entity = new User()
                {
                    Email = "v.w@ef.com",
                    UserId = 1,
                    UserName = "vincent",
                    UserRoleId = 1
                };
            }
            else if(email == "r.w@ef.com" && password =="123")
            {

                result.Entity = new User()
                {
                    Email = "r.w@ef.com",
                    UserId = 2,
                    UserName = "Roger",
                    UserRoleId = 2
                };
            }
            else
                result.IsSuccess = false;

            return result;
        }
    }
}
