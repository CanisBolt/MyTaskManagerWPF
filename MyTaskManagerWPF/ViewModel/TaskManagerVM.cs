using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTaskManagerWPF.ViewModel
{
    public class TaskManagerVM
    {
        public ObservableCollection<UserTask> ActiveTasks { get; set; }
        public ObservableCollection<UserTask> ArchiveTasks { get; set; }
        public UserTask SelectedTask { get; set; }
        public ICommand ShowAddWindowCommand { get; set; }
        public ICommand ShowEditWindowCommand { get; set; }

        public TaskManagerVM()
        {
            ActiveTasks = TaskManagerData.GetActiveTasks();
            ArchiveTasks = TaskManagerData.GetArchiveTasks();

            ShowAddWindowCommand = new RelayCommands(ShowAddWindow, CanShowWindow);
            ShowEditWindowCommand = new RelayCommands(ShowEditWindow, CanShowWindow);
        }

        private void ShowAddWindow(object obj)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.Show();
        }

        private void ShowEditWindow(object obj)
        {
            if (obj is UserTask selectedTask)
            {
                EditTaskVM editTaskVM = new EditTaskVM(selectedTask);
                EditTaskWindow editTaskWindow = new EditTaskWindow();
                editTaskWindow.DataContext = editTaskVM;
                editTaskWindow.ShowDialog();
            }
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }
    }
}
