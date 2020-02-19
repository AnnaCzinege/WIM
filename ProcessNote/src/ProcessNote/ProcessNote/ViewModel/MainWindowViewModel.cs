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

        public void GetAllProcesses(Process[] currentProccesses)
        {

            foreach (var process in currentProccesses)
            {
                try
                {
                    Processes.ProcessCollection.Add(new MyProcess(process));
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

