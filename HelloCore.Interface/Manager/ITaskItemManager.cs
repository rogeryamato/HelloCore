using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.Interface.Manager
{
    public interface ITaskItemManager
    {
        TaskItem AddTaskItem(TaskItemModel task);

        ResultEntity<TaskItem> UpdateTask(TaskItemModel task);

        void DeleteTask(int id);

        TaskItem GetTaskItem(int itemId);

        Task GetTask(int taskId);

        System.Threading.Tasks.Task<PageList<TaskItem>> GetTaskItems(TaskItemConditionModel model);

        IEnumerable<TaskItem> GetAll(int taskId);
    }
}
