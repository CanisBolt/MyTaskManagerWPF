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
        ResourceManager resourceManager = new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());
        const string SaveDirectory = "Saves";
        TaskManagerVM taskManagerVM = new TaskManagerVM();
        public TaskManagerData taskManagerData { get; set; } = new TaskManagerData();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = taskManagerVM;
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            ResetFiledColor();

            string name = tbName.Text;
            if(name.Length == 0)
            {
                MessageBox.Show(resourceManager.GetString("TaskNameCannotBeEmpty"));
                tbName.Background = Brushes.Red;
                return;
            }

            string description = tbDescription.Text;
            if (description.Length == 0)
            {
                MessageBox.Show(resourceManager.GetString("TaskDescriptionCannotBeEmpty"));
                tbDescription.Background = Brushes.Red;
                return;
            }
            string taskPriority = cbPriority.SelectionBoxItem.ToString();

            //taskManagerData.ActiveTasks.Add(new UserTask(name, description, DateTime.Now, UserTask.GetTaskPriority(taskPriority)));
            ResetFiledValue();
            MessageBox.Show(resourceManager.GetString("TaskSuccessfullyAdded"));
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if(lvTasks.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(resourceManager.GetString("DeleteTask"), resourceManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //taskManagerData.ActiveTasks.Remove((UserTask)lvTasks.SelectedItem);
                    MessageBox.Show(resourceManager.GetString("TaskSuccessfullyDeleted"));
                }
            }
        }

        private void ResetFiledValue()
        {
            tbName.Text = string.Empty;
            tbDescription.Text = string.Empty;
        }

        private void ResetFiledColor()
        {
            tbName.Background = Brushes.White;
            tbDescription.Background = Brushes.White;
        }

        private void cbLanguage_DropDownClosed(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cbLanguage.SelectedItem;
            if (selectedItem != null)
            {
                string cultureName = (string)selectedItem.Tag;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

                btnAddTask.Content = resourceManager.GetString("ButtonAddTask");
                btnEditTask.Content = resourceManager.GetString("ButtonEditTask");
                btnDeleteTask.Content = resourceManager.GetString("ButtonDeleteTask");
                btnLoad.Content = resourceManager.GetString("ButtonLoad");
                btnSave.Content = resourceManager.GetString("ButtonSave");

                lblTaskName.Content = resourceManager.GetString("LabelTaskName");
                lblTaskText.Content = resourceManager.GetString("LabelTaskDescription");
                lblTaskPriority.Content = resourceManager.GetString("LabelTaskPriority");
            }
        }

        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO It works, but not sure how good it'll be. Rewrite it later for sure.
            if (lvTasks.SelectedItem != null)
            {
                UserTask task = (UserTask)lvTasks.SelectedItem;

                EditTaskWindow editTaskWindow = new EditTaskWindow(task);
                if(editTaskWindow.ShowDialog() == true)
                {
                    //taskManagerData.ActiveTasks.Remove(task);
                    task = new UserTask(editTaskWindow.Name, editTaskWindow.Description, DateTime.Now, UserTask.GetTaskPriority(editTaskWindow.Priority));
                    //taskManagerData.ActiveTasks.Add(task);
                }
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string saveName;/*
            if (!taskManagerData.ActiveTasks.Any())
            {
                MessageBox.Show(resourceManager.GetString("NoTasksFound"));
                return;
            }
            */
            SaveWindow save = new SaveWindow();
            if (save.ShowDialog() == true)
            {
                saveName = save.SaveFileName;
                saveName += ".json";
            }
            else return;


            string fullSavePath = Path.Combine(SaveDirectory, saveName); 
            
            if (File.Exists(fullSavePath))
            {
                MessageBoxResult result = MessageBox.Show(resourceManager.GetString("FileExistsOverwriteWarning"), resourceManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await Saving(fullSavePath);
                }
                else
                {
                    MessageBox.Show(resourceManager.GetString("SaveCancelled"));
                    return;
                }
            }
            else
            {
                await Saving(fullSavePath);
            }
            MessageBox.Show(resourceManager.GetString("TasksSuccessfullySaved"));
        }

        async Task Saving(string fullSavePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(taskManagerData, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(fullSavePath, json);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(string.Format(resourceManager.GetString("ErrorFileNotFound"), Path.GetFileName(fullSavePath)));
            }
            catch (JsonException ex)
            {
                MessageBox.Show(resourceManager.GetString("ErrorCorruptedJson"));
                MessageBox.Show(resourceManager.GetString("ErrorDetails"));
                taskManagerData = new TaskManagerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(resourceManager.GetString("ErrorGeneric"));
            }
        }


        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {/*
            string saveName;
            string dirName = SaveDirectory;
            if (Directory.Exists(dirName))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

                if (openFileDialog.ShowDialog() == true)
                {
                    saveName = openFileDialog.FileName;

                    if (taskManagerData.ActiveTasks.Any() || taskManagerData.ArchiveTasks.Any())
                    {
                        MessageBoxResult result = MessageBox.Show(resourceManager.GetString("CurrentDataWillBeErased"), resourceManager.GetString("Warning"), MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            MessageBox.Show(resourceManager.GetString("LoadCancelled"));
                            return;
                        }
                    }

                    try
                    {
                        string json = await File.ReadAllTextAsync(saveName);
                        if (string.IsNullOrWhiteSpace(json))
                        {
                            MessageBox.Show(resourceManager.GetString("LoadErrorFileEmpty"));
                            taskManagerData = new TaskManagerData();
                            return;
                        }
                        var loadedData = JsonSerializer.Deserialize<TaskManagerData>(json);
                        if (loadedData == null)
                        {
                            MessageBox.Show(resourceManager.GetString("LoadFailed"));
                            return;
                        }
                        taskManagerData.ActiveTasks.Clear();
                        taskManagerData.ArchiveTasks.Clear();
                        foreach (var task in loadedData.ActiveTasks)
                        {
                            taskManagerData.ActiveTasks.Add(task);
                        }
                        foreach (var task in loadedData.ArchiveTasks)
                        {
                            taskManagerData.ArchiveTasks.Add(task);
                        }
                        MessageBox.Show(resourceManager.GetString("DataLoadedSuccessfully"));
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show(resourceManager.GetString("ErrorFileNotFound"));
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show(resourceManager.GetString("ErrorCorruptedJson"));
                        MessageBox.Show(resourceManager.GetString("ErrorDetails"));
                        taskManagerData = new TaskManagerData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(resourceManager.GetString("ErrorGeneric"));
                    }
                }
            }
            */
        }
    }
}