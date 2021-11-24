using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace readwrite_task
{
    public static class Combain
    {
        private static int count;
        private static ReaderWriterLock spinLocker = new ReaderWriterLock();

        public static void AddToCount(int value)
        {
            spinLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            count += value;
            Console.WriteLine(Combain.GetCount() + " --------- Клиент изменил на <"+value+"> значение переменной count INTEGER INTEGER");
            spinLocker.ReleaseWriterLock();
        }

        public static int GetCount()
        {
            spinLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            try
            {
                return count;
            }
            finally
            {
                spinLocker.ReleaseReaderLock();
            }
        }
    }
}
