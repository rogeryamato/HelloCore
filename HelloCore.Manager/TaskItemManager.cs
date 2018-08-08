using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using HelloCore.Interface.Manager;
using HelloCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HelloCore.Manager
{
    public class TaskItemManager : ITaskItemManager
    {
        private readonly ITaskItemRepository repository;
        public TaskItemManager(ITaskItemRepository repository)
        {
            this.repository = repository;
        }
        public TaskItem AddTaskItem(TaskItemModel task)
        {
            var item = new TaskItem()
            {
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority,
                TaskId = task.TaskId
            };

            repository.Add(item);
            return item;
        }

        public void DeleteTask(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<TaskItem> GetAll(int taskId)
        {
            return repository.Get(s => s.TaskId == taskId);
        }

        public Task GetTask(int taskId)
        {
            var tasks = repository.Get(s => s.Task.Id == taskId);
            return tasks?.First().Task;
        }

        public TaskItem GetTaskItem(int itemId)
        {
            return repository.Get(itemId);
        }

        public async System.Threading.Tasks.Task<PageList<TaskItem>> GetTaskItems(TaskItemConditionModel model)
        {
            List<Expression<Func<TaskItem, bool>>> expressions = new List<Expression<Func<TaskItem, bool>>>();
            if (!string.IsNullOrWhiteSpace(model.Name))
                expressions.Add(s => model.Name.Contains(s.Name));
            if (!string.IsNullOrWhiteSpace(model.Description))
                expressions.Add(s => model.Description.Contains(model.Description));

            return await repository.Get(expressions,model as PageCondition);
        }

        public ResultEntity<TaskItem> UpdateTask(TaskItemModel task)
        {
            ResultEntity<TaskItem> result = new ResultEntity<TaskItem>();
            var taskItem = repository.Get(task.Id);

            if (taskItem != null)
            {
                taskItem.Name = task.Name;
                taskItem.Priority = task.Priority;
                taskItem.Description = task.Description;

                repository.Update(taskItem);
            }
            else
            {
                result.AddError("Task item not found.");
            }

            return result;
        }
    }
}
