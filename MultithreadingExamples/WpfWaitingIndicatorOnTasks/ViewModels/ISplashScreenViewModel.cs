using System.Collections.Generic;
using WpfWaitingIndicatorOnTasks.Services;

namespace WpfWaitingIndicatorOnTasks.ViewModels
{
    public interface ISplashScreenViewModel
    {
        bool IsShowIndicator { get; set; }
        string Message { get; }
        List<TaskToExecute> Status { get; }
    }
}
