using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.DomainModel
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public Task Task { get; set; }
    }
}
