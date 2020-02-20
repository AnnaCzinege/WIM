using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProcessNote.Annotations;

namespace ProcessNote.Model
{
    public class MyProcess : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _memoryUsage;
        private DateTime _startTime;
        private DateTime lastTime;
        private TimeSpan lastTotalProcessorTime;
        private DateTime currentTime;
        private TimeSpan currentTotalProcessorTime;

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


        public string MemoryUsage
        {
            get => _memoryUsage;
            set
            {
                _memoryUsage = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private string _runTime;

        public string RunTime
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

        private List<string> _commentList;

        public List<string> CommentList
        {
            get => _commentList;
            set
            {
                _commentList = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //For refresh 
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetCpuUsage(Process process)
        {
            
            if (lastTime == null)
            {
                lastTime = DateTime.Now;
                lastTotalProcessorTime = process.TotalProcessorTime;
                return "0.00 %";
            }

            else
            {
                currentTime = DateTime.Now;
                currentTotalProcessorTime = process.TotalProcessorTime;

                double CpuUsage = (currentTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) /
                    (currentTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount));

                lastTime = currentTime;
                lastTotalProcessorTime = currentTotalProcessorTime;
                return $"{CpuUsage:N2} %";
            }
            
            
        }

        public MyProcess(Process process)
        {
            Id = process.Id;
            Name = process.ProcessName;
            MemoryUsage = $"{(((float)process.WorkingSet64) / 1024 / 1024):N1} MB";
            StartTime = process.StartTime;
            var runTime = DateTime.Now - process.StartTime;
            RunTime = string.Format("{0}:{1}:{2}", (int)runTime.Hours, (int)runTime.Minutes, (int)runTime.Seconds);
            Threads = process.Threads;
            CpuUsage = GetCpuUsage(process);
            CommentList = new List<string>();
        }


    }
}
