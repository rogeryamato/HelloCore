using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using HelloCore.Interface.Manager;
using HelloCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HelloCore.Manager
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

        public async System.Threading.Tasks.Task<PageList<Task>> GetTasks(TaskConditionModel model)
        {
            var expressions = new List<Expression<Func<Task, bool>>>();

            if (!string.IsNullOrWhiteSpace(model.Name))
                expressions.Add(s => s.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Description))
                expressions.Add(s => s.Description.Contains(model.Description));

            return await repository.Get(expressions,model as PageCondition);
        }

        public ResultEntity<Task> UpdateTask(TaskModel task)
        {
            ResultEntity<Task> result = new ResultEntity<Task>();

            var data = repository.Get(task.Id);
            if (data != null)
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

}
