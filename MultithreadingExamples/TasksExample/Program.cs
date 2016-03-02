using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TasksExample
{
    class Program
    {
        private static Random r = new Random();

        static void Main(string[] args)
        {
            int numberOfProcessors = Environment.ProcessorCount;
            var tasks = new List<Task>();
            int n;

            Console.Write("Set the value of the N parameter: ");
            var sn = Console.ReadLine();

            if (Int32.TryParse(sn, out n))
            {
                if (n == 0) n = Int32.MaxValue - 2000000000;

                Console.WriteLine("Value of the N parameter: {0}", n);

                var sourceOfData = GenerateListOfRandomInts(n);

                int sizeOfTaskForSingleThread = n / numberOfProcessors;

                for (int i = 0; i < numberOfProcessors; i++)
                {
                    var indexStart = i * sizeOfTaskForSingleThread;
                    var dataForSingleThread = new List<int>(sourceOfData.Skip(indexStart).Take(sizeOfTaskForSingleThread));

                    tasks.Add(new Task(data => DoSomeAction(data), dataForSingleThread));
                }

                var durationOnMultiThreads = CalculateTheExecutionTime(() =>
                {
                    tasks.ForEach(t => t.Start());
                    Task.WaitAll(tasks.ToArray());
                });

                var durationOnSingleThread = CalculateTheExecutionTime(() =>
                {
                    DoSomeAction(sourceOfData);
                });
                
                Console.WriteLine("durationOnMultiThreads {0}", durationOnMultiThreads);
                Console.WriteLine("durationOnSingleThread {0}", durationOnSingleThread);
            }
            else
            {
                Console.WriteLine("Validation error. Incorrect type of N parameter. Please provide Int32 number.");
            }
        }

        private static void DoSomeAction(object parameter)
        {
            var data = parameter as List<int>;

            var result = data.Sum();

            Console.WriteLine("Result: {0}", result);
        } 

        private static List<int> GenerateListOfRandomInts(int size)
        {
            var list = new List<int>(size);

            for (int i = 0; i < size; i++)
                list.Add(r.Next(0, 2));

            return list;
        }

        private static long CalculateTheExecutionTime(Action method)
        {
            var watch = Stopwatch.StartNew();

            method();

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
