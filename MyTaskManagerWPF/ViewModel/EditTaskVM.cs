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
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskPriority { get; set; }
        public ICommand EditTaskCommand { get; set; }

        public EditTaskVM()
        {
            EditTaskCommand = new RelayCommands(EditTask, CanEditTask);
        }

        private void EditTask(object obj)
        {
            TaskManagerData.AddActiveTask(new UserTask(Name, Description, DateTime.Now, UserTask.GetTaskPriority(TaskPriority)));
        }

        private bool CanEditTask(object obj)
        {
            return true;
        }
    }
}
