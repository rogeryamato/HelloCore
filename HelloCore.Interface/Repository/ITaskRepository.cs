using HelloCore.DomainModel;
using HelloCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Interface.Repository
{
    public interface ITaskRepository : IBaseRepository<Task, int>
    {

    }
}
