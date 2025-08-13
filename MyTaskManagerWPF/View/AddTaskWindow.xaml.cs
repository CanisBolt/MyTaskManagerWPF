using MyTaskManagerWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
