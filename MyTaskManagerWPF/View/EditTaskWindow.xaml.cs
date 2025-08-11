using MyTaskManagerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
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
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        ResourceManager resourceManager = new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Priority { get; private set; }
        public EditTaskWindow(UserTask task)
        {
            InitializeComponent();
            
            tbName.Text = task.Name;
            tbDescription.Text = task.Description;
            cbPriority.Text = task.TaskPriority.ToString();

            btnEditApply.Content = resourceManager.GetString("ButtonApply");
            btnEditCancel.Content = resourceManager.GetString("ButtonCancel");
        }

        private void btnEditApply_Click(object sender, RoutedEventArgs e)
        {
            Name = tbName.Text;
            Description = tbDescription.Text;
            Priority = cbPriority.Text;

            GetWindow(this).DialogResult = true;
            GetWindow(this).Close();
        }

        private void btnEditCancel_Click(object sender, RoutedEventArgs e)
        {
            GetWindow(this).DialogResult = false;
            GetWindow(this).Close();
        }
    }
}
