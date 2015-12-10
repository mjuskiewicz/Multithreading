using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerComplexExample
{
    public class Producer
    {
        private static ParameterizedThreadStart task = new ParameterizedThreadStart(param =>
        {
            var warehouse = param as IWarehouse;
            var producerThreadIsActive = true;

            while (true)
            {
                if (!producerThreadIsActive)
                {
                    Sleep(3000);
                    producerThreadIsActive = true;
                }

                if (producerThreadIsActive)
                {
                    Sleep(50);

                    if (warehouse.StockState < warehouse.MaStockState)
                    {                        
                        warehouse.Add();
                    }
                    else
                    {
                        producerThreadIsActive = false;
                    }
                }
            }
        });

        public static Thread CreateaAndRun(IWarehouse warehouse)
        {
            var newThread = new Thread(task);
            newThread.IsBackground = true;
            newThread.Start(warehouse);
            return newThread;
        }

        private static void Sleep(int time)
        {
            Thread.Sleep(time);
        }
    }
}
