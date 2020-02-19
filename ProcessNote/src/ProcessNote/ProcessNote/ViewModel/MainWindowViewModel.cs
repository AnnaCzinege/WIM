using System;
using System.ComponentModel;
using System.Diagnostics;
using ProcessNote.Model;

namespace ProcessNote.ViewModel
{
    public class MainWindowViewModel
    {
        public Processes Processes { get; set; }

        public MainWindowViewModel()
        {
            Processes = new Processes();
        }

        public void GetAllProcesses()
        {

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    Processes.ProcessCollection.Add(new MyProcess()
                    {

                        Id = process.Id,
                        Name = process.ProcessName,
                        MemoryUsage = process.PrivateMemorySize64,
                        StartTime = process.StartTime,
                        RunTime = DateTime.Now - process.StartTime,
                        Threads = process.Threads,

                    });
                }
                catch (Win32Exception)
                {

                }
                catch (InvalidOperationException)
                {

                }

            }
        }
    }
}

