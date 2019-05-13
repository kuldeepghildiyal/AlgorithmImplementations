using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******************Block Address Start**************************");
            Console.WriteLine();
            string[] inputList = new string[] { "*book", "facebook.com", "google.com", "snapchat.com" };
            string[] wildCards = new string[] { "*", "#" };
            string[] hashedList = PrepareHashedList(inputList);
            Console.WriteLine("Is {0} is a blocked address : {1}", "fbook.edu", BlockAddress(hashedList, "fbook.edu", wildCards) ? "Yes" : "No");
            Console.WriteLine();
            Console.WriteLine("******************Block Address End**************************");

            Console.WriteLine();

            CustomHashtableExamples();
            Console.ReadLine();
        }

        /// <summary>
        /// Check address is blocked or not 
        /// Performance 
        /// Best case :  O(1)
        /// Wrost case O(M*M*K) where M is length of input variable and K is length of wilf cards.
        /// </summary>
        /// <param name="hashedList"></param>
        /// <param name="inputString"></param>
        /// <param name="wildCards"></param>
        /// <returns></returns>
        public static bool BlockAddress(string[] hashedList, string inputString, string[] wildCards)
        {
            string tempVar = "";
            bool matchFind = false;

            int hashValue = GetHashValue(inputString, hashedList, 197);
            if (!string.IsNullOrWhiteSpace(hashedList[hashValue]))
                matchFind = true;
            else
            {
                //Check for wild card search 
                if (wildCards.Length > 0)
                {
                    for (int i = 0; i < inputString.Length; i++)
                    {
                        tempVar = inputString[i].ToString();
                        for (int j = i + 1; j < inputString.Length; j++)
                        {
                            tempVar = tempVar + inputString[j];
                            for (int k = 0; k < wildCards.Length; k++)
                            {
                                int hashValueWithPattern = GetHashValue(wildCards[0] + tempVar, hashedList, 197);
                                if (!string.IsNullOrWhiteSpace(hashedList[hashValueWithPattern]))
                                {
                                    matchFind = true;
                                    break;
                                }
                            }
                            if (matchFind)
                                break;
                        }

                    }
                }
            }
            return matchFind;
        }

        /// <summary>
        /// Preparing a closed hash list, whose lenght is fixed to 1000
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static string[] PrepareHashedList(string[] inputList)
        {

            string[] hasCodeWithValues = new string[1000];
            for (int i = 0; i < inputList.Length; i++)
            {
                long hashValue = 0;
                string inputString = inputList[i];

                hashValue = GetHashValue(inputString, hasCodeWithValues, 197);

                hasCodeWithValues[hashValue] = inputString;
            }
            return hasCodeWithValues;
        }
        /// <summary>
        /// Has function to create a hash value  with use of prime number
        /// https://en.wikipedia.org/wiki/Open_addressing 
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="hashedArray"></param>
        /// <param name="primeNumber"></param>
        /// <returns></returns>

        static int GetHashValue(string inputString, string[] hashedArray, int primeNumber)
        {
            long hasValue = 0;
            for (int i = 0; i < inputString.Length; i++)
                hasValue += primeNumber * hasValue + (int)inputString[i];
            hasValue = hasValue % hashedArray.Length - 1;
            if (hasValue < 0)
                hasValue += hashedArray.Length - 1;
            return (int)hasValue;
        }
        public static void CustomHashtableExamples()
        {
            string[] inputList = new string[] { "*book", "facebook.com", "google.com", "snapchat.com" };
            string[] wildCards = new string[] { "*", "#" };
            Console.WriteLine("**************************Linear prob example********************************");
            ClosedHashTableManager closedHashTableManagerLinear = new ClosedHashTableManager(1000, ProbingType.Linear);
            // adding in the list
            for (int i = 0; i < inputList.Length; i++)
                closedHashTableManagerLinear.Add(i, inputList[i]);

            // Retrive from the hashed list
            for (int i = 0; i < inputList.Length; i++)
                Console.WriteLine("Retrive data : {0}", closedHashTableManagerLinear.Get(i));

            Console.Write("******************Quadratic prob example**************************");
            ClosedHashTableManager closedHashTableManagerQuadratic = new ClosedHashTableManager(1000, ProbingType.Quadratic);
            // adding in the list
            for (int i = 0; i < inputList.Length; i++)
                closedHashTableManagerQuadratic.Add(i, inputList[i]);

            // Retrive from the hashed list
            for (int i = 0; i < inputList.Length; i++)
                Console.WriteLine("Retrive data : {0}", closedHashTableManagerQuadratic.Get(i));



            Console.Write("********************Double prob example***************************");
            ClosedHashTableManager closedHashTableManagerDouble = new ClosedHashTableManager(1000, ProbingType.Double);
            // adding in the list
            for (int i = 0; i < inputList.Length; i++)
                closedHashTableManagerDouble.Add(i, inputList[i]);

            // Retrive from the hashed list
            for (int i = 0; i < inputList.Length; i++)
                Console.WriteLine("Retrive data : {0}", closedHashTableManagerDouble.Get(i));


            Console.WriteLine("Working on Open hastable solution");
            Console.ReadLine();
        }


    }
}
