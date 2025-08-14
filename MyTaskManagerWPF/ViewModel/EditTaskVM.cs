using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTaskManagerWPF.ViewModel
{
    public class EditTaskVM
    {
        private UserTask _originalTask;

        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskPriority { get; set; }
        public ICommand EditTaskCommand { get; set; }

        public EditTaskVM(UserTask task)
        {
            _originalTask = task;

            Name = task.Name;
            Description = task.Description;
            TaskPriority = task.TaskPriority.ToString();

            EditTaskCommand = new RelayCommands(EditTask, CanEditTask);
        }

        private void EditTask(object obj)
        {
            _originalTask.Name = Name;
            _originalTask.Description = Description;
            _originalTask.TaskPriority = UserTask.GetTaskPriority(TaskPriority);
        }

        private bool CanEditTask(object obj)
        {
            return true;
        }
    }
}
