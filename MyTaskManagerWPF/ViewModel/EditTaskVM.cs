using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using System.Windows.Input;

namespace MyTaskManagerWPF.ViewModel
{
    public class EditTaskVM
    {
        public Action CloseAction { get; set; }
        public UserTask originalTask;

        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskPriority { get; set; }
        public ICommand EditTaskCommand { get; set; }

        public EditTaskVM(UserTask task)
        {
            originalTask = task;

            Name = task.Name;
            Description = task.Description;
            TaskPriority = task.TaskPriority.ToString();

            EditTaskCommand = new RelayCommands(EditTask, CanEditTask);
        }

        private void EditTask(object obj)
        {
            TaskManagerData.RemoveActiveTask(originalTask);
            TaskManagerData.AddActiveTask(new UserTask(Name, Description, DateTime.Now, UserTask.GetTaskPriority(TaskPriority)));

            CloseAction?.Invoke();
        }

        private bool CanEditTask(object obj)
        {
            return true;
        }
    }
}
