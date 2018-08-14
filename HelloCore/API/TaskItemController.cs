using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCore.DomainModel;
using HelloCore.Interface.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebExtension;

namespace HelloCore.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("admin")]
    public class TaskItemController : BaseApiController
    {
        private readonly ITaskItemManager taskItemManager;
        public TaskItemController(ITaskItemManager taskItemManager)
        {
            this.taskItemManager = taskItemManager;
        }

        [HttpGet]
        public TaskItem Get(int id)
        {
            return new TaskItem()
            {
                Id = 1,
                Description = "task item",
                Name = "come on",
                Priority = 100,
                TaskId = 1
            };
        }


    }
}