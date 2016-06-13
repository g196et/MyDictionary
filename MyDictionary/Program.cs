using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MyDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            string enter, key, value;
            char[] tchar = new char [10];
            MyDictionary2 myDictionary = new MyDictionary2();
            Console.Write("Insert, Find, Remove, Print, File, Exit\n> ");
            enter = Console.ReadLine();
            while (enter != "Exit")
            {     
                switch (enter)
                {
                    case ("i"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        Console.Write("Value = ");
                        value = Console.ReadLine();
                        if (!myDictionary.Insert(key, value))
                            Console.WriteLine("Error");
                        else Console.WriteLine("Item added");
                        break;
                    case ("f"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        string temp = myDictionary.Find(key);
                        Console.WriteLine(temp);
                        break;
                    case ("r"):
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        if (myDictionary.Remove(key))
                            Console.WriteLine("Item is remove");
                        else Console.WriteLine("Item not found");
                        break;
                    case ("p"):
                        myDictionary.Print();
                        break;
                    case ("file"):
                        using (StreamReader reader = new StreamReader("1.txt"))
                        {
                            while ((key = reader.ReadLine()) != null)
                            {
                                value = key;
                                myDictionary.Insert(key, value);
                            }
                        }
                        break;
                }
                Console.Write("> ");
                enter = Console.ReadLine();
            }
        }
    }
}
