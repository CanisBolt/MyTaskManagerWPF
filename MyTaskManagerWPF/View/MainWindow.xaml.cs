using System.Windows;
using System.Text.Json;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Win32;
using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.ViewModel;

namespace MyTaskManagerWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManagerVM taskManagerVM = new TaskManagerVM();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = taskManagerVM;
        }


        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lvTasks.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(LocalizationManager.GetString("DeleteTask"), LocalizationManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    taskManagerVM.ActiveTasks.Remove((UserTask)lvTasks.SelectedItem);
                    MessageBox.Show(LocalizationManager.GetString("TaskSuccessfullyDeleted"));
                }
            }
        }

        private void cbLanguage_DropDownClosed(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cbLanguage.SelectedItem;
            if (selectedItem != null)
            {
                string cultureName = (string)selectedItem.Tag;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

                btnAddTask.Content = LocalizationManager.GetString("ButtonAddTask");
                btnEditTask.Content = LocalizationManager.GetString("ButtonEditTask");
                btnDeleteTask.Content = LocalizationManager.GetString("ButtonDeleteTask");
                btnLoad.Content = LocalizationManager.GetString("ButtonLoad");
                btnSave.Content = LocalizationManager.GetString("ButtonSave");
            }
        }


        /*
        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string saveName;
            string dirName = SaveDirectory;
            if (Directory.Exists(dirName))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

                if (openFileDialog.ShowDialog() == true)
                {
                    saveName = openFileDialog.FileName;

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
                        string json = await File.ReadAllTextAsync(saveName);
                        if (string.IsNullOrWhiteSpace(json))
                        {
                            MessageBox.Show(LocalizationManager.GetString("LoadErrorFileEmpty"));
                            return;
                        }
                        TaskManagerVM loadedData = JsonSerializer.Deserialize<TaskManagerVM>(json);
                        if (loadedData == null)
                        {
                            MessageBox.Show(LocalizationManager.GetString("LoadFailed"));
                            return;
                        }
                        taskManagerVM.ActiveTasks.Clear();
                        taskManagerVM.ArchiveTasks.Clear();

                        taskManagerVM.ActiveTasks = loadedData.ActiveTasks;
                        taskManagerVM.ActiveTasks = loadedData.ArchiveTasks;

                        MessageBox.Show(LocalizationManager.GetString("DataLoadedSuccessfully"));
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show(LocalizationManager.GetString("ErrorFileNotFound"));
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
            }
        }*/
    }
}