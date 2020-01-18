using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PrimeNumberGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            do
            {
                string prime2 = "";
                BigInteger prime10 = 0;
                bool isPrime = false;

                do
                {
                    prime2 = GenerateNumber(50, random);
                    prime10 = Convert.ToInt64(prime2, 2);
                    isPrime = RabinMiller(prime10, 5, random);
                }
                while (!isPrime);
                Console.WriteLine("Binary: " + prime2);
                Console.WriteLine("Decimal: " + prime10);
                Console.WriteLine("To generate another prime press any key. Escape to exit");
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static string GenerateNumber(int bits, Random random)
        {
            string number = "1";
       
            for (int i = 0; i < ( bits - 2); i++)
            {
                string part = random.Next(0, 2).ToString();
                number = number + part;
            }
            number = number + "1";

            return number;
        }

        public static bool RabinMiller(BigInteger n, int k, Random r)
        {
            if (n < 2)
            {
                return false;
            }
            if (n != 2 && n % 2 == 0)
            {
                return false;
            }
            BigInteger s = n - 1;
            while (s % 2 == 0)
            {
                s >>= 1;
            }
            for (int i = 0; i < k; i++)
            {
                BigInteger a = RandomBigInteger(n - 1) + 1;
                BigInteger temp = s;
                BigInteger mod = BigInteger.ModPow(a, temp, n);   //ModuloPower(a, temp, n);
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp = temp * 2;
                }
                if (mod != n - 1 && temp % 2 == 0)
                {
                    return false;
                }
            }
            return true;
        }

    

        static Random rand = new Random();

        // returns a evenly distributed random BigInteger from 1 to N.
        static BigInteger RandomBigInteger(BigInteger N)
        {
            BigInteger result = 0;
            do
            {
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rand.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);
            return result;
        }
    }
}
