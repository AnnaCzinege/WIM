using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Process[] processes;

        void GetAllProcesses()
        {
            processes = Process.GetProcesses();
            ListBox.Items.Clear();
            foreach (var process in processes)
            {
                ListBox.Items.Add(process.ProcessName);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            GetAllProcesses();
        }

        private void aotClick(object sender, RoutedEventArgs e)
        {
            if (Window.Topmost == false)
            {
                Window.Topmost = true;
            }
            else
            {
                Window.Topmost = false;
            }
        }
    }
}

