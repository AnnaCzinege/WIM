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
using System.Drawing;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process[] processes;

        public Window RunWin { get; set; }
       

        public MainWindow()
        {
            InitializeComponent();
        }

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
            if (Window.Topmost == false) Window.Topmost = true;
            else Window.Topmost = false;
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            processes[ListBox.SelectedIndex].Kill();
        }

        private void showRunWindow(object sender, RoutedEventArgs e)
        {
            if (RunWin == null) 
            {
                Run runWindow = new Run();
                RunWin = runWindow;
                runWindow.Show();
                runWindow.Topmost = true;
            }
            
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showIcons(object sender, RoutedEventArgs e)
        {
            //Icon ico = Icon
        }
    }
}

