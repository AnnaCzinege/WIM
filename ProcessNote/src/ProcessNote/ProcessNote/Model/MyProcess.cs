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

        public DateTime StartTime
        {
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
