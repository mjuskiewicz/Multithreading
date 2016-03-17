using Bing;
using System.Collections.Generic;

namespace WpfBingSearchWithRx.ViewModels
{
    public interface ISearchViewModel
    {
        string SearchTerm { get; set; }
        string Key { get; }
        IEnumerable<WebResult> ListOfPages { get; }
    }
}
