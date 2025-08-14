using Microsoft.Win32;
using MyTaskManagerWPF.Commands;
using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace MyTaskManagerWPF.ViewModel
{
    public class SaveVM
    {
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

            SaveCommand = new RelayCommands(async saveName => await SaveTasksToFile(saveName.ToString()), CanSaveTask);
        }

        public async Task SaveTasksToFile(string saveName)
        {
            if (!taskManagerVM.ActiveTasks.Any())
            {
                MessageBox.Show(LocalizationManager.GetString("NoTasksFound"));
                return;
            }


            string fullSavePath = Path.Combine(SaveDirectory, saveName);

            if (File.Exists(fullSavePath))
            {
                MessageBoxResult result = MessageBox.Show(LocalizationManager.GetString("FileExistsOverwriteWarning"), LocalizationManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await Saving(fullSavePath);
                }
                else
                {
                    MessageBox.Show(LocalizationManager.GetString("SaveCancelled"));
                    return;
                }
            }
            else
            {
                await Saving(fullSavePath);
            }
            MessageBox.Show(LocalizationManager.GetString("TasksSuccessfullySaved"));
        }

        async Task Saving(string fullSavePath)
        {
            // Wrong. Need to separate it from ViewModel
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;

                await taskManagerVM.SaveViewModel.SaveTasksToFile(fileName);
            }
            try
            {
                // Add saving
                //await File.WriteAllTextAsync(fullSavePath, json);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(string.Format(LocalizationManager.GetString("ErrorFileNotFound"), Path.GetFileName(fullSavePath)));
            }
            catch (JsonException ex)
            {
                MessageBox.Show(LocalizationManager.GetString("ErrorCorruptedJson"));
                MessageBox.Show(LocalizationManager.GetString("ErrorDetails"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationManager.GetString("ErrorGeneric"));
            }
        }

        private bool CanSaveTask(object obj)
        {
            return taskManagerVM.ActiveTasks.Any();
        }
    }
}
