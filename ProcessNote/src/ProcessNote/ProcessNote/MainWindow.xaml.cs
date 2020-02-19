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
using System.Linq;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window RunWin { get; set; }
        public ObservableCollection<myProcess> Processes { get; set; }

        myProcess myProcess = new myProcess();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = myProcess;
        }

        void GetAllProcesses()
        {
            
            Processes = new ObservableCollection<myProcess>();
            var data = new List<myProcess>();

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    this.Processes.Add(new myProcess()
                    {
                        Id = process.Id,
                        Name = process.ProcessName,
                        MemoryUsage = process.PrivateMemorySize64,
                        StartTime = process.StartTime,
                        RunTime = DateTime.Now - process.StartTime,
                        Threads = process.Threads,
                        CpuUsage = GetCpuUsage(process)

                    });
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

        private String GetCpuUsage(Process process)
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

        private void aotClick(object sender, RoutedEventArgs e)
        {
            if (Window.Topmost == false) Window.Topmost = true;
            else Window.Topmost = false;
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            //Processes[ListBox.SelectedIndex].Kill();
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

        private void loadIcons(object sender, RoutedEventArgs e)
        {
            //foreach (var thisProcess in Processes)
            //{
              //  Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(thisProcess.Name);
            //}
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }

    public class myProcess
    {
        private int id;
        private string name;
        private long memoryUsage;
        private DateTime startTime;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        

        public long MemoryUsage
        {
            get { return memoryUsage; }
            set { memoryUsage = value; }
        }

        public DateTime StartTime { 
            get { return startTime; }
            set { startTime = value; }
        }

        private TimeSpan runTime;

        public TimeSpan RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }

        private ProcessThreadCollection threads;

        public ProcessThreadCollection Threads
        {
            get { return threads; }
            set { threads = value; }
        }

        private String cpuUsage;

        public String CpuUsage
        {
            get { return cpuUsage; }
            set { cpuUsage = value; }
        }



    }
}

