using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Feladat0
{
    class Program
    {
        static int[] vektor = new int[10];
        static int alsoPoz;
        static int felsoPoz;
        static int alsoDb = 0;
        static int felsoDb = 0;
        static int osszeg = 0;

        static void Main(string[] args)
        {
            /*
             * Write a program that uses two methods to count the sum of numbers in a vector. 
             * One method should start adding from the beginning, and the other from the end, 
             * stopping when they meet. The two methods should run on separate threads. 
             * The main program should generate the vector's elements. At the end, the main program 
             * should output the total sum of the numbers and how many numbers each thread added to the 
             * shared sum variable.
            */
            Random rnd = new Random();
            for (int i = 0; i < vektor.Length; i++)
            {
                vektor[i] = rnd.Next(0, 10);
                Console.WriteLine(vektor[i]);
            }
            Console.WriteLine();

            var szál1 = new Thread(() => futtat(vektor, 0));
            var szál2 = new Thread(() => futtat(vektor, vektor.Length - 1));

            szál1.Start(); szál2.Start();
            szál1.Join(); szál2.Join();

            Console.WriteLine(alsoDb);
            Console.WriteLine(felsoDb);
            Console.WriteLine(osszeg);

            Console.ReadKey();
        }

        static void futtat(int[] tömb, int index)
        {
            if (index == 0)
            {
                for (int i = 0; i < tömb.Length - 1; i++)
                {
                    lock (tömb)
                    {
                        if (felsoPoz - 1 == alsoPoz)
                        {
                            return;
                        }
                        lock (typeof(Program))
                        {
                            alsoDb++;
                            osszeg += tömb[i];
                        }
                        alsoPoz = i;
                    }
                }
            }
            else
            {
                for (int i = tömb.Length - 1; i > 0; i--)
                {
                    lock (tömb)
                    {
                        if (felsoPoz - 1 == alsoPoz)
                        {
                            return;
                        }
                        lock (typeof(Program))
                        {
                            felsoDb++;
                            osszeg += tömb[i];
                        }
                        felsoPoz = i;
                    }
                }
            }
        }
    }
}
