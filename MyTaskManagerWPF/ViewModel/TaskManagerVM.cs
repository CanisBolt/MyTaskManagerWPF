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

        public ICommand ShowWindowCommand { get; set; }

        public TaskManagerVM()
        {
            ActiveTasks = TaskManagerData.GetActiveTasks();
            ArchiveTasks = TaskManagerData.GetArchiveTasks();

            ShowWindowCommand = new RelayCommands(ShowWindow, CanShowWindow);
        }

        private void ShowWindow(object obj)
        {
            MainWindow addTaskWindow = new MainWindow();
            addTaskWindow.Show();
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }


        
    }
}
