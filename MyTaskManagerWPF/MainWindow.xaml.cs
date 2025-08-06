using System.Windows;
using MyTaskManager;
using System.Text.Json;
using System.Resources;
using System.Reflection;
using System.IO;

namespace MyTaskManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResourceManager resourceManager = new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());
        const string SaveDirectory = "Saves";
        TaskManagerData taskManagerData = new TaskManagerData();

        public MainWindow()
        {
            InitializeComponent();

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
            taskManagerData.ActiveTasks.Add(new UserTask(tbName.Text, tbDescription.Text, DateTime.Now, UserTask.Priority.Высокая));
            MessageBox.Show(resourceManager.GetString("TaskSuccessfullyAdded"));
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked Delete Task!");
        }
    }
}