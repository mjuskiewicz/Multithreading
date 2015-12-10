using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerComplexExample
{
    public class Consumer
    {
        private static ParameterizedThreadStart task = new ParameterizedThreadStart(param =>
        {
            var warehouse = param as IWarehouse;
            var threadIsActive = true;

            while (true)
            {
                if (!threadIsActive)
                {
                    Sleep(3000);
                    threadIsActive = true;
                }

                if (threadIsActive)
                {
                    Sleep(100);

                    if (warehouse.StockState > 0)
                    {
                        warehouse.Remove();
                    }
                    else
                    {
                        threadIsActive = false;
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
