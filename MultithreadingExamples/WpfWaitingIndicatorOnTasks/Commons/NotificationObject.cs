using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WpfWaitingIndicatorOnTasks.Commons
{
    public class NotificationObject : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            RaisePropertyChanged(memberExpression.Member.Name);
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
