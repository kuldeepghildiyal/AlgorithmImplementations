using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsImplementation
{
    //https://en.wikipedia.org/wiki/Open_addressing
    public enum ProbingType { Linear, Quadratic, Double };
    class ClosedHashTableManager
    {

        class HashTable
        {
            private int key;
            private string value;
            public HashTable(int key, string value)
            {
                this.key = key;
                this.value = value;
            }
            public int GetKey => key;
            public string GetValue => value;
        }
        readonly int maxSize; //our table size
        HashTable[] list;
        private readonly int probingValue;

        public ClosedHashTableManager()
        {
            this.maxSize = 1000;
            list = new HashTable[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                list[i] = null;
            }
            probingValue = (int)ProbingType.Linear;
        }
        public ClosedHashTableManager(int maxSize)
        {
            this.maxSize = maxSize;
            list = new HashTable[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                list[i] = null;
            }
            probingValue = (int)ProbingType.Linear;
        }
        public ClosedHashTableManager(ProbingType probing)
        {
            this.maxSize = 1000;
            list = new HashTable[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                list[i] = null;
            }
            probingValue = (int)probing;
        }
        public ClosedHashTableManager(int maxSize, ProbingType probing)
        {
            this.maxSize = maxSize;
            list = new HashTable[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                list[i] = null;
            }
            probingValue = (int)probing;
        }
        public string Get(int key)
        {
            int? hashValue = GetHashKey(key);
            if (hashValue == null)
                return "Not found!";
            else
            {
                if (list[hashValue.Value] == null)
                    return "Not found!";
                else
                    return list[hashValue.Value].GetValue;
            }

        }
        public void Add(int key, string data)
        {
            int? hashValue = GetHashKey(key);
            if (hashValue == null)//if space is not available
                Console.WriteLine("No space available!");
            else
                list[hashValue.Value] = new HashTable(key, data);
        }

        public bool Remove(int key)
        {
            int? hashValue = GetHashKey(key);
            if (hashValue == null)
                return false;
            else
            {
                if (list[hashValue.Value] == null)
                    return false;
                else
                {
                    list[hashValue.Value] = null;
                    return true;
                }
            }
        }
        private bool ClaimSpace()//Checks for valid spaces in the table/list for addition
        {
            bool isValid = false;
            for (int i = 0; i < maxSize; i++)
            {
                if (list[i] == null)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        /// <summary>
        /// Get hask kay according to user selection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int? GetHashKey(int key)
        {
            int? hashValue = null;
            switch (probingValue)
            {
                case 0:
                    hashValue = LinearHashInsert(key);
                    break;
                case 1:
                    hashValue = QuadraticHashInsert(key);
                    break;
                case 2:
                    hashValue = DoubleHashInsert(key);
                    break;
                default:
                    hashValue = LinearHashInsert(key);
                    break;
            }
            return hashValue;
        }
        /// <summary>
        /// The interval between probes is fixed — often set to 1.
        /// </summary>
        /// <param name="key"></param>
        private int? LinearHashInsert(int key)
        {
            if (!ClaimSpace())//if space is not available
                return null;
            int hashValue = (key % maxSize);
            while (list[hashValue] != null && list[hashValue].GetKey != key)
            {
                hashValue = (hashValue + 1) % maxSize;
            }
            return hashValue;
        }
        /// <summary>
        /// The interval between probes increases quadratically (hence, the indices are described by a quadratic function).
        /// </summary>
        /// <param name="key"></param>
        public int? QuadraticHashInsert(int key)
        {
            if (!ClaimSpace())//if space is not available
                return null;

            int i = 0;
            int hashValue = key % maxSize;
            while (list[hashValue] != null && list[hashValue].GetKey != key)
            {
                i++;
                hashValue = (hashValue + i * i) % maxSize;
            }
            return hashValue;
        }
        /// <summary>
        /// The interval between probes is fixed for each record but is computed by another hash function.
        /// </summary>
        /// <param name="key"></param>
        public int? DoubleHashInsert(int key)
        {
            if (!ClaimSpace())//if space is not available
                return null;

            int hashVal = hash1(key);
            int stepSize = hash2(key);

            while (list[hashVal] != null && list[hashVal].GetKey != key)
            {
                hashVal = (hashVal + stepSize * hash2(key)) % maxSize;
            }

            return hashVal;
        }
        private int hash1(int key)
        {
            return key % maxSize;
        }
        private int hash2(int key)
        {
            //must be non-zero, less than array size, ideally odd
            return 5 - key % 5;
        }
    }

    class OpenHashTableManager
    {
        class Node
        {
            int key;
            string value;
            Node next;
            public Node(int key, string value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }
            public int GetKey => key;
            public string GetValue => value;
            public void SetNextNode(Node obj)
            {
                next = obj;
            }
            public Node GetNextNode()
            {
                return this.next;
            }
        }

        Node[] list;
        readonly int size = 10;

        public OpenHashTableManager(int maxSize)
        {
            list = new Node[size];
            for (int i = 0; i < size; i++)
            {
                list[i] = null;
            }
            this.size = maxSize;
        }
        public void Add(int key, string data)
        {
            Node nObj = new Node(key, data);
            int hash = key % size;
            while (list[hash] != null && list[hash].GetKey % size != key % size)
            {
                hash = (hash + 1) % size;
            }
            if (list[hash] != null && hash == list[hash].GetKey % size)
            {
                nObj.SetNextNode(list[hash].GetNextNode());
                list[hash].SetNextNode(nObj);
                return;
            }
            else
            {
                list[hash] = nObj;
                return;
            }
        }
        public string Get(int key)
        {
            int hash = key % size;
            while (list[hash] != null && list[hash].GetKey % size != key % size)
            {
                hash = (hash + 1) % size;
            }
            Node current = list[hash];
            while (current.GetKey != key && current.GetNextNode() != null)
            {
                current = current.GetNextNode();
            }
            if (current.GetKey == key)
            {
                return current.GetValue;
            }
            else
            {
                return "Not found!";
            }
        }
        public void Remove(int key)
        {
            int hash = key % size;
            while (list[hash] != null && list[hash].GetKey % size != key % size)
            {
                hash = (hash + 1) % size;
            }
            //a current node pointer used for traversal, currently points to the head
            Node current = list[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.GetKey == key)
                {
                    list[hash] = current.GetNextNode();
                    Console.WriteLine("Data removed successfully!");
                    isRemoved = true;
                    break;
                }

                if (current.GetNextNode() != null)
                {
                    if (current.GetNextNode().GetKey == key)
                    {
                        Node newNext = current.GetNextNode().GetNextNode();
                        current.SetNextNode(newNext);
                        Console.WriteLine("Data removed successfully!");
                        isRemoved = true;
                        break;
                    }
                    else
                    {
                        current = current.GetNextNode();
                    }
                }

            }

            if (!isRemoved)
            {
                Console.WriteLine("Not found to delete!");
                return;
            }
        }


    }



}

