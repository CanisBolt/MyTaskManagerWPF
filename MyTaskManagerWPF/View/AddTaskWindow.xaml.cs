using MyTaskManagerWPF.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace MyTaskManagerWPF.View
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        AddTaskVM addTaskVM = new AddTaskVM();
        public AddTaskWindow()
        {
            InitializeComponent();
            this.DataContext = addTaskVM;
        }
    }
}
