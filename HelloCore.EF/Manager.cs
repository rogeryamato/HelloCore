using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace HelloCore.EF
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository repository;
        public TaskManager(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public Task AddTask(TaskModel task)
        {
            var t = new Task()
            {
                Name = task.Name,
                Description = task.Description,
                TaskItems = new List<TaskItem>()
            };
            repository.Add(t);
            return t;
        }

        public void DeleteTask(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return repository.GetAll();
        }

        public Task GetTask(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<Task> GetTasks(TaskConditionModel model)
        {
            var expressions = new List<Expression<Func<Task, bool>>>();

            if (!string.IsNullOrWhiteSpace(model.Name))
                expressions.Add(s => s.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Description))
                expressions.Add(s => s.Description.Contains(model.Description));

            return repository.Get(expressions);
        }

        public ResultEntity<Task> UpdateTask(TaskModel task)
        {
            ResultEntity<Task> result = new ResultEntity<Task>();

            var data = repository.Get(task.Id);
            if(data!=null)
            {
                data.Name = task.Name;
                data.Description = task.Description;
                repository.Update(data);
                result.Entity = data;
            }
            else
            {
                result.AddError("Task not found.");
            }

            return result;
        }
    }


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
            return repository.Get(s=>s.TaskId == taskId);
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

        public IEnumerable<TaskItem> GetTaskItems(TaskItemConditionModel model)
        {
            List<Expression<Func<TaskItem, bool>>> expressions = new List<Expression<Func<TaskItem, bool>>>();
            if (!string.IsNullOrWhiteSpace(model.Name))
                expressions.Add(s => model.Name.Contains(s.Name));
            if (!string.IsNullOrWhiteSpace(model.Description))
                expressions.Add(s => model.Description.Contains(model.Description));

            return repository.Get(expressions);
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
