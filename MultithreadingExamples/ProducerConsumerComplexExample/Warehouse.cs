using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerComplexExample
{
    public class Warehouse : IWarehouse
    {
        private readonly int maxStockState;
        private object synch = new object();
        private int stockState = 0;

        public Warehouse(int nmaxStockState)
        {
            maxStockState = nmaxStockState;
        }

        public int StockState
        {
            get
            {
                return stockState;
            }
        }

        public int MaStockState
        {
            get
            {
                return maxStockState;
            }
        }

        public void Add()
        {
            lock (synch)
            {
                stockState++;
            }
        }

        public void Remove()
        {
            lock (synch)
            {
                stockState--;
            }
        }
    }
}
