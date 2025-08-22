using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.View;
using System.Collections.ObjectModel;
using System.Windows;
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
        public ICommand ShowSaveWindowCommand { get; set; }
        public ICommand ShowLoadWindowCommand { get; set; }
        public ICommand MarkAsCompleteCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public SaveVM SaveViewModel { get; }
        public LoadVM LoadViewModel { get; }

        public TaskManagerVM()
        {
            ActiveTasks = TaskManagerData.GetActiveTasks();
            ArchiveTasks = TaskManagerData.GetArchiveTasks();

            ShowAddWindowCommand = new RelayCommands(ShowAddWindow, CanShowWindow);
            ShowEditWindowCommand = new RelayCommands(ShowEditWindow, CanShowWindow);
            ShowSaveWindowCommand = new RelayCommands(ShowSaveWindow, CanShowWindow);
            ShowLoadWindowCommand = new RelayCommands(ShowLoadWindow, CanShowWindow);
            MarkAsCompleteCommand = new RelayCommands(MarkAsComplete, CanMarkAsComplete);
            DeleteTaskCommand = new RelayCommands(DeleteTask, CanDeleteTask);

            SaveViewModel = new SaveVM(this);
            LoadViewModel = new LoadVM(this);
        }

        private void ShowAddWindow(object obj)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.ShowDialog();
        }

        private void ShowEditWindow(object obj)
        {
            if (obj is UserTask selectedTask)
            {
                EditTaskWindow editTaskWindow = new EditTaskWindow(selectedTask);
                editTaskWindow.ShowDialog();
            }
        }

        private async void ShowSaveWindow(object obj)
        {
            await SaveViewModel.SaveTasksToFile();
        }

        private void ShowLoadWindow(object obj)
        {
            LoadViewModel.Load();
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void MarkAsComplete(object obj)
        {
            if (obj is UserTask selectedTask)
            {
                MessageBoxResult result = MessageBox.Show(LocalizationManager.GetString("MarkAsCompletedPrompt"), LocalizationManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBoxResult archive = MessageBox.Show(LocalizationManager.GetString("MoveToArchivePrompt"), LocalizationManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (archive == MessageBoxResult.Yes)
                    {
                        MessageBox.Show(LocalizationManager.GetString("TaskCompletedAndArchived"));
                        selectedTask.Completed = DateTime.Now;
                        ArchiveTasks.Add(selectedTask);
                    }
                    ActiveTasks.Remove(selectedTask);
                }
            }
        }

        private bool CanMarkAsComplete(object obj)
        {
            return true;
        }

        private void DeleteTask(object obj)
        {
            if (SelectedTask == null)
            {
                return;
            }
            if (ActiveTasks.Contains(SelectedTask))
            {
                ActiveTasks.Remove(SelectedTask);
            }
            else if (ArchiveTasks.Contains(SelectedTask))
            {
                ArchiveTasks.Remove(SelectedTask);
            }
            MessageBox.Show(LocalizationManager.GetString("TaskSuccessfullyDeleted"));
        }

        private bool CanDeleteTask(object obj)
        {
            return true;
        }
    }
}
