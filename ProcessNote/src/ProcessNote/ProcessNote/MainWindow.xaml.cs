﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using ProcessNote.Model;
using ProcessNote.ViewModel;
using System.Windows.Threading;

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

        private void RefreshAllProcesses(object sender, EventArgs e)
        {
           
            foreach (var process in MainWindowViewModel.Processes.ProcessCollection)
            {
                RefreshProcessInfo(process);
            }
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

