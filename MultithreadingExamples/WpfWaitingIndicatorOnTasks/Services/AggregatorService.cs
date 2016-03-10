using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfWaitingIndicatorOnTasks.Commons;

namespace WpfWaitingIndicatorOnTasks.Services
{
    public class AggregatorService : NotificationObject , IAggregatorService
    {
        private List<TaskToExecute> _status = new List<TaskToExecute>();
        private List<Task> _tasks = new List<Task>();
        private bool? _isReady = null;
        private bool _isBlocked;
        
        public bool? IsReady
        {
            get
            {
                return _isReady;
            }

            private set
            {
                if (_isReady != value)
                {
                    _isReady = value;
                    RaisePropertyChanged(() => IsReady);
                }
            }
        }

        public List<TaskToExecute> Status
        {
            get
            {
                return _status;
            }
        }

        public void RegisterTask(string name, Action singleAction)
        {
            if (_isBlocked)
                throw new Exception("The stream was closed");

            var singleStatus = new TaskToExecute { Name = name, IsCompleted = false };
            _status.Add(singleStatus);
            _tasks.Add(new Task(() => { singleAction(); singleStatus.IsCompleted = true; }));
        }

        public void Build()
        {
            _isBlocked = true;
            IsReady = false;

            Task.Factory.StartNew(() =>
            {
                _tasks.ForEach(t => t.Start());
                Task.WaitAll(_tasks.ToArray());
                IsReady = true;
            });
        }
    }

    public class TaskToExecute : NotificationObject
    {
        private bool _isCompleted;
        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    RaisePropertyChanged(() => IsCompleted);
                }
            }
        }

        public string Name { get; set; }
    }
}