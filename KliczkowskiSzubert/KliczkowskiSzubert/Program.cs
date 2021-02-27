using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KliczkowskiSzubert
{
    class Program
    {


        static void RunThreads(IEnumerable<IRunnable> runnables)
        {
            var threads = new List<Thread>(runnables.Count());
            int suma = 0;
            foreach (var runnable in runnables)
            {
                var t = new Thread(runnable.Run);
                threads.Add(t);
                t.Start();
            }

            bool allFinished = false;
            while (!allFinished)
            {
                Thread.Sleep(100);
                allFinished = !runnables.Any(r => !r.HasFinished);
            }
        }

        static List<IRunnable> GenerateRunnables()
        {
            var runnables = new List<IRunnable>();
            int id = 0;
            
           
            Object myLock = new object();
            var bank = new BankAgent(id, ref myLock);
            runnables.Add(bank);
            for (; id < 10; id++)
            {
                if (id % 2 == 0)
                {
                    runnables.Add(new BankClientAgent(id, ref bank, 1, ref myLock));
                }
                else
                {
                    runnables.Add(new BankClientAgent(id, ref bank, 2, ref myLock));
                }
            }

            return runnables;
        }

        static void RunFibers(IEnumerable<IRunnable> runnables)
    {
    var timeStep = 0.0f;
    var enumerators = runnables.Select(r => r.CoroutineUpdate());

    bool allFinished = false;
        while (!allFinished)
        {
            foreach (var enumerator in enumerators)
            {
                if (enumerator.MoveNext())
                {
                    timeStep = enumerator.Current;
                }
            }

            allFinished = !runnables.Any(r => !r.HasFinished);
            Thread.Sleep(100);
        }

    }


 
    static void Main(string[] args)
    {
            var timers = new List<GoodTimer>(5);
           // int i = 1;
        /*    foreach(GoodTimer timer in timers)
            {
                timer = new GoodTimer(25 * i);
                i++;
            }*/
            for(int i =1; i<6; i++)
            {
                timers.Add(new GoodTimer((25 * i), i));
            }
            var threads = new List<Thread>(timers.Count);
            foreach (var timer in timers)
            {
                var t = new Thread(timer.StartGoodTimer);
                threads.Add(t);
                t.Start();
            }
            /*var runnables = GenerateRunnables();
            Program.RunThreads(runnables);*/
            //Program.RunFibers(runnables);
            Console.ReadKey();
    }
}
}
