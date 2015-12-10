using System;
using System.Threading;

namespace ProducerConsumerSimpleExample
{
    public class Program
    {
        private static int stockState = 0;
        private static int maxStockState = 20;
        private static int producerOnNextTime = 100;
        private static int consumerOnNextTime = 300;

        private static bool producerThreadIsActive = true;
        private static bool consumerThreadIsActive = true;

        private static object synch = new object();

        private static Random r = new Random();

        public static void Main(string[] args)
        {
            ThreadStart producerTask = new ThreadStart(() =>
            {
                while (true)
                {
                    if (!producerThreadIsActive)
                    {
                        SleepRandom(3000);
                        producerThreadIsActive = true;
                    }

                    if (producerThreadIsActive)
                    {
                        Thread.Sleep(producerOnNextTime);

                        if (stockState < maxStockState)
                        {
                            lock (synch)
                            {
                                stockState = stockState + 1;
                                Console.Write("New element has been   added\t");
                                DispalyStockState();
                            }
                        }
                        else
                        {
                            producerThreadIsActive = false;
                            Console.WriteLine("Producer thread is suspended");
                        }
                    }
                }
            });

            ThreadStart consumerTask = new ThreadStart(() =>
            {
                while (true)
                {
                    if (!consumerThreadIsActive)
                    {
                        SleepRandom(3000);
                        consumerThreadIsActive = true;
                    }

                    if (consumerThreadIsActive)
                    {
                        Thread.Sleep(consumerOnNextTime);

                        if (stockState > 0)
                        {
                            lock (synch)
                            {
                                stockState = stockState - 1;
                                Console.Write("New element has been removed\t");
                                DispalyStockState();
                            }
                        }
                        else
                        {
                            consumerThreadIsActive = false;
                            Console.WriteLine("Consumer thread is suspended");
                        }
                    }
                }
            });

            var t1 = new Thread(producerTask);
            t1.IsBackground = true;
            t1.Start();
            var t2 = new Thread(consumerTask);
            t2.IsBackground = true;
            t2.Start();

            Console.ReadKey();
        }

        private static void SleepRandom(int time)
        {
            Thread.Sleep(r.Next(time));
        }

        private static void DispalyStockState()
        {
            Console.WriteLine("Thread: {0} \t Stock State: {1}", Thread.CurrentThread.ManagedThreadId, stockState);
        }
    }
}
