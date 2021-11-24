using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace readwrite_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, i =>
            {                
                 Combain.AddToCount(5);                
                 Console.WriteLine(Combain.GetCount()+ " --------- Клиент получил значение переменной count INTEGER");
            });
            Console.WriteLine(Combain.GetCount());
            Console.WriteLine("Работа завершена.");
            Console.ReadKey();
        }


    }
}
