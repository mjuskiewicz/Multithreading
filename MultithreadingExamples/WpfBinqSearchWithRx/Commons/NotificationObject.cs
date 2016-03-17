using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WpfBingSearchWithRx.Commons
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public IConnectableObservable<string> PropertyChangedStream { get; private set; }
        private ReplaySubject<string> streamSource = new ReplaySubject<string>();

        public NotificationObject()
        {
            PropertyChangedStream = streamSource.AsObservable().Publish();
            PropertyChangedStream.Connect();
        }

        protected string GetPropertyName<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            return memberExpression.Member.Name;
        }

        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var propertyName = GetPropertyName(projection);
            RaisePropertyChanged(propertyName);
        }

        private void RaisePropertyChanged(string name)
        {
            streamSource.OnNext(name);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
