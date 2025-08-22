using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.ViewModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

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
                btnMarkASCompelted.Content = LocalizationManager.GetString("ButtonMarkAsComplete");
            }
        }
    }
}