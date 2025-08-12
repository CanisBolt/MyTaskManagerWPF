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
    public class AddTaskVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskPriority { get; set; }
        public ICommand AddTaskCommand { get; set; }

        public AddTaskVM()
        {
            AddTaskCommand = new RelayCommands(AddTask, CanAddTask);
        }

        private void AddTask(object obj)
        {
            TaskManagerData.AddActiveTask(new UserTask(Name, Description, DateTime.Now, UserTask.GetTaskPriority(TaskPriority)));
        }

        private bool CanAddTask(object obj)
        {
            return true;
        }
    }
}
