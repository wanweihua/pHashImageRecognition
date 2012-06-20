using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PHashCS;
using System.Threading;

namespace PHashCSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAsynchronous();
            TestSynchronous();
            return;
        }

        private static void TestSynchronous()
        {
            string diff_1 = "diff1.jpg",
                   same_1 = "same1.jpg",
                   same_2 = "same2.jpg";

            DateTime start = DateTime.Now;

            int result_diffsame = PHashCS.PHashCS.CompareImageFiles(diff_1, same_1);
            DateTime end_diffsame = DateTime.Now;

            int result_samesame = PHashCS.PHashCS.CompareImageFiles(same_1, same_2);
            DateTime end_samesame = DateTime.Now;

            Console.WriteLine(String.Format("Images \'{0}\' and \'{1}\' have Hamming distance {2}.",
                                            new object[] { diff_1, same_1, result_diffsame }));
            Console.WriteLine(String.Format("Images \'{0}\' and \'{1}\' have Hamming distance {2}.",
                                            new object[] { same_1, same_2, result_samesame }));
            Console.WriteLine(String.Format("Diff-Same test elapsed in {0}s.",
                                            (end_diffsame - start).Seconds));
            Console.WriteLine(String.Format("Same-Same test elapsed in {0}s.",
                                            (end_samesame - end_diffsame).Seconds));
        }

        private static void TestAsynchronous()
        {
            PHashCS.EventListenerSample listener = new PHashCS.EventListenerSample();

            Thread thread1 = new Thread(CompareDiffSame);
            Thread thread2 = new Thread(CompareSameSame);

            thread1.Start();
            Console.WriteLine("Async diff-same started...");
            thread2.Start();
            Console.WriteLine("Async same-same started...");
        }

        public static void CompareDiffSame()
        {
            string diff_1 = "diff1.jpg",
                   same_1 = "same1.jpg";
            PHashCS.PHashCS.BeginCompareImageFiles(diff_1, same_1);
        }

        public static void CompareSameSame()
        {
            string same_1 = "same1.jpg",
                   same_2 = "same2.jpg";
            PHashCS.PHashCS.BeginCompareImageFiles(same_1, same_2);
        }
    }
}
