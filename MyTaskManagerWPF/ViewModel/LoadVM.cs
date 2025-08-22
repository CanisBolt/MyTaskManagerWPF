using Microsoft.Win32;
using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace MyTaskManagerWPF.ViewModel
{
    public class LoadVM
    {
        const string SaveDirectory = "Saves";
        private TaskManagerVM taskManagerVM;
        public ICommand LoadCommand { get; set; }

        public LoadVM(TaskManagerVM _taskManagerVM)
        {
            taskManagerVM = _taskManagerVM;

            LoadCommand = new RelayCommands(async loadName => await Load(), CanLoadTask);
        }

        private bool CanLoadTask(object obj)
        {
            if (!Directory.Exists(SaveDirectory))
            {
                MessageBox.Show(LocalizationManager.GetString("ErrorSaveDirectoryNotFound"));
                return false;
            }
            return true;
        }

        public async Task Load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetFullPath(SaveDirectory);
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                if (taskManagerVM.ActiveTasks.Any() || taskManagerVM.ArchiveTasks.Any())
                {
                    MessageBoxResult result = MessageBox.Show(LocalizationManager.GetString("CurrentDataWillBeErased"), LocalizationManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show(LocalizationManager.GetString("LoadCancelled"));
                        return;
                    }
                }

                try
                {
                    string json = await File.ReadAllTextAsync(filePath);
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        MessageBox.Show(LocalizationManager.GetString("LoadErrorFileEmpty"));
                        return;
                    }

                    var loadedData = JsonSerializer.Deserialize<TaskData>(json);

                    if (loadedData == null)
                    {
                        MessageBox.Show(LocalizationManager.GetString("LoadFailed"));
                        return;
                    }

                    taskManagerVM.ActiveTasks.Clear();
                    foreach (var task in loadedData.ActiveTasks)
                    {
                        taskManagerVM.ActiveTasks.Add(task);
                    }

                    taskManagerVM.ArchiveTasks.Clear();
                    foreach (var task in loadedData.ArchiveTasks)
                    {
                        taskManagerVM.ArchiveTasks.Add(task);
                    }

                    MessageBox.Show(LocalizationManager.GetString("DataLoadedSuccessfully"));
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(LocalizationManager.GetString("ErrorFileNotFound"));
                }
                catch (JsonException)
                {
                    MessageBox.Show(LocalizationManager.GetString("ErrorCorruptedJson"));
                }
                catch (Exception)
                {
                    MessageBox.Show(LocalizationManager.GetString("ErrorGeneric"));
                }
            }
        }
    }
}
