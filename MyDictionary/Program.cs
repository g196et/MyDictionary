using System;
using System.IO;

namespace MyDictionary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tchar = new char[10];
            var myDictionary = new MyDictionary2();
            Console.Write("Insert — i, Find — f, Remove — r, Print — p , File — file, Exit — e\n> ");
            var enter = Console.ReadLine();
            while (enter != "e")
            {
                string key;
                string value;
                TimeSpan spentTime; // Объявляем переменную типа TimeSpan для хранения затраченного времени
                DateTime before;
                switch (enter)
                {
                    case "i":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        Console.Write("Value = ");
                        value = Console.ReadLine();
                        Console.WriteLine(!myDictionary.Insert(key, value) ? "Error" : "Item added");
                        break;

                    case "f":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        before = DateTime.Now; // Засекаем время до выполнения алгоритма
                        var temp = myDictionary.Find(key);
                        // ТУТ ОШИБКА ЧОМУ-ТО НЕ ВОЗВРАЩАЕТ FALSE ЕСЛИ НЕТУ ТАКОВО КЛЮЧА
                        spentTime = DateTime.Now - before; // Вычисляем время, затраченное на выполнение цикла
                        Console.WriteLine("{0} (found this shit in {1} ms)", temp, spentTime.TotalMilliseconds);
                        //чому 0
                        break;

                    case "r":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        before = DateTime.Now;
                        if (myDictionary.Remove(key))
                        {
                            spentTime = DateTime.Now - before;
                            Console.WriteLine("Item removed in {0} ms", spentTime.TotalMilliseconds);
                        }
                        else Console.WriteLine("Item not found");
                        break;

                    case "p":
                        myDictionary.Print();
                        break;

                    case "file":
                        Console.Write("filename = ");
                        var file = Console.ReadLine();
                        if (File.Exists(file))
                        {
                            if (file != null)
                                using (var reader = new StreamReader(file))
                                {
                                    before = DateTime.Now; // Засекаем время до выполнения алгоритма
                                    while ((key = reader.ReadLine()) != null)
                                    {
                                        var split = key.Split('|');
                                        key = split[0];
                                        value = split[1];
                                        myDictionary.Insert(key, value);
                                    }
                                    spentTime = DateTime.Now - before;
                                    // Вычисляем время, затраченное на выполнение цикла
                                    Console.WriteLine("added this shit in {0} ms", spentTime.TotalMilliseconds);
                                }
                        }
                        else
                        {
                            Console.WriteLine("no such file desu");
                        }
                        break;
                }
                Console.Write("> ");
                enter = Console.ReadLine();
            }
        }
    }
}