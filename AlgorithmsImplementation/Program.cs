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

            Console.ReadLine();
        }

    }
}
