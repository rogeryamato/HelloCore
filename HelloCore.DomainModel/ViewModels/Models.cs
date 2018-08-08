using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.DomainModel
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
    
    public class TaskConditionModel : PageCondition
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class TaskItemModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
    
    public class TaskItemConditionModel: PageCondition
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class PageCondition
    {
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public List<string> SortFields { get; set; }

        public bool OrderDESC { get; set; }

        public List<string> Includes { get; set; }
    }


    public class ResultEntity
    {
        public ResultEntity():this("")
        {
            IsSuccess = true;
        }

        public ResultEntity(Exception e):this(e.ToString())
        {
        }

        public ResultEntity(string error)
        {
            errors = new List<string>();

            if (!string.IsNullOrWhiteSpace(error))
                errors.Add(error);
            else
                IsSuccess = true;
        }

        public bool IsSuccess { get; set; }

        public string Error
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in errors)
                {
                    sb.AppendLine(item);
                }
                return sb.ToString();
            }
        }

        private List<string> errors { get; set; }

        public void AddError(string error)
        {
            IsSuccess = false;
            errors.Add(error);
        }
    }

    public class ResultEntity<T> : ResultEntity
    {
        public T Entity { get; set; }

        public ResultEntity():base()
        {
            Entity = Activator.CreateInstance<T>();
        }
    }

}
