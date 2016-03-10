using System.Collections.Generic;
using System.ComponentModel;

namespace WpfWaitingIndicatorOnTasks.Services
{
    public interface IAggregatorService : INotifyPropertyChanged
    {
        bool? IsReady { get; }
        List<TaskToExecute> Status { get; }
        void Build();
    }
}
