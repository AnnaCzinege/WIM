using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using ProcessNote.Model;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window RunWin { get; set; }
        private readonly Processes _processes;


        public MainWindow()
        {
            _processes = new Processes();
            InitializeComponent();
            DataContext = _processes;
        }

        void GetAllProcesses()
        {
            
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    _processes.ProcessCollection.Add(new MyProcess()
                    {
                        
                        Id = process.Id,
                        Name = process.ProcessName,
                        MemoryUsage = process.PrivateMemorySize64,
                        StartTime = process.StartTime,
                        RunTime = DateTime.Now - process.StartTime,
                        Threads = process.Threads,
                        CpuUsage = GetCpuUsage(process)

                    }) ;
                }
                catch (Win32Exception)
                {

                }
                catch (InvalidOperationException)
                {

                }
                
            }
        }

        private string GetCpuUsage(Process process)
        {
            PerformanceCounter myAppCpu = new PerformanceCounter( "Process", "% Processor Time", process.ProcessName);

            double pct = myAppCpu.NextValue();
            //Thread.Sleep(1000);
            return $"{pct} %";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            GetAllProcesses();
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

            _processes.ProcessCollection.Remove(selectedProcess);
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
            process.CpuUsage = GetCpuUsage(refreshedProcess);
        }
    }

}

