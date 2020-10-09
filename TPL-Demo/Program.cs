using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPL_Demo
{
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }
        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }

        static void ParallelInvolke()
        {
            Parallel.Invoke(() => Task1(), () => Task2());
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }

        static void ParallelForEach()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });
        }

        static void ParallelFor()
        {
            var items = Enumerable.Range(0, 500).ToArray();
            Parallel.For(0, items.Length, i =>
            {
                WorkOnItem(items[i]);
            });
        }

        static void LoopState()
        {
            var items = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, items.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 200)
                    loopState.Stop();

                WorkOnItem(items[i]);
            });
        }

        static void Main(string[] args)
        {
            var items = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, items.Count(), (int i, ParallelLoopState loopState) =>
              {
                  if (i == 200)
                      loopState.Stop();

                  WorkOnItem(items[i]);
              });
            Console.WriteLine("Complete:" + result.IsCompleted);
            Console.WriteLine("Items:" + result.LowestBreakIteration);
            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
