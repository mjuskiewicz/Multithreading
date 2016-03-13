using System;
using System.Linq;
using System.Reactive.Linq;

namespace RxCombineLatestExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<char> charSequence = "ABCDEFGHIJ".ToObservable();
            IObservable<int> numberSequence = Observable.Create<int>(seq =>
            {
                for (int i = 0; i < 10; i++)
                {
                    seq.OnNext(i);
                    System.Threading.Thread.Sleep(1000);
                }

                seq.OnCompleted();

                return () => { };
            });

            var combinedSequence1 = Observable.Zip(charSequence, numberSequence, (number, letter) => string.Format("{0}{1}", number, letter));
                combinedSequence1.Subscribe(Console.WriteLine, () => Console.WriteLine("The end of the first example."));

            var rangeSequence = Observable.Zip(Observable.Range(0, 10), Observable.Interval(TimeSpan.FromSeconds(1)), (number , _) => number );

            var combinedSequence2 = Observable.CombineLatest(rangeSequence, rangeSequence.Skip(1), (numberOne, numberTwo) => new { numberOne, numberTwo });
                combinedSequence2.Subscribe(onNext: (number) => Console.WriteLine(number));
            
            Console.ReadLine();
        }
    }
}
