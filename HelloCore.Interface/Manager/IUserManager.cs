using HelloCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Interface.Manager
{
    public interface IUserManager
    {
        ResultEntity<User> ValidateUser(string email, string password);

        System.Threading.Tasks.Task<ResultEntity<User>> GetAsync(int id);
    }
}
