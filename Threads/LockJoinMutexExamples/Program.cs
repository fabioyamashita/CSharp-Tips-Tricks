// Example inspired from https://www.youtube.com/watch?v=5Zv8fF-KPrE&list=WL&index=4&t=4s&ab_channel=CODELLIGENT
// Thread Synchronization in C# .Net made easy! | Lock | Monitor | Mutex | Semaphore by CODELLIGENT

// Threads

using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace LockJoinMutexExamples
{
    class Program
    {
        private static object _locker = new object();

        static void Main(string[] args)
        {
            // First example - how to execute only 1 thread using Thread.Join()
            for (int i = 0; i < 2; i++)
            {
                var thread = new Thread(Dowork1);
                thread.Start();
                thread.Join();
            }

            // Second example - how to execute only 1 thread using lock
            for (int i = 0; i < 2; i++)
            {
                var thread = new Thread(Dowork2);
                thread.Start();
            }

            // Third example - how to execute only 1 thread using Monitor
            for (int i = 0; i < 2; i++)
            {
                var thread = new Thread(Dowork3);
                thread.Start();
            }
        }

        // First example - how to execute only 1 thread using Thread.Join()
        public static void Dowork1()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starting...");
            Thread.Sleep(2000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed...");
        }

        // Second example - how to execute only 1 thread using lock
        public static void Dowork2()
        {
            lock (_locker)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starting...");
                Thread.Sleep(2000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed...");
            }
        }

        // Third example - how to execute only 1 thread using Monitor
        // Difference: We can use a try catch block here
        public static void Dowork3()
        {
            try
            {
                Monitor.Enter(_locker);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starting...");
                Thread.Sleep(2000);
                // throw new Exception();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed...");
            }
            catch (Exception e)
            {
                // error logger
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }
    }
}