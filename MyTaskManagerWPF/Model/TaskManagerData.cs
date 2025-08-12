using System.Collections.ObjectModel;

namespace MyTaskManagerWPF.Model
{
    public class TaskManagerData
    {
        private static ObservableCollection<UserTask> ActiveTasks { get; set; } = new ObservableCollection<UserTask>();
        private static ObservableCollection<UserTask> ArchiveTasks { get; set; } = new ObservableCollection<UserTask>();

        public TaskManagerData()
        {

        }

        public static ObservableCollection<UserTask> GetActiveTasks()
        {
            return ActiveTasks;
        }

        public static void AddActiveTask(UserTask task)
        {
            ActiveTasks.Add(task);
        }
        public static void RemoveActiveTask(UserTask task)
        {
            ActiveTasks.Remove(task);
        }

        public static ObservableCollection<UserTask> GetArchiveTasks()
        {
            return ArchiveTasks;
        }

        public static void AddArchiveTask(UserTask task)
        {
            ArchiveTasks.Add(task);
        }
    }
}
