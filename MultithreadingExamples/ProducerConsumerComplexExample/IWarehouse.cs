using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerComplexExample
{
    public interface IWarehouse
    {
        int StockState { get; }

        int MaStockState { get; }

        void Add();

        void Remove();
    }
}
