using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    static public class MathFunctions
    {
        static int[] factorialValues = { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600 };
        internal static int GreatestCommmonDivisor(int a, int b)
        {
            while(a!=b)
            {
                if (a > b) a -= b;
                else b -= a;
            }
            return a;
        }

        internal static int LeastCommonMultiple(int a, int b)
        {
            return (a * b) / GreatestCommmonDivisor(a, b);
        }

        internal static int LCMForArray(int[] array)
        {
            int result = 0;
            bool flag = true;
            for (int i = array.Length - 1; i > 0; i--)
            {
                if (flag)
                {
                    result = MathFunctions.LeastCommonMultiple(array[i - 1], array[i]);
                    flag = false;
                }
                else result = MathFunctions.LeastCommonMultiple(array[i - 1], result);

            }
            return result;
        }

        public static int Factorial(int n)
        {
            if (n < 0) throw new Exception("Number is lower than zero.");
            if (n < 12)
                return factorialValues[n];
            /*
            int returnedValue = 1;
            for (int i = 1; i <= n; i++)
            {
                returnedValue *= i;
            }
            return returnedValue;*/
            throw new Exception("To highest value.");
        }

        internal static int Exponentation(int _base, int exponent)
        {
            int result = 1;
            for (int i = exponent; i >= 0; i--)
                result *= _base;
            return result;
        }
        
    }
}
