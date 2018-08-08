using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.DomainModel
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskItem> TaskItems { get; set; }
    }
}
