using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    static public class WithoutRepetition
    {
        static public int[] ReversePermutation(int[] permutation)
        {
            Check(permutation);

            int length = permutation.Length;
            int[,] TwoRowsPermutation = new int[2, length];

            for (int i = 0; i < length; i++)
            {
                TwoRowsPermutation[0, i] = i + 1;
                TwoRowsPermutation[1, i] = permutation[i];

            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    if (TwoRowsPermutation[1, j] > TwoRowsPermutation[1, j + 1])
                    {
                        int temp = TwoRowsPermutation[0, j], temp2 = TwoRowsPermutation[1, j];
                        TwoRowsPermutation[0, j] = TwoRowsPermutation[0, j + 1];
                        TwoRowsPermutation[1, j] = TwoRowsPermutation[1, j + 1];
                        TwoRowsPermutation[0, j + 1] = temp;
                        TwoRowsPermutation[1, j + 1] = temp2;
                    }
                }
            }

            int[] reversePermutation = new int[length];

            for (int i = 0; i < length; i++)
            {
                reversePermutation[i] = TwoRowsPermutation[0, i];
            }

            return reversePermutation;
        }

        static public bool CheckOnePermutation(int[] permutation)
        {
            if (permutation.Length < 2) return false;
            bool not = false;

            for (int i = 1; i <= permutation.Length; i++)
            {
                for (int j = 0; j < permutation.Length; j++)
                {
                    if (j == 0)
                        not = false;
                    if (permutation[j] == i)
                        not = true;
                    if (j == (permutation.Length - 1) && !not)
                        return false;
                }
            }
            return true;
        }

        static bool CheckTwoPermutations(int[] permutation1, int[] permutation2)
        {
            if (permutation1.Length == permutation2.Length)
                if (CheckOnePermutation(permutation1))
                    if (CheckOnePermutation(permutation2)) return true;
            return false;
        }

        static public int[] CompositionOfPermutation(int[] permutation1, int[] permutation2)
        {
            if (!CheckTwoPermutations(permutation1, permutation2)) throw new Exception("Bad format of permutations");
            int[] returnedArray = new int[permutation1.Length];
            for (int i = 0; i < permutation1.Length; i++)
            {
                returnedArray[i] = permutation1[permutation2[i] - 1];
            }
            return returnedArray;
        }

        static public int[][] PermutationToCycle(int[] permutation)
        {
            Check(permutation);
            int number = 0;
            Collection[] collection = new Collection[permutation.Length];
            collection[number] = new Collection();
            bool[] tab_bool = new bool[permutation.Length];
            for (int i = 0; i < tab_bool.Length; i++)
                tab_bool[i] = true;

            for (int i = 0; i < permutation.Length; i++)
            {

                if (tab_bool[i])
                {
                    collection[number].list.Add(permutation[i]);
                    tab_bool[i] = false;
                    int j = permutation[i] - 1;
                    while (tab_bool[j])
                    {
                        collection[number].list.Add(permutation[j]);
                        tab_bool[j] = false;
                        j = permutation[j] - 1;
                    }
                    number++;
                    if(number<permutation.Length) collection[number] = new Collection();
                }
            }

            int[][] cyclesToSort = new int[number][];

            
            for (int i = 0; i < number; i++)
            {
                cyclesToSort[i] = collection[i].list.ToArray();
            }

            for(int i=0;i<cyclesToSort.Length;i++)
                for(int j=0;j<cyclesToSort.Length-1;j++)
                    if(cyclesToSort[j].Min()>cyclesToSort[j+1].Min())
                    {
                        int[] temp = cyclesToSort[j + 1];
                        cyclesToSort[j] = cyclesToSort[j + 1];
                        cyclesToSort[j + 1] = temp;
                    }
            int[][] returnedPermutation = new int[cyclesToSort.Length][];
            int indexOfCycle = 0;
            foreach(int[] cycleToSort in cyclesToSort)
            {
                int maxValue = cycleToSort.Max();
                bool flag = true;
                int indexOfMax = 0;
                int index = 0;
                while(flag)
                {
                    if(cycleToSort[index]==maxValue)
                    {
                        indexOfMax = index;
                        flag = false;
                    }
                    else
                        index++;
                }
                returnedPermutation[indexOfCycle] = new int[cycleToSort.Length];
                int indexOfElement = 0;
                for (int i = indexOfMax; i < cycleToSort.Length; i++)
                {
                    returnedPermutation[indexOfCycle][indexOfElement] = cycleToSort[i];
                    indexOfElement++;
                }
                for (int i = 0; i < indexOfMax; i++)
                {
                    returnedPermutation[indexOfCycle][indexOfElement] = cycleToSort[i];
                    indexOfElement++;
                }
                indexOfCycle++;
            }

            return returnedPermutation;
        }

        static public int OrderOfPermutation(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = PermutationToCycle(permutation);
            if (permutationCycle.Length == 1) return permutationCycle[0].Length;
            int[] arrayLCM = new int[permutationCycle.Length];
            for (int i = 0; i < permutationCycle.Length; i++) arrayLCM[i] = permutationCycle[i].Length;
            return MathFunctions.LCMForArray(arrayLCM);

        }
        /*
        static public int TypeOfPermutation(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = VectorToCycle(permutation);
            int[] arrayOfValues = new int[permutation.Length+1];
            int addition = 0;
            for (int i = 0; i < permutationCycle.Length; i++)
                arrayOfValues[permutationCycle[i].Length]++;
            for (int i = 0; i < arrayOfValues.Length; i++)
                addition += i * arrayOfValues[i];
            return addition;
        }
        */
        static public int[] TypeOfPermutation(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = WithoutRepetition.PermutationToCycle(permutation);
            int[] returnedVector = new int[permutation.Length + 1];
            //typ tutaj jest przedstawiony jako [indeks tablicy] do potęgi wartości tablicy pod danym indeksem, 
            //pierwszy indeks [0] będzie zawsze zero, 
            //dla jasności tworzy się tablicę o jeden większą żeby zaczynać od jednego
            for (int i = 0; i < permutationCycle.Length; i++)
                returnedVector[permutationCycle[i].Length]++;
            return returnedVector;
        }
        static public bool IsInvolution(int[] permutation)
        {
            Check(permutation);
            int[] reversePermutation = ReversePermutation(permutation);
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] != reversePermutation[i]) return false;
            return true;
        }

        static public bool IsDeregement(int[] permutation)
        {
            Check(permutation);
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] == i + 1) return false;
            return true;
        }
        /*
        static public int CountDeregements(int[] permutation)
        {
            int disorders = 0;
            Check(permutation);
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i; j < permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i]) disorders++;
                }
            }
            return disorders;
        }*/

        static public bool IsEven(int[] permutation)
        {
            Check(permutation);
            if (SignOfPermutation1(permutation) == 1)
                return true;
            return false;
        }

        static public bool IsOdd(int[] permutation)
        {
            Check(permutation);
            if (SignOfPermutation1(permutation) == 1)
                return true;
            return true;
        }

        /*static public int SignOfPermutation(int[] permutation)
        {
            Check(permutation);
            if (IsEven(permutation)) return 1;
            return -1;
        }*/

        static public int SignOfPermutation1(int[] permutation) //ze wzoru (-1)^(liczby inwersji)
        {
            Check(permutation);
            //int sign = MathFunctions.Exponentation(-1, InversionsCount(permutation));
            int sign = -1;
            if (InversionsCount(permutation) % 2 == 0)
                sign = 1;
            return sign;
        }

        static public int SignOfPermutation2(int[] permutation) //ze wzoru (-1)^(n-c(p))
        {
            Check(permutation);
            int sign = -1;
            if ((permutation.Length - AllCyclesCount(permutation)) % 2 == 0)
                sign = 1;
            //int sign = MathFunctions.Exponentation(-1, permutation.Length-AllCyclesCount(permutation));
            return sign;
        }

        static public int SignOfPermutation3(int[] permutation) //ze wzoru (-1)^(n+c(p))
        {
            Check(permutation);
            int sign = -1;
            if ((permutation.Length + AllCyclesCount(permutation)) % 2 == 0)
                sign = 1;
            //int sign = MathFunctions.Exponentation(-1, permutation.Length + AllCyclesCount(permutation));
            return sign;
        }

        static public int SignOfPermutation4(int[] permutation) //ze wzoru (-1)^(cpd(p))
        {
            Check(permutation);
            int sign = -1;
            if (EvenCyclesCount(permutation) % 2 == 0)
                sign = 1;
            //int sign = MathFunctions.Exponentation(-1, EvenCyclesCount(permutation));
            return sign;
        }

        static public bool IsTransposition(int[] permutation)
        {
            Check(permutation);
            bool flag = false;
            int[][] permutationCycle = WithoutRepetition.PermutationToCycle(permutation);
            for (int i = 0; i < permutationCycle.Length; i++)
            {
                if (permutationCycle[i].Length != 1)
                    if (permutationCycle[i].Length == 2)
                    {
                        if (!flag) flag = true;
                        else return false;
                    }
                    else return false;
            }

            if (flag) return true;
            return false;
        }

        static void Check(int[] permutation)
        {
            if (!CheckOnePermutation(permutation)) throw new Exception("Bad format of permutation.");
        }

        static public int[] PowerOfPermutation(int[] permutation, int power)
        {
            Check(permutation);
            if (power < 0)
            {
                power = -power;
                permutation = ReversePermutation(permutation);
            }
            int order = OrderOfPermutation(permutation);
            power = power % order;
            int[] returnedPermutation = new int[permutation.Length];
            if (power == 0)
            {
                for (int i = 0; i < permutation.Length; i++)
                    returnedPermutation[i] = i + 1;
                return returnedPermutation;
            }
            else if (power == 1) return permutation;


            returnedPermutation = permutation;
            for (int i = 2; i <= power; i++)
                returnedPermutation = CompositionOfPermutation(returnedPermutation, permutation);

            return returnedPermutation;
        }

        static public bool IsOneCycle(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = PermutationToCycle(permutation);
            if (permutationCycle.Length == 1) return true;
            return false;
        }

        static public int[] InversionVector(int[] permutation)
        {
            Check(permutation);
            int[] vector = new int[permutation.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = 0;
            }
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i+1; j <permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i]) vector[i]++;
                }
            }

            return vector;
        }

        static public int AllCyclesCount(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = PermutationToCycle(permutation);
            return permutationCycles.Length;
        }

        static public int EvenCyclesCount(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = WithoutRepetition.PermutationToCycle(permutation);
            int count = 0;
            for (int i = 0; i < permutationCycles.Length; i++)
                if (permutationCycles[i].Length % 2 == 0) count++;
            return count;
        }

        static public int[,] PermutationToMatrix(int[] permutation)
        {
            Check(permutation);
            int[,] matrix = new int[permutation.Length, permutation.Length];
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = 0; j < permutation.Length; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < permutation.Length; i++)
                matrix[permutation[i] - 1, i] = 1;
            return matrix;
        }

        static public int[] CycleToPermutation(int[][] cyclePermutation)
        {
            if (!CheckCyclePermutation(cyclePermutation)) throw new Exception("Bad format of permutation");
            int count = CountElements(cyclePermutation);

            int[] vectorPermutation = new int[count];
            for (int i = 0; i < cyclePermutation.Length; i++)
            {
                for (int j = 0; j < cyclePermutation[i].Length; j++)
                {
                    if (j != (cyclePermutation[i].Length - 1))
                        vectorPermutation[cyclePermutation[i][j] - 1] = cyclePermutation[i][j + 1];
                    else
                        vectorPermutation[cyclePermutation[i][j] - 1] = cyclePermutation[i][0];
                }
            }

            return vectorPermutation;
        }

        static int CountElements(int[][] permutation)
        {
            //if (!CheckCyclePermutation(permutation)) throw new Exception("Bad format of permutation");
            int count = 0;
            for (int i = 0; i < permutation.Length; i++)
                for (int j = 0; j < permutation[i].Length; j++)
                    count++;
            return count;
        }

        static public bool CheckCyclePermutation(int[][] permutation)
        {
            int length = CountElements(permutation);
            bool[] tab = new bool[length];
            for (int i = 0; i < length; i++)
                tab[i] = false;
            try
            {
                for (int i = 0; i < permutation.Length; i++)
                    for (int j = 0; j < permutation[i].Length; j++)
                        tab[permutation[i][j] - 1] = true;
            }
            catch (Exception e)
            {
                return false;
            }

            for (int i = 0; i < tab.Length; i++)
            {
                if (!tab[i]) return false;
            }

            return true;
        }

        static public int InversionsCount(int[] permutation)
        {
            Check(permutation);
            int count = 0;
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i +1; j<permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i]) count++;
                }
            }

            return count;
        }
        /*
        static public int[,] ListOfInversions(int[] permutation)
        {
            int numberOfInversion = InversionsCount(permutation);
            int[,] matrix = new int[numberOfInversion, 2];
            int index = 0;
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i + 1; j < permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i])
                    {
                        matrix[index, 0] = permutation[i];
                        matrix[index, 1] = permutation[j];
                        index++;
                    }
                }
            }
            return matrix;
        }
        */
        static public int PermutationIndexInLexOrder(int[] permutation) // LO - Lexicographical order
        {
            Check(permutation);

            int returnedValue = 0;
            int[] vectorOfInversion = InversionVector(permutation);
            int number = vectorOfInversion.Length - 1;

            for (int i = 0; i < vectorOfInversion.Length; i++)
            {
                returnedValue += vectorOfInversion[i] * MathFunctions.Factorial(number);
                number--;
            }

            return returnedValue;
        }

        static public int[] VectorFromNumber(int number, int n) //wektor inwersji z numeru i liczebności zbioru
        {
            if ((number > MathFunctions.Factorial(n)) || number < 0 || n < 2) throw new Exception("Bad value of typed data.");

            int[] vectorOfInversion = new int[n];
            int factorial;
            n--;
            for (int i = 0; i < vectorOfInversion.Length; i++)
            {
                factorial = MathFunctions.Factorial(n);
                vectorOfInversion[i] = number / factorial;
                number = number % factorial;
                n--;
            }

            return vectorOfInversion;
        }

       

        static public int[] VectorToPermutation(int[] vectorOfInversions)
        {
            if (!CheckVectorOfInversion(vectorOfInversions)) throw new Exception("Bad format of vector.");

            int[] indentityOfPermutation = CreateIdentityPermutation(vectorOfInversions.Length);
            bool[] logicArray = new bool[vectorOfInversions.Length]; // false-nieużyta, true-użyta
            int[] permutation = new int[vectorOfInversions.Length];

            for (int i = 0; i < permutation.Length; i++)
            {
                int number = vectorOfInversions[i], index = 0;

                while (number != 0)
                {
                    if (logicArray[index])
                        index++;
                    else
                    {
                        index++;
                        number--;
                    }
                }
                while (logicArray[index])
                    index++;
                permutation[i] = indentityOfPermutation[index];
                logicArray[index] = true;
            }

            return permutation;
        }

        static public int[] CreateIdentityPermutation(int n)
        {
            if (n < 2) throw new Exception("Bad number elements of permutation.");

            int[] identityPermutation = new int[n];

            for (int i = 0; i < n; i++)
                identityPermutation[i] = i + 1;

            return identityPermutation;
        }

        static public bool CheckVectorOfInversion(int[] vector)
        {
            int max = vector.Length - 1;

            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > max) return false;
                max--;
            }

            return true;
        }

        static public int[] PermutationFromLexOrderIndex(int number, int n) //lexicographical order
        {
            int[] vectorOfInversions = VectorFromNumber(number, n);
            int[] permutation = VectorToPermutation(vectorOfInversions);

            return permutation;
        }

        static public int[][] AllPermutationsInLexOrder(int n)
        {
            if (n < 2) throw new Exception("Bad number of length.");

            int count = MathFunctions.Factorial(n);
            int[][] permutations = new int[count][];
            permutations[0] = WithoutRepetition.CreateIdentityPermutation(n);

            for (int i = 1; i < count; i++)
                permutations[i] = NextElementLexicographical(permutations[i - 1]);
                          
            return permutations;
        }

        static int[] NextElementLexicographical(int[] previousPermutation)
        {
            int[] returnedPermutation = new int[previousPermutation.Length];
            int index = ArrayFunctions.DecreasingSequence(previousPermutation); //index przechowuje indeks pierwszego elementu ciągu malejącego w tablicy

            if (index == 0)
            {
                returnedPermutation = ArrayFunctions.ReverseArray(WithoutRepetition.CreateIdentityPermutation(previousPermutation.Length));
            }
            else
            {
                int indexMin;
                List<int> list = new List<int>();
                for (int j = previousPermutation.Length - 1; ; j--)
                    if (previousPermutation[j] > previousPermutation[index - 1])
                    {
                        indexMin = j;
                        break;
                    }


                for (int j = 0; j < index - 1; j++)
                    list.Add(previousPermutation[j]);
                list.Add(previousPermutation[indexMin]);

                List<int> toReverse = new List<int>();

                for (int j = index; j < previousPermutation.Length; j++)
                {

                    if (j != indexMin)
                        toReverse.Add(previousPermutation[j]);
                    else
                        toReverse.Add(previousPermutation[index - 1]);
                }

                List<int> reversedList = ArrayFunctions.ReversedList(toReverse);

                foreach (int j in reversedList)
                    list.Add(j);

                returnedPermutation = list.ToArray();


            }

            return returnedPermutation;
        }

        static public int[][] AllPermutationsOfGivenType(int[] type)
        {
            List<int> listOfIndex = new List<int>();
            int factorial = MathFunctions.Factorial(type.Length - 1);

            int[] permutation = CreateIdentityPermutation(type.Length - 1);

            if (ArrayFunctions.CompareIntArrays(TypeOfPermutation(permutation), type))
                listOfIndex.Add(0);
            int index = 1;
            while (index != factorial)
            {
                permutation = NextElementLexicographical(permutation);
                if (ArrayFunctions.CompareIntArrays(TypeOfPermutation(permutation), type))
                    listOfIndex.Add(index);
                index++;
            }
            int[][] returnedArray = new int[listOfIndex.Count][];
            int indexOfArray = 0;
            foreach (int i in listOfIndex)
            {
                returnedArray[indexOfArray] = PermutationFromLexOrderIndex(i + 1, type.Length - 1);
                indexOfArray++;
            }

            return returnedArray;
        }

        static public int[][] AllPermutationsOfGivenOrder(int order, int length)
        {
            if (order < 1)
                throw new Exception("Bad number of order.");
            int[] permutation = WithoutRepetition.CreateIdentityPermutation(length);
            if (WithoutRepetition.OrderOfPermutation(permutation) == order)
            {
                int[][] returnedPermutation = new int[1][];
                returnedPermutation[0] = permutation;
                return returnedPermutation;
            }
            List<int> listOfIndex = new List<int>();
            int factorial = MathFunctions.Factorial(length);
            int index = 1;
            while (index != factorial)
            {
                permutation = WithoutRepetition.NextElementLexicographical(permutation);
                if (WithoutRepetition.OrderOfPermutation(permutation) == order)
                    listOfIndex.Add(index);
                index++;
            }
            int[][] returnedArray = new int[listOfIndex.Count][];
            int indexOfArray = 0;
            foreach (int i in listOfIndex)
            {
                returnedArray[indexOfArray] = WithoutRepetition.PermutationFromLexOrderIndex(i + 1, length);
                indexOfArray++;
            }
            return returnedArray;
        }

        public static int[][] RootOfPermutation(int[] permutation, int root)
        {
            Check(permutation);
            List<int[]> list = new List<int[]>();
            int[] testedPermutation = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
            if (ArrayFunctions.CompareIntArrays(permutation, WithoutRepetition.PowerOfPermutation(testedPermutation, root)))
                list.Add(testedPermutation);
            for (int i = 1; i < MathFunctions.Factorial(permutation.Length); i++)
            {
                testedPermutation = WithoutRepetition.NextElementLexicographical(testedPermutation);
                if (ArrayFunctions.CompareIntArrays(permutation, WithoutRepetition.PowerOfPermutation(testedPermutation, root)))
                    list.Add(testedPermutation);
            }
            int[][] toReturn = list.ToArray();
            return toReturn;
        }

        static int[] NextElementAntilexicographical(int[] previousPermutation)
        {
            int[] tempPermutation = new int[previousPermutation.Length];
            for (int i = 0; i < previousPermutation.Length; i++)
                tempPermutation[i] = previousPermutation[i];
            int[] returnedPermutation = new int[tempPermutation.Length];
            int index = 0;
            for (int i = 0; i < tempPermutation.Length - 1; i++)
            {
                if (tempPermutation[i] < tempPermutation[i + 1])
                {
                    index = i + 1;
                    break;
                }
            }
            int indexOfMin = 0;
            for (int i = 0; i < index; i++)
            {
                if (tempPermutation[index] > tempPermutation[i])
                {
                    indexOfMin = i;
                    break;
                }
            }
            //Console.WriteLine("index: " + index + "min: " + indexOfMin);

            int temp = tempPermutation[indexOfMin];
            tempPermutation[indexOfMin] = tempPermutation[index];
            tempPermutation[index] = temp;
            int counter = 0;
            for (int i = index - 1; i >= 0; i--)
            {
                returnedPermutation[counter] = tempPermutation[i];
                counter++;
            }
            for (int i = index; i < tempPermutation.Length; i++)
            {
                returnedPermutation[counter] = tempPermutation[i];
                counter++;
            }
            return returnedPermutation;
        }

        static public int[][] AllPermutationsInAntiLexOrder(int n)
        {
            if (n < 2) throw new Exception("Bad number of length.");
            int count = MathFunctions.Factorial(n);
            int[] permutation = WithoutRepetition.CreateIdentityPermutation(n);
            int[][] returnedPermutations = new int[count][];
            returnedPermutations[0] = permutation;
            for (int i = 1; i < count; i++)
                returnedPermutations[i] = NextElementAntilexicographical(returnedPermutations[i - 1]);
            return returnedPermutations;
        }

        static bool IsAnyIdentity(int[][] permutations)
        {
            int[] e = WithoutRepetition.CreateIdentityPermutation(permutations[0].Length);
            for (int i = 0; i < permutations.Length; i++)
                if (ArrayFunctions.CompareIntArrays(permutations[i], e))
                    return true;
            return false;
        }

        static bool IsAnyReverse(int[][] permutations)
        {
            for (int i = 0; i < permutations.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < permutations.Length; j++)
                {
                    if (!flag)
                    {
                        int[] reverse = WithoutRepetition.ReversePermutation(permutations[j]);
                        if (ArrayFunctions.CompareIntArrays(permutations[i], reverse))
                            flag = true;
                    }
                }
                if (!flag)
                    return false;
            }
            return true;
        }

        static bool IsAnyComposition(int[][] permutations)
        {
            for (int i = 0; i < permutations.Length; i++)
            {
                for (int j = 0; j < permutations.Length; j++)
                {
                    //if (j != i)
                    //{
                        int[] composition = WithoutRepetition.CompositionOfPermutation(permutations[i], permutations[j]);
                        bool flag = false;
                        for (int k = 0; k < permutations.Length; k++)
                        {
                            if (!flag)
                            {
                                if (ArrayFunctions.CompareIntArrays(composition, permutations[k]))
                                {
                                    flag = true;
                                    //Console.WriteLine("i: " + i + " j: " + j);
                                }
                            }
                        }
                        if (!flag)
                            return false;
                    //}
                }
            }
            return true;
        }

        public static bool IsPermutationGroup(int[][] permutations)
        {
            if (IsAnyComposition(permutations) && IsAnyIdentity(permutations) && IsAnyReverse(permutations))
                return true;
            return false;
        }

        static public int[] AntiInversionVector(int[] permutation)
        {
            Check(permutation);
            int[] vector = new int[permutation.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = 0;
            }
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (permutation[j] > permutation[i]) vector[i]++;
                }
            }

            return vector;
        }

        static public long PermutationIndexInAntiLexOrder(int[] permutation) // ALO - Anti Lexicographical order
        {
            Check(permutation);

            int returnedValue = 0;
            int[] vectorOfInversion = AntiInversionVector(permutation);
            int number = vectorOfInversion.Length - 1;

            for (int i = vectorOfInversion.Length - 1; i >= 0; i--)
            {
                returnedValue += vectorOfInversion[i] * MathFunctions.Factorial(number);
                number--;
            }

            return returnedValue;
        }

        static int[] AntiVectorFromNumber(int number, int n) //antywektor inwersji z numeru i liczebności zbioru
        {
            int[] inversionVector = WithoutRepetition.VectorFromNumber(number, n);
            int[] antiInversionVector = new int[n];
            int index = n - 1;
            for (int i = 0; i < antiInversionVector.Length; i++)
            {
                antiInversionVector[i] = inversionVector[index];
                index--;
            }
            return antiInversionVector;
        }

        static int[] AntiVectorToPermutation(int[] antiVectorOfInversions)
        {
            //if (!CheckVectorOfInversion(vectorOfInversions)) throw new Exception("Bad format of vector.");

            int[] indentityOfPermutation = WithoutRepetition.CreateIdentityPermutation(antiVectorOfInversions.Length);
            bool[] logicArray = new bool[antiVectorOfInversions.Length]; // false-nieużyta, true-użyta
            int[] permutation = new int[antiVectorOfInversions.Length];

            for (int i = permutation.Length - 1; i >= 0; i--)
            {
                int number = antiVectorOfInversions[i], index = permutation.Length - 1;

                while (number != 0)
                {
                    if (logicArray[index])
                        index--;
                    else
                    {
                        index--;
                        number--;
                    }
                }
                while (logicArray[index])
                    index--;
                permutation[i] = indentityOfPermutation[index];
                logicArray[index] = true;
            }

            return permutation;
        }

        static public int[] PermutationFromAntiLexOrderIndex(int number, int n)
        {
            {
                int[] antiVectorOfInversions = AntiVectorFromNumber(number, n);
                int[] permutation = AntiVectorToPermutation(antiVectorOfInversions);

                return permutation;
            }
        }

        static public int OddCyclesCount(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = WithoutRepetition.PermutationToCycle(permutation);
            int count = 0;
            for (int i = 0; i < permutationCycles.Length; i++)
                if (permutationCycles[i].Length % 2 == 1) count++;
            return count;
        }


        static public int FixedPointsCount(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = WithoutRepetition.PermutationToCycle(permutation);
            int count = 0;
            for (int i = 0; i < permutationCycles.Length; i++)
                if (permutationCycles[i].Length == 1)
                    count++;
            return count;
        }

        public static int[][] PermutationToCompositionOfTranpositions1(int[] permutation)
        {
            Check(permutation);
            if (isIdentity(permutation))
                throw new Exception("Permutation is identity. There aren't any transposistions.");
            int[][] permutationCycle = WithoutRepetition.PermutationToCycle(permutation);
            int[] identity = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
            int counter = 0;
            for (int i = 0; i < permutationCycle.Length; i++)
                if (permutationCycle[i].Length > 1)
                    counter += (permutationCycle[i].Length - 1);
            int[][] transpositions = new int[counter][];
            int indexOfTransposition = 0;
            int[][] cyclesInNewOrder = new int[permutationCycle.Length][];
            for (int i = 0; i < permutationCycle.Length; i++)
            {
                int min = permutationCycle[i].Min();
                int indexOfMin = 0;
                for (int j = 0; j < permutationCycle[i].Length; j++)
                    if (permutationCycle[i][j] == min)
                        indexOfMin = j;
                int index = 0;
                cyclesInNewOrder[i] = new int[permutationCycle[i].Length];
                for (int j = indexOfMin; j < permutationCycle[i].Length; j++)
                {
                    cyclesInNewOrder[i][index] = permutationCycle[i][j];
                    index++;
                }
                for (int j = 0; j < indexOfMin; j++)
                {
                    cyclesInNewOrder[i][index] = permutationCycle[i][j];
                    index++;
                }

                int indexOfMinVec = 0;

                for (int j = 0; j < identity.Length; j++)
                    if (identity[j] == min)
                        indexOfMinVec = j;

                for (int j = cyclesInNewOrder[i].Length - 1; j > 0; j--)
                {
                    int indexOfJ = 0;
                    for (int k = 0; k < identity.Length; k++)
                        if (identity[k] == cyclesInNewOrder[i][j])
                            indexOfJ = k;
                    transpositions[indexOfTransposition] = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
                    transpositions[indexOfTransposition][indexOfMinVec] = transpositions[indexOfTransposition][indexOfJ];
                    transpositions[indexOfTransposition][indexOfJ] = min;
                    indexOfTransposition++;
                }
            }
            return transpositions;
        }

        public static string PermutationGroupFacts(int[][] permutations)
        {
            if (!WithoutRepetition.IsPermutationGroup(permutations))
                throw new Exception("Permutations aren't a group.");
            int[][] permutationsNewOrder = new int[permutations.Length][];
            permutationsNewOrder[0] = WithoutRepetition.CreateIdentityPermutation(permutations[0].Length);
            int counter = 1;
            int indexOfPermutation = 0;
            while (counter < permutations.Length)
            {
                if (ArrayFunctions.CompareIntArrays(permutations[indexOfPermutation], permutationsNewOrder[0]))
                    indexOfPermutation++;
                else
                {
                    permutationsNewOrder[counter] = permutations[indexOfPermutation];
                    indexOfPermutation++;
                    counter++;
                }
            }
            string toReturn = "";
            for (int i = 1; i < permutationsNewOrder.Length; i++)
            {
                toReturn += "p" + i + "=<";
                for (int j = 0; j < permutationsNewOrder[i].Length; j++)
                {
                    if (j != permutationsNewOrder[i].Length - 1)
                        toReturn += permutationsNewOrder[i][j] + ",";
                    else
                        toReturn += permutationsNewOrder[i][j];
                }
                toReturn += ">\n";
            }
            toReturn += "\n";
            for (int i = 1; i < permutationsNewOrder.Length; i++)
            {
                for (int j = 1; j < permutationsNewOrder.Length; j++)
                {

                    int[] composition = WithoutRepetition.CompositionOfPermutation(permutationsNewOrder[i], permutationsNewOrder[j]);
                    bool flag = false;
                    for (int k = 1; k < permutationsNewOrder.Length; k++)
                    {
                        if (!flag)
                        {
                            if (ArrayFunctions.CompareIntArrays(composition, permutationsNewOrder[k]))
                            {
                                toReturn += "p" + i + "*p" + j + "=p" + k + "\n";
                                flag = true;
                                //Console.WriteLine("i: " + i + " j: " + j);
                            }

                        }


                    }
                    if (!flag)
                        toReturn += "p" + i + "*p" + j + "=e\n";

                }
            }
            toReturn += "\n";
            for (int i = 1; i < permutationsNewOrder.Length; i++)
            {
                bool flag = false;
                for (int j = 1; j < permutationsNewOrder.Length; j++)
                {
                    if (!flag)
                    {
                        int[] reverse = WithoutRepetition.ReversePermutation(permutationsNewOrder[j]);
                        if (ArrayFunctions.CompareIntArrays(permutationsNewOrder[i], reverse))
                            toReturn += "(p" + i + ")^(-1)=p" + j + "\n";
                    }
                }

            }
            return toReturn;
        }
        static public int[][] PermutationToCompositionOfTranpositions2(int[] permutation)
        {
            Check(permutation);
            if (isIdentity(permutation))
                throw new Exception("Permutation is identity. There aren't any transposistions.");
            int[][] permutationCycle = WithoutRepetition.PermutationToCycle(permutation);
            int[] identity = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
            int count = 0;
            for (int i = 0; i < permutationCycle.Length; i++)
                if (permutationCycle[i].Length > 1)
                    count += permutationCycle[i].Length - 1;
            int[][] transpositions = new int[count][];
            int indexOfTransposition = 0;
            int[][] cyclesInNewOrder = new int[permutationCycle.Length][];
            for (int i = 0; i < permutationCycle.Length; i++)
            {
                int min = permutationCycle[i].Min();
                int indexOfMin = 0;
                for (int j = 0; j < permutationCycle[i].Length; j++)
                    if (permutationCycle[i][j] == min)
                        indexOfMin = j;
                int index = 0;
                cyclesInNewOrder[i] = new int[permutationCycle[i].Length];
                for (int j = indexOfMin; j < permutationCycle[i].Length; j++)
                {
                    cyclesInNewOrder[i][index] = permutationCycle[i][j];
                    index++;
                }
                for (int j = 0; j < indexOfMin; j++)
                {
                    cyclesInNewOrder[i][index] = permutationCycle[i][j];
                    index++;
                }
                for (int k = 0; k < cyclesInNewOrder[i].Length - 1; k++)
                {
                    int first = 0, second = 0;
                    for (int j = 0; j < identity.Length; j++)
                        if (identity[j] == cyclesInNewOrder[i][k])
                            first = j;
                    for (int j = 0; j < identity.Length; j++)
                        if (identity[j] == cyclesInNewOrder[i][k + 1])
                            second = j;

                    transpositions[indexOfTransposition] = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
                    int temp = transpositions[indexOfTransposition][first];
                    transpositions[indexOfTransposition][first] = transpositions[indexOfTransposition][second];
                    transpositions[indexOfTransposition][second] = temp;
                    indexOfTransposition++;
                }

            }
            return transpositions;
        }
        static public int[][] PermutationToNeighbouringTransposition1(int[] permutation) //najmniejsza w lewo
        {
            Check(permutation);
            if (isIdentity(permutation))
                throw new Exception("Permutation is identity. There aren't any transposistions.");
            List<int[]> list = new List<int[]>();
            bool[] tab = new bool[permutation.Length];
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] == i + 1)
                    tab[i] = true;
            //for(int i)
            for (int i = 0; i < permutation.Length; i++)
            {
                while (permutation[i] != (i + 1))
                {
                    for (int j = 0; j < permutation.Length; j++)
                    {
                        if (permutation[j] == i + 1)
                        {
                            int temp = permutation[j];
                            permutation[j] = permutation[j - 1];
                            permutation[j - 1] = temp;
                            list.Add(MakeTransposition(j + 1, j, permutation.Length));
                        }
                    }
                }

            }
            list.Reverse();
            return list.ToArray();
        }

        static public int[][] PermutationToNeighbouringTransposition2(int[] permutation) //najmniejsza w lewo
        {
            Check(permutation);
            if (isIdentity(permutation))
                throw new Exception("Permutation is identity. There aren't any transposistions.");
            List<int[]> list = new List<int[]>();
            bool[] tab = new bool[permutation.Length];
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] == i + 1)
                    tab[i] = true;
            //for(int i)
            for (int i = permutation.Length - 1; i >= 0; i--)
            {
                while (permutation[i] != (i + 1))
                {
                    for (int j = permutation.Length - 1; j >= 0; j--)
                    {
                        if (permutation[j] == i + 1)
                        {
                            int temp = permutation[j];
                            permutation[j] = permutation[j + 1];
                            permutation[j + 1] = temp;
                            list.Add(MakeTransposition(j + 2, j + 1, permutation.Length));
                        }
                    }
                }

            }
            list.Reverse();
            return list.ToArray();
        }

        static int[] MakeTransposition(int a, int b, int length)
        {
            int[] permutation = WithoutRepetition.CreateIdentityPermutation(length);
            permutation[a - 1] = b;
            permutation[b - 1] = a;
            return permutation;
        }

        static public int[][] ListOfInversions(int[] permutation)
        {
            int numberOfInversion = InversionsCount(permutation);
            int[][] listOfInversion = new int[numberOfInversion][];
            int index = 0;
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i + 1; j < permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i])
                    {
                        listOfInversion[index] = new int[2];
                        listOfInversion[index][0] = permutation[i];
                        listOfInversion[index][1] = permutation[j];
                        index++;
                    }
                }
            }
            return listOfInversion;
        }

        static bool isIdentity(int[] permutation)
        {
            int[] identity = CreateIdentityPermutation(permutation.Length);
            if (ArrayFunctions.CompareIntArrays(identity, permutation))
                return true;
            return false;
        }

    }
}

