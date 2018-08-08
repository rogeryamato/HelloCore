using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Interface.Manager
{
    public interface ITaskManager
    {
        Task AddTask(TaskModel task);

        ResultEntity<Task> UpdateTask(TaskModel task);

        void DeleteTask(int id);

        Task GetTask(int id);

        System.Threading.Tasks.Task<PageList<Task>> GetTasks(TaskConditionModel model);

        IEnumerable<Task> GetAll();
    }
}
