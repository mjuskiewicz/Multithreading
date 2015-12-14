using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierExample
{
    public class Program
    {
        private static int numberOfThreads = 10;

        public static void Main(string[] args)
        {
            Console.WriteLine("Version without synchronization");
            WithoutSynchronization();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Version with synchronization (Monitor.Wait, Monitor.Pulse)");
            WithMonitorSynchronization();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Version with synchronization (Barrier)");
            WithBarrierSynchronization();
        }

        private static void WithoutSynchronization()
        {
            ThreadStart action = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                }
            };

            var threads = new List<Thread>();

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads.Add(new Thread(action));
            }

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }

        private static int waitCounter = 0;

        private static void WithMonitorSynchronization()
        {
            var synch = new object();

            ThreadStart action = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                    lock (synch)
                    {
                        waitCounter++;

                        if (waitCounter == numberOfThreads)
                        {
                            waitCounter = 0;
                            Console.WriteLine(string.Empty);
                            Monitor.PulseAll(synch);
                        }
                        else
                        {
                            Monitor.Wait(synch);
                        }
                    }
                }
            };

            var threads = new List<Thread>();

            for (int i = 0; i < numberOfThreads; i++)
            { 
                threads.Add(new Thread(action));
            }

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }

        private static void WithBarrierSynchronization()
        {
            Barrier synch = new Barrier(numberOfThreads, s => Console.WriteLine());

            ThreadStart action = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                    synch.SignalAndWait();
                }
            };

            var threads = new List<Thread>();

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads.Add(new Thread(action));
            }

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }
    }
}
