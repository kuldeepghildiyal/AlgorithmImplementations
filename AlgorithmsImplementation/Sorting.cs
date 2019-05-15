using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsImplementation
{
    public class Sorting
    {

        public int[] BubbleSort(int[] data)
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < data.Length - 1; i++)
                {
                    if (data[i] > data[i + 1])
                    {
                        var temp = data[i];
                        data[i] = data[i + 1];
                        data[i + 1] = temp;
                        swapped = true;
                    }
                }
            }
            while (swapped == true);

            return data;
        }
        public int[] InsertionSort(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int temp = data[i];
                for (int j = i; j > 0; j--)
                {
                    if (temp < data[j - 1])
                    {
                        var intTemp = data[j - 1];
                        data[j - 1] = temp;
                        data[j] = intTemp;
                    }

                }

            }
            return data;
        }

        public int[] SelectionSort(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int smallest = int.MaxValue;
                int smallestIndex = 0;
                for (int j = i; j < data.Length; j++)
                {
                    if (smallest > data[j])
                    {
                        smallest = data[j];
                        smallestIndex = j;
                    }

                }
                data[smallestIndex] = data[i];
                data[i] = smallest;
            }



            return data;
        }
    }
}
