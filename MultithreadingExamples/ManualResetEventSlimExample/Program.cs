using System;
using System.Threading;

namespace ManualResetEventSlimExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManualResetEvent threadOneController = new ManualResetEvent(true);
            ManualResetEvent threadTwoController = new ManualResetEvent(false);

            ThreadStart action1 = new ThreadStart(() =>
            {
                while (true)
                {
                    threadOneController.WaitOne();
                    threadTwoController.Reset();
                    Console.WriteLine("{0} --- Thread 1 is doing some actions", DateTime.Now);
                    threadTwoController.Set();
                }
            });

            ThreadStart action2 = new ThreadStart(() =>
            {
                while (true)
                {
                    threadTwoController.WaitOne();
                    threadOneController.Reset();
                    Console.WriteLine("{0} --- Thread 2 is doing some actions", DateTime.Now);
                    threadOneController.Set();
                }
            });

            var t1 = new Thread(action1);
            var t2 = new Thread(action2);

            t1.Start();
            t2.Start();
        }
    }
}
