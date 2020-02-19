using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using ProcessNote.Model;
using ProcessNote.ViewModel;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public Window RunWin { get; set; }


        public MainWindow()
        {
            MainWindowViewModel = new MainWindowViewModel();
            DataContext = MainWindowViewModel.Processes;
            InitializeComponent();
        }


      

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
           
            MainWindowViewModel.GetAllProcesses(Process.GetProcesses());
        }

        private void AotClick(object sender, RoutedEventArgs e)
        {
            if (Window.Topmost == false) Window.Topmost = true;
            else Window.Topmost = false;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var selectedProcess = (MyProcess) ListBox.SelectedItem;
            var actualProcess = Process.GetProcessById(selectedProcess.Id);

            actualProcess.Kill();

            MainWindowViewModel.Processes.ProcessCollection.Remove(selectedProcess);
        }

        private void ShowRunWindow(object sender, RoutedEventArgs e)
        {
            if (RunWin == null) 
            {
                Run runWindow = new Run();
                RunWin = runWindow;
                runWindow.Show();
                runWindow.Topmost = true;
            }
                  }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadIcons(object sender, RoutedEventArgs e)
        {
            //foreach (var thisProcess in Processes)
            //{
              //  Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(thisProcess.Name);
            //}
        }


        private void MouseDoubleClickRefresh(object sender, MouseButtonEventArgs e)
        {
            MyProcess selectedProcess = (MyProcess) ListBox.SelectedItem;
            RefreshProcessInfo(selectedProcess);
        }


        private void RefreshProcessInfo(MyProcess process)
        {
            var refreshedProcess = Process.GetProcessById(process.Id);
            

            process.RunTime = DateTime.Now - refreshedProcess.StartTime;
            process.MemoryUsage = refreshedProcess.PrivateMemorySize64;
            process.CpuUsage = process.GetCpuUsage(refreshedProcess);
        }
    }

}

