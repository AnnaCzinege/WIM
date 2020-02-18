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

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        myProcess myProcess = new myProcess();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = myProcess;
        }

        public ObservableCollection<myProcess> Processes { get; set; }


        void GetAllProcesses()
        {
            
            Processes = new ObservableCollection<myProcess>();
            foreach (var process in Process.GetProcesses())
            {
                this.Processes.Add(new myProcess()
                {
                    Name = process.ProcessName,
                    MemoryUsage = process.Id

                }) ;
            }
            ListBox.ItemsSource = Processes;
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
            if (this.Topmost == false)
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            //Processes[ListBox.SelectedIndex].Kill();
        }

        
    }

    public class myProcess
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int memoryUsage;

        public int MemoryUsage
        {
            get { return memoryUsage; }
            set { memoryUsage = value; }
        }


    }
}

