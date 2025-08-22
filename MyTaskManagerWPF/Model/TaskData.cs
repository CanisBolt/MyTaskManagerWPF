using System.Collections.ObjectModel;

namespace MyTaskManagerWPF.Model
{
    public class TaskData
    {
        public ObservableCollection<UserTask> ActiveTasks { get; set; }
        public ObservableCollection<UserTask> ArchiveTasks { get; set; }
    }
}
