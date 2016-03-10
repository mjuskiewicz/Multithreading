using System.Collections.Generic;
using WpfWaitingIndicatorOnTasks.Commons;
using WpfWaitingIndicatorOnTasks.Services;

namespace WpfWaitingIndicatorOnTasks.ViewModels
{
    public class SplashScreenViewModel : NotificationObject, ISplashScreenViewModel
    {
        private string _message;
        private bool _isShowIndicator;
        private IAggregatorService _aggregatorService;
        
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        public bool IsShowIndicator
        {
            get
            {
                return _isShowIndicator;
            }
            set
            {
                if (_isShowIndicator != value)
                {
                    _isShowIndicator = value;
                    RaisePropertyChanged(() => IsShowIndicator);
                }
            }
        }

        public List<TaskToExecute> Status
        {
            get
            {
                return _aggregatorService.Status;
            }
        }

        public SplashScreenViewModel(IAggregatorService aggregatorService)
        {
            _isShowIndicator = true;
            _aggregatorService = aggregatorService;
            _aggregatorService.PropertyChanged += AggregatorService_PropertyChanged;
        }

        private void AggregatorService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReady")
            {
                IsShowIndicator = !_aggregatorService.IsReady ?? false;
                Message = "The content is loaded";
            }
        }
    }
}
