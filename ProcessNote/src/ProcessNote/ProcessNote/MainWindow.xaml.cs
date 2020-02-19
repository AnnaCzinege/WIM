using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ProcessNote.Annotations;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window RunWin { get; set; }
        public ObservableCollection<MyProcess> Processes { get; set; }

        MyProcess myProcess = new MyProcess();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = myProcess;
        }

        void GetAllProcesses()
        {
            
            Processes = new ObservableCollection<MyProcess>();
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    this.Processes.Add(new MyProcess()
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
            ListBox.ItemsSource = Processes;
        }

        private string GetCpuUsage(Process process)
        {
            PerformanceCounter myAppCpu = new PerformanceCounter( "Process", "% Processor Time", process.ProcessName);

            double pct = myAppCpu.NextValue();
            //Thread.Sleep(1000);
            return $"{pct} %";
        }


      

        private void createGrid()
        {

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

            Processes.Remove(selectedProcess);
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


    public class MyProcess : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private long _memoryUsage;
        private DateTime _startTime;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        
        public long MemoryUsage
        {
            get => _memoryUsage;
            set
            {
                _memoryUsage = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime { 
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _runTime;

        public TimeSpan RunTime
        {
            get => _runTime;
            set
            {
                _runTime = value;
                OnPropertyChanged();
            }
        }

        private ProcessThreadCollection threads;

        public ProcessThreadCollection Threads
        {
            get => threads;
            set
            {
                threads = value;
                OnPropertyChanged();
            }
        }

        private string _cpuUsage;

        public string CpuUsage
        {
            get => _cpuUsage;
            set
            {
                _cpuUsage = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

