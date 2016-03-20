using Bing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using WpfBingSearchWithRx.Commons;

namespace WpfBingSearchWithRx.ViewModels
{
    public class SearchViewModel : NotificationObject, ISearchViewModel, IDisposable
    {
        private string _key = "";
        private string _searchTerm;
        private IEnumerable<WebResult> _listOfPages;
        private const string BingUrlApi = "https://api.datamarket.azure.com/Bing/Search//";
        private BingSearchContainer _bingApi;
        private CompositeDisposable _disposableResources = new CompositeDisposable();

        public SearchViewModel()
        {
            _bingApi = new BingSearchContainer(new Uri(BingUrlApi))
            {
                Credentials = new NetworkCredential(Key, Key)
            };

            PropertyChangedStream.Subscribe(p => Debug.WriteLine(p) );

            _disposableResources.Add(
                PropertyChangedStream
                    .Where(p => p == GetPropertyName(() => SearchTerm))
                    .Throttle(TimeSpan.FromSeconds(0.5))
                    .Select(_ => GetResultsForSelectedTerm())
                    .Subscribe(result => ListOfPages = result));
        }

        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    RaisePropertyChanged(() => SearchTerm);
                }
            }
        }

        public IEnumerable<WebResult> ListOfPages
        {
            get
            {
                return _listOfPages;
            }
            set
            {
                if (_listOfPages != value)
                {
                    _listOfPages = value;
                    RaisePropertyChanged(() => ListOfPages);
                }
            }
        }

        public string Key
        {
            get
            {
                return _key;
            }
        }

        private IEnumerable<WebResult> GetResultsForSelectedTerm()
        {
            var query = _bingApi.Web(SearchTerm, null, null, null, null, null, null, null);
            return query.Execute();
        }

        public void Dispose()
        {
            _disposableResources.Dispose();
        }
    }
}
