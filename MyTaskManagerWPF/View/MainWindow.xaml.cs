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

        private void btnDeleteTask_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}