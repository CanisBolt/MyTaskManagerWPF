using MyTaskManagerWPF.Model;
using MyTaskManagerWPF.ViewModel;
using System.Windows;

namespace MyTaskManagerWPF.View
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public EditTaskWindow(UserTask task)
        {
            InitializeComponent();

            var viewModel = new EditTaskVM(task);
            viewModel.CloseAction = () => Close();

            DataContext = viewModel;
        }
    }
}
