using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManagerWPF.Model
{
    public class TaskData
    {
        public ObservableCollection<UserTask> ActiveTasks { get; set; }
        public ObservableCollection<UserTask> ArchiveTasks { get; set; }
    }
}
