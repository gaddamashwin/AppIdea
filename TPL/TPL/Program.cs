using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<Action> measure = (body) =>
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    body();
                    Console.WriteLine("{0},{1}", Thread.CurrentThread.ManagedThreadId, st.ElapsedTicks.ToString());
                    st.Stop();
                };
            Action myJob = () =>
            {
                for (int i = 0; i < 200000; i++)
                {

                }
            };

            Action ioJob = () => { Thread.Sleep(1000); };

            //METHOD1
            //var tasks = new[]
            //{
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob)), 
            //    Task.Factory.StartNew(() => measure(myJob))
            //};
            //Task.WaitAll(tasks);

            //METHOD2
            //var tasks = Enumerable.Range(1, 10)
            //    .Select(_ => Task.Factory.StartNew(() => measure(ioJob)))
            //    .ToArray();
            //Task.WaitAll(tasks);
            
            //METHOD3
            //Parallel.For(0, 10, _ => measure(ioJob));


            //All Methods 1, 2 and 3 run in parallel and take the same time

            //METHOD4 SEQUENTIAL
            //Enumerable.Range(1, 10)
            //    .ToList()
            //    .ForEach(_ => measure(ioJob));

            //METHOD5
            //Enumerable.Range(1, 10)
            //    .AsParallel()
            //    .ForAll(_ => measure(ioJob));
            
            //PLinq
            //METHOD6
            //ParallelEnumerable.Range(1, 10)
            //    .ForAll(_ => measure(ioJob));

            //METHOD7
            //Both PLinq and TPL use ThreadPool, Change the values of minThreads and degree of parallelism
            //ThreadPool.SetMinThreads(10, 10);
            //ParallelEnumerable.Range(1, 10)
            //    .WithDegreeOfParallelism(5)
            //    .ForAll(_ => measure(ioJob));

            //PRODUCER and CONSUMER Problem
            //var queue = new Queue<int>(); //Queue is not THREAD SAFE and CONSUMBER Cannot signal that queue is done.
            //var queue = new ConcurrentQueue<int>(); //These are thread safe
            var queue = new BlockingCollection<int>(100); //Uses concurrentQueue but also specify the max number of items. 
            var producers = Enumerable.Range(1, 10)
                .Select(_ => Task.Factory.StartNew(() =>
                    {
                        Enumerable.Range(1, 100)
                            .ToList()
                            .ForEach(i =>
                                { 
                                    queue.Add(i);
                                    Thread.Sleep(100);
                                }
                        );
                    }))
                    .ToArray();

            var consumers = Enumerable.Range(1, 1)
                    .Select(_ => Task.Factory.StartNew(() =>
                    {
                        foreach (var item in queue.GetConsumingEnumerable())
                        {
                            Console.WriteLine(item);
                        }
                    }))
                     .ToArray();

            Console.WriteLine("Start");
            
            Task.WaitAll(producers);
            Console.WriteLine("producers is done." + queue.Count);
            queue.CompleteAdding();
            Task.WaitAll(consumers);
            Console.WriteLine("All is done..." + queue.Count);

            Console.ReadLine();

        }
    }
}
