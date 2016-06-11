using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            string enter, key, value;
            MyDictionary myDictionary = new MyDictionary();
            Console.Write("Insert, Find, Remove, Print, Exit\n> ");
            enter = Console.ReadLine();
            while (enter != "Exit")
            {     
                switch (enter)
                {
                    case ("Insert"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        Console.Write("Value = ");
                        value = Console.ReadLine();
                        if (!myDictionary.Insert(key, value))
                            Console.WriteLine("Error");
                        else Console.WriteLine("Item added");
                        break;
                    case ("Find"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        if (myDictionary.Find(key))
                            Console.WriteLine("Item is exist");
                        else Console.WriteLine("Item not found");
                        break;
                    case ("Remove"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        if (myDictionary.Remove(key))
                            Console.WriteLine("Item is remove");
                        else Console.WriteLine("Item not found");
                        break;
                    case ("Print"):
                        myDictionary.Print();
                        break;
                }
                Console.Write("> ");
                enter = Console.ReadLine();
            }
        }
    }
}
