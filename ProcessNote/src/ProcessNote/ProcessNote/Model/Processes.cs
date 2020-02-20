using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote.Model
{
    public class Processes
    {

        public Processes()
        {
            ProcessCollection = new ObservableCollection<MyProcess>();
        }

        public ObservableCollection<MyProcess> ProcessCollection { get; set; }


    }
}
