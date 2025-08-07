using System.Windows;
using MyTaskManager;
using System.Text.Json;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Windows.Media;

namespace MyTaskManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResourceManager resourceManager = new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());
        const string SaveDirectory = "Saves";
        public TaskManagerData taskManagerData { get; set; } = new TaskManagerData();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }
        /*
        void ChangeLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            string cultureName = Thread.CurrentThread.CurrentUICulture.NativeName;
            Console.WriteLine(string.Format(resourceManager.GetString("LanguageChangedSuccessfully"), cultureName));
        }
        */

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

            taskManagerData.ActiveTasks.Add(new UserTask(name, description, DateTime.Now, UserTask.GetTaskPriority(taskPriority)));
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
                    taskManagerData.ActiveTasks.Remove((UserTask)lvTasks.SelectedItem);
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
    }
}