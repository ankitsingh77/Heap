
namespace HeapDataStructure
{
    using System;
    using Heap;

    class Program
    {
        static void Main(string[] args)
        {
            var heap = new Heap.Heap(4, HeapType.MinHeap);
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Choose an Options");
                Console.WriteLine("1. Insert Element");
                Console.WriteLine("2. Extract Top");
                Console.WriteLine("3. Peek");
                Console.WriteLine("4. HeapSize");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. Exit");
                var option = Console.ReadLine();
                Console.WriteLine("");
                switch (option)
                {
                    case "1":
                    {
                        Console.Write("Enter Value : ");
                        var value = Console.ReadLine();
                        heap.Insert(Convert.ToInt32(value));
                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("Extracted Top : " + heap.ExtractTop());
                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine("Peek : " + heap.Peek);
                        break;
                    }
                    case "4":
                    {
                        Console.WriteLine("HeapSize : " + heap.HeapSize);
                        break;
                    }
                    case "5":
                    {
                        Console.Write("Heap Data : ");
                        foreach (var data in heap.HeapData)
                        {
                                Console.Write(data);
                                Console.Write(" ");
                        }
                        break;
                    }
                    case "6":
                    {
                        return;
                    }
                }
            }

        }
    }
}
