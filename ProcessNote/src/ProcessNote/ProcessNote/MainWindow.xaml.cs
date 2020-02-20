using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using ProcessNote.Model;
using ProcessNote.ViewModel;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer; 
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public Window RunWin { get; set; }
        public Window CommentWin { get; set; }

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
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(RefreshAllProcesses);
            _timer.Start();
        }

        private void AotClick(object sender, RoutedEventArgs e)
        {
            if (Window.Topmost == false) Window.Topmost = true;
            else Window.Topmost = false;
        }

        private void EndTask(object sender, RoutedEventArgs e)
        {
            var selectedProcesses = ListBox.SelectedItems;
            int lenght = selectedProcesses.Count;

            for (int i = 0; i < lenght; i++)
            {
                MyProcess actual = (MyProcess)selectedProcesses[0];
                var actualProcess = Process.GetProcessById(actual.Id);
                actualProcess.Kill();
                MainWindowViewModel.Processes.ProcessCollection.Remove(actual);
            }
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

        private void RefreshAllProcesses(object sender, EventArgs e)
        {
           
            foreach (var process in MainWindowViewModel.Processes.ProcessCollection)
            {
                RefreshProcessInfo(process);
            }
        }

        private void RefreshProcessInfo(MyProcess process)
        {
            try
            {
                var refreshedProcess = Process.GetProcessById(process.Id);

                var runTime = DateTime.Now - refreshedProcess.StartTime;
                process.RunTime = string.Format("{0}:{1}:{2}", (int) runTime.Hours, (int)runTime.Minutes, (int)runTime.Seconds);
                process.MemoryUsage = $"{(((float)refreshedProcess.WorkingSet64) / 1024 / 1024):N1} MB";
                process.CpuUsage = process.GetCpuUsage(refreshedProcess);
            }
            catch (ArgumentException)
            {
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItems.Count == 0) Process.Start("chrome.exe", "http://github.com/AnnaCzinege/WIM");
            else
            {
                if (ListBox.SelectedItems.Count > 1) MessageBox.Show("Multiple processes selected", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                { 
                    MyProcess process = (MyProcess)ListBox.SelectedItem;
                    Process.Start("chrome.exe", $"http://google.com/search?q={process.Name}");
                }
            }
        }

        private void ShowThreads(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItems.Count == 0) MessageBox.Show("No process selected", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (ListBox.SelectedItems.Count > 1) MessageBox.Show("Multiple processes selected", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MyProcess process = (MyProcess)ListBox.SelectedItem;
                    //foreach (var t in process.Threads)
                    //{
                    //    ProcessThread sdfdsf = (ProcessThread)t;
                    //    Console.WriteLine(sdfdsf.ToString());
                    //    int a = 4;
                    //}
                    MessageBox.Show($"Number of threads: {process.Threads.Count}", "Threads", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ClickOnTask(object sender, MouseButtonEventArgs e)
        {
            if (CommentWin == null)
            {
                Comment commentWin = new Comment((MyProcess)ListBox.SelectedItem);
                CommentWin = commentWin;
                commentWin.Show();
                commentWin.Topmost = true;
            }
        }
    }

}

