using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task_invoke
{
    internal class Program
    {
        int timer = 6000;
        private void Worker(object sender, EventArgs e)
        {
            Console.WriteLine("Запуск экземпляра <<<Worker>>>");            
            Thread.Sleep(timer);
            Console.WriteLine("Ожидание в <"+ ((timer/1000.0)) + "> сек завершено ... Остановка экземпляра <<<Worker>>>");
        }

        private void Run()
        {
            EventHandler worker = new EventHandler(Worker);
            AsyncCaller asyncCaller = new AsyncCaller(worker);
            if (asyncCaller.Invoke(5000, this, EventArgs.Empty))
                Console.WriteLine("Завершено удачно.");
            else                
                Console.WriteLine("Превышено ожидание в 5 сек ... Внимание!!!");
        }

        static void Main(string[] args)
        {
            new Program().Run();
            Console.WriteLine("Работа завершена.");
            Console.ReadKey();
        }
    }

    public class AsyncCaller
    {
        private readonly EventHandler workerHandler;
        private Thread thread;

        public AsyncCaller(EventHandler workerHandler)
        {
            this.workerHandler = workerHandler;
        }

        private void Aborter(IAsyncResult asyncResult)
        {
            thread?.Abort();
        }

        private void Waiting(object timeout)
        {
            Thread.Sleep((int)timeout);
        }

        public bool Invoke(int timeout, object sender, EventArgs e)
        {
            thread = new Thread(Waiting);
            IAsyncResult asyncResult = workerHandler?.BeginInvoke(sender, e, Aborter, this);
            thread.Start(timeout);
            thread.Join();
            thread = null;
            return asyncResult.IsCompleted;
        }
    }



}

