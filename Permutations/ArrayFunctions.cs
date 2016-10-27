using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    static public class ArrayFunctions
    {
        internal static int DecreasingSequence(int[] array) // wyznacza indeks ciągu maląjącego tablicy
        {
            int index = 0;
            bool flag, currentFlag = false; //currentFlag to flaga w przypadku gdy podczas wyszukiwania jest pierwszy element ciągu

            for (int i = 0; i < array.Length; i++)
            {
                flag = true;
                for (int j = i + 1; j < array.Length; j++)
                    if (array[i] < array[j])
                        flag = false;
                if (!flag)
                    currentFlag = false;
                else
                    if (!currentFlag)
                {
                    currentFlag = true;
                    index = i;
                }
            }

            return index;
        }

        internal static int[] ReverseArray(int[] array)
        {
            int[] returnedArray = new int[array.Length];
            int index = 0;

            for (int i = array.Length - 1; i >= 0; i--)
            {
                returnedArray[index] = array[i];
                index++;
            }

            return returnedArray;
        }

        internal static List<int> ReversedList(List<int> list)
        {
            int[] array = list.ToArray();
            int[] reversedArray = ReverseArray(array);
            List<int> returnedList = reversedArray.ToList();

            return returnedList;
        }

        public static bool CompareIntArrays(int[] array1, int[] array2)
        {
            if (array1.Length != array2.Length)
                return false;
            for (int i = 0; i < array1.Length; i++)
                if (array1[i] != array2[i])
                    return false;
            return true;
        }
    }
}
