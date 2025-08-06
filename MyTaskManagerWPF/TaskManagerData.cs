using System.Collections.ObjectModel;

namespace MyTaskManager
{
    public class TaskManagerData
    {
        public ObservableCollection<UserTask> ActiveTasks { get; set; } = new ObservableCollection<UserTask>();
        public ObservableCollection<UserTask> ArchiveTasks { get; set; } = new ObservableCollection<UserTask>();

        public TaskManagerData()
        {

        }
    }
}
