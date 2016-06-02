using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    static public class WithoutRepetition
    {
        static public int[] ReversePermutation(int[] Array)
        {
            if (!CheckOnePermutation(Array)) throw new Exception("Bad format of permutation.");

            int Length = Array.Length;
            int[,] TwoRowsArray = new int[2, Length];

            for (int i = 0; i < Length; i++)
            {
                TwoRowsArray[0, i] = i + 1;
                TwoRowsArray[1, i] = Array[i];

            }

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length - 1; j++)
                {
                    if (TwoRowsArray[1, j] > TwoRowsArray[1, j + 1])
                    {
                        int Temp = TwoRowsArray[0, j], Temp2 = TwoRowsArray[1, j];
                        TwoRowsArray[0, j] = TwoRowsArray[0, j + 1];
                        TwoRowsArray[1, j] = TwoRowsArray[1, j + 1];
                        TwoRowsArray[0, j + 1] = Temp;
                        TwoRowsArray[1, j + 1] = Temp2;
                    }
                }
            }

            int[] Table = new int[Length];

            for (int i = 0; i < Length; i++)
            {
                Table[i] = TwoRowsArray[0, i];
            }

            return Table;
        }

        static public bool CheckOnePermutation(int[] array)
        {
            bool not = false;

            for (int i = 1; i <= array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (j == 0)
                    {
                        not = false;
                    }

                    if (array[j] == i)
                        not = true;

                    if (j == (array.Length - 1) && !not)
                        return false;
                }
            }
            return true;
        }

        static public bool CheckTwoPermutations(int[] array1, int[] array2)
        {
            if (array1.Length == array2.Length)
                if (CheckOnePermutation(array1))
                    if (CheckOnePermutation(array2)) return true;
            return false;
        }

        static public int[] FoldingPermutations(int[] array1, int[] array2)
        {
            if (!CheckTwoPermutations(array1, array2)) throw new Exception("Bad format of permutations");
            int[] returnedArray = new int[array1.Length];
            for (int i = 0; i < array1.Length; i++)
            {
                returnedArray[i] = array1[array2[i] - 1];
            }
            return returnedArray;
        }

        static public int[][] VectorToCycle(int[] array)
        {
            if (!CheckOnePermutation(array)) throw new Exception("Bad format of permutation.");
            int number = 0;
            Collection[] collection = new Collection[array.Length];
            collection[number] = new Collection();
            bool[] tab_bool = new bool[array.Length];
            for (int i = 0; i < tab_bool.Length; i++)
                tab_bool[i] = true;

            for (int i = 0; i < array.Length; i++)
            {

                if (tab_bool[i])
                {
                    collection[number].list.Add(array[i]);
                    tab_bool[i] = false;
                    int j = array[i] - 1;
                    while (tab_bool[j])
                    {
                        collection[number].list.Add(array[j]);
                        tab_bool[j] = false;
                        j = array[j] - 1;
                    }
                    number++;
                    collection[number] = new Collection();
                }
            }

            int[][] returnedArray = new int[number][];

            for (int i = 0; i < number; i++)
            {
                returnedArray[i] = collection[i].list.ToArray();
            }

            return returnedArray;
        }

        static public int RankOfPermutation(int[] array)
        {
            int[][] arrayCycle = VectorToCycle(array);
            int[] arrayLCM = new int[arrayCycle.Length];
            for (int i = 0; i < arrayCycle.Length; i++) arrayLCM[i] = arrayCycle[i].Length;
            return MathFunctions.LCMForArray(arrayLCM);

        }

        static public int TypeOfPermutation(int[] array)
        {
            int[][] arrayCycle = WithoutRepetition.VectorToCycle(array);
            int[] arrayOfValues = new int[array.Length];
            int addition = 0;
            for (int i = 0; i < arrayCycle.Length; i++)
                arrayOfValues[arrayCycle[i].Length]++;
            for (int i = 0; i < arrayOfValues.Length; i++)
                addition += i * arrayOfValues[i];
            return addition;
        }

        static public bool IfInvolution(int[] array)
        {
            if (!CheckOnePermutation(array)) throw new Exception("Bad format of permutation.");
            int[] reversedArray = WithoutRepetition.ReversePermutation(array);
            for (int i = 0; i < array.Length; i++)
                if (array[i] != reversedArray[i]) return false;
            return true;
        }

        static public bool IfDisorder(int[] array)
        {
            if (!CheckOnePermutation(array)) throw new Exception("Bad format of permutation.");
            for (int i = 0; i < array.Length; i++)
                if (array[i] == i + 1) return true;
            return false;
        }


    }
}
