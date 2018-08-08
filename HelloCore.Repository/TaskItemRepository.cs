using HelloCore.DomainModel;
using HelloCore.Interface;
using HelloCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Repository
{
    public class TaskItemRepository : BaseRepository<TaskItem, int>, ITaskItemRepository
    {
        public TaskItemRepository() : base()
        {

        }
    }

}
