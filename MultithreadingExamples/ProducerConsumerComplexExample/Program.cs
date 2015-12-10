using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProducerConsumerComplexExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Warehouse warehouse = new Warehouse(500);

            var producers = new List<Thread>();
            var consumers = new List<Thread>();

            producers.Add(Producer.CreateaAndRun(warehouse));
            consumers.Add(Consumer.CreateaAndRun(warehouse));

            while (true)
            {
                Thread.Sleep(100);
                DispalyStockState(warehouse, producers, consumers);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            producers.Add(Producer.CreateaAndRun(warehouse));
                            break;
                        case ConsoleKey.DownArrow:
                            if (producers.Any())
                            {
                                producers[0].Abort();
                                producers.RemoveAt(0);
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            consumers.Add(Consumer.CreateaAndRun(warehouse));
                            break;
                        case ConsoleKey.LeftArrow:
                            if (consumers.Any())
                            {
                                consumers[0].Abort();
                                consumers.RemoveAt(0);
                            }
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }
        }

        private static void DispalyStockState(IWarehouse warehouse, IList<Thread> producers, IList<Thread> consumers)
        {
            var process = Process.GetCurrentProcess();

            Console.Clear();
            Console.WriteLine("Stock State: {0}\t Producers: {1}\t Consumers: {2}\t all: {3}", warehouse.StockState, producers.Count, consumers.Count, process.Threads.Count);
        }
    }
}
