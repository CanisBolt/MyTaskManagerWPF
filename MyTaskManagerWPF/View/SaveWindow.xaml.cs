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
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        ResourceManager resourceManager = new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());

        public string SaveFileName { get; private set; }
        public SaveWindow()
        {
            InitializeComponent();

            lblSaveName.Content = resourceManager.GetString("EnterSaveFileName"); 
            btnEditApply.Content = resourceManager.GetString("ButtonApply");
            btnEditCancel.Content = resourceManager.GetString("ButtonCancel");


        }
        private void btnEditApply_Click(object sender, RoutedEventArgs e)
        {
            if(tbSaveName.Text.Length < 3)
            {
                MessageBox.Show(resourceManager.GetString("SaveFileNameTooShort"));
                return;
            }

            SaveFileName = tbSaveName.Text;
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
