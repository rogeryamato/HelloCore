using System;
using System.Collections.Generic;
using System.Linq;
using HelloCore.DomainModel;
using HelloCore.Interface.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebExtension;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloCore.API
{
    [Authorize]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {
        private readonly ITaskManager taskManager;
        public TaskController(ITaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            var tasks = taskManager.GetAll();

            return tasks;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            return taskManager.GetTask(id);
        }

        // POST api/<controller>
        // frombody and fromForm is mandatory
        [HttpPost]
        public void Post([FromBody]TaskModel model)
        {
            taskManager.AddTask(model);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody]TaskModel model)
        {
            taskManager.UpdateTask(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            taskManager.DeleteTask(id);
        }

    }
}
