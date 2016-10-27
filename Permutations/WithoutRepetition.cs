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

        static public int[] FoldingPermutations(int[] permutation1, int[] permutation2)
        {
            if (!CheckTwoPermutations(permutation1, permutation2)) throw new Exception("Bad format of permutations");
            int[] returnedArray = new int[permutation1.Length];
            for (int i = 0; i < permutation1.Length; i++)
            {
                returnedArray[i] = permutation1[permutation2[i] - 1];
            }
            return returnedArray;
        }

        static public int[][] VectorToCycle(int[] permutation)
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

            int[][] returnedPermutation = new int[number][];

            for (int i = 0; i < number; i++)
            {
                returnedPermutation[i] = collection[i].list.ToArray();
            }

            return returnedPermutation;
        }

        static public int OrderOfPermutation(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = VectorToCycle(permutation);
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
            int[][] permutationCycle = WithoutRepetition.VectorToCycle(permutation);
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

        static public bool IsDisorder(int[] permutation)
        {
            Check(permutation);
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] == i + 1) return false;
            return true;
        }

        static int CountDisorders(int[] permutation)
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
        }

        static public bool IsEven(int[] permutation)
        {
            Check(permutation);
            int disorders = CountDisorders(permutation);
            if (disorders % 2 == 0) return true;
            return false;
        }

        static public bool IsNonEven(int[] permutation)
        {
            Check(permutation);
            if (IsEven(permutation)) return false;
            return true;
        }

        static public int SignOfPermutation(int[] permutation)
        {
            Check(permutation);
            if (IsEven(permutation)) return 1;
            return -1;
        }

        static public bool IsTransposition(int[] permutation)
        {
            Check(permutation);
            bool flag = false;
            int[][] permutationCycle = WithoutRepetition.VectorToCycle(permutation);
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
                returnedPermutation = FoldingPermutations(returnedPermutation, permutation);

            return returnedPermutation;
        }

        static public bool IsOneCycle(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycle = VectorToCycle(permutation);
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

        static public int CountCycles(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = VectorToCycle(permutation);
            return permutationCycles.Length;
        }

        static public int CountEvenCycles(int[] permutation)
        {
            Check(permutation);
            int[][] permutationCycles = WithoutRepetition.VectorToCycle(permutation);
            int count = 0;
            for (int i = 0; i < permutationCycles.Length; i++)
                if (permutationCycles[i].Length % 2 == 0) count++;
            return count;
        }

        static public int[,] VectorToMatrix(int[] permutation)
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

        static public int[] CycleToVector(int[][] cyclePermutation)
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

        static public int CountInversions(int[] permutation)
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

        static public int[,] MatrixOfInversions(int[] permutation)
        {
            int numberOfInversion = CountInversions(permutation);
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

        static public long NumberOfPermutation(int[] permutation)
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

            return returnedValue + 1;
        }

        static public int[] VectorByNumber(int number, int n) //wektor inwersji z numeru i liczebności zbioru
        {
            if ((number > MathFunctions.Factorial(n)) || number < 1 || n < 2) throw new Exception("Bad value of typed data.");

            int[] vectorOfInversion = new int[n];
            int factorial;

            number--;
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

        static public int[] PermutationByNumber(int number, int n)
        {
            int[] vectorOfInversions = VectorByNumber(number, n);
            int[] permutation = VectorToPermutation(vectorOfInversions);

            return permutation;
        }

        static public int[][] PermutationLexicographical(int n)
        {
            if (n < 2) throw new Exception("Bad number of length.");

            int count = MathFunctions.Factorial(n);
            int[][] permutations = new int[count][];
            permutations[0] = WithoutRepetition.CreateIdentityPermutation(n);

            for (int i = 1; i < count; i++)
                permutations[i] = NextElementLexicographical(permutations[i - 1]);
                          
            return permutations;
        }

        static public int[] NextElementLexicographical(int[] previousPermutation)
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

        static public int[][] GeneratePermutationsOfType(int[] type)
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
                returnedArray[indexOfArray] = PermutationByNumber(i + 1, type.Length - 1);
                indexOfArray++;
            }

            return returnedArray;
        }

        static public int[][] GenerateWithOrder(int order, int length)
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
                returnedArray[indexOfArray] = WithoutRepetition.PermutationByNumber(i + 1, length);
                indexOfArray++;
            }
            return returnedArray;
        }

        public static int[] RootOfPermutation(int[] permutation, int power)
        {
            Check(permutation);
            int[] testedPermutation = WithoutRepetition.CreateIdentityPermutation(permutation.Length);
            if (ArrayFunctions.CompareIntArrays(permutation, WithoutRepetition.PowerOfPermutation(testedPermutation, power)))
                return testedPermutation;
            for (int i = 1; i < MathFunctions.Factorial(permutation.Length); i++)
            {
                testedPermutation = WithoutRepetition.NextElementLexicographical(testedPermutation);
                if (ArrayFunctions.CompareIntArrays(permutation, WithoutRepetition.PowerOfPermutation(testedPermutation, power)))
                    return testedPermutation;
            }
            return testedPermutation;
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

        static public int[][] PermutationAntylexicographical(int n)
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
    }
}
