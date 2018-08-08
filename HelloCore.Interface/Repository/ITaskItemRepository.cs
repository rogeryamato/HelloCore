using HelloCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Interface.Repository
{
    public interface ITaskItemRepository : IBaseRepository<TaskItem, int>
    {

    }
}
