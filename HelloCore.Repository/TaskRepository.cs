using HelloCore.DomainModel;
using HelloCore.Interface;
using HelloCore.Interface.Repository;

namespace HelloCore.Repository
{
    public class TaskRepository : BaseRepository<Task, int>, ITaskRepository
    {
        public TaskRepository() : base()
        {

        }
    }
}
