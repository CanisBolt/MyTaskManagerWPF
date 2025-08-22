using Microsoft.Win32;
using MyTaskManagerWPF.Commands;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace MyTaskManagerWPF.ViewModel
{
    public class SaveVM
    {
        // Saving working, but has MessageBox in it.
        // Need to fix it.
        const string SaveDirectory = "Saves";
        private TaskManagerVM taskManagerVM;
        public ICommand SaveCommand { get; set; }

        public SaveVM(TaskManagerVM _taskManagerVM)
        {
            taskManagerVM = _taskManagerVM;

            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            SaveCommand = new RelayCommands(async saveName => await SaveTasksToFile(), CanSaveTask);
        }

        public async Task SaveTasksToFile()
        {
            if (!taskManagerVM.ActiveTasks.Any())
            {
                MessageBox.Show(LocalizationManager.GetString("NoTasksFound"));
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() != true)
            {
                MessageBox.Show(LocalizationManager.GetString("SaveCancelled"));
                return;
            }

            string fullSavePath = saveFileDialog.FileName;

            try
            {
                var dataToSave = new
                {
                    ActiveTasks = taskManagerVM.ActiveTasks,
                    ArchiveTasks = taskManagerVM.ArchiveTasks
                };
                string json = JsonSerializer.Serialize(dataToSave, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(fullSavePath, json);

                MessageBox.Show(LocalizationManager.GetString("TasksSuccessfullySaved"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LocalizationManager.GetString("ErrorGeneric")}\n{ex.Message}");
            }
        }

        private bool CanSaveTask(object obj)
        {
            return taskManagerVM.ActiveTasks.Any();
        }
    }
}
