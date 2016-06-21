using System;
using System.Diagnostics;
using System.IO;

namespace MyDictionary {

    internal class Program {

        private static void Main (string[] args) {
            var tchar = new char[10];
            var myDictionary = new MyDictionary2();
            Console.Write("Insert — i, Find — f, Remove — r, Print — p , File — file, Exit — e\n> ");
            var enter = Console.ReadLine();
            while (enter != "e") {
                string key;
                string value;
                long ts;
                TimeSpan spentTime; // Объявляем переменную типа TimeSpan для хранения затраченного времени
                Stopwatch stopWatch = new Stopwatch();
                DateTime before;
                switch (enter) {
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
                        try {
                            stopWatch.Start();
                            var temp = myDictionary.Find(key);
                            stopWatch.Stop();
                            ts = stopWatch.ElapsedTicks;

                            Console.WriteLine("{0} (found this key in {1} ms)", temp, ts);
                        } catch (Exception) { Console.WriteLine("key not found"); }
                        break;

                    case "r":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        stopWatch.Start();
                        if (myDictionary.Remove(key)) {
                            stopWatch.Stop();
                            ts = stopWatch.ElapsedTicks;
                            Console.WriteLine("Item removed in {0} ms", ts);
                        } else Console.WriteLine("Item not found");
                        break;

                    case "p":
                        myDictionary.Print();
                        break;

                    case "file":
                        Console.Write("filename = ");
                        var file = Console.ReadLine();
                        if (File.Exists(file)) {
                            if (file != null)
                                using (var reader = new StreamReader(file)) {
                                    before = DateTime.Now; // Засекаем время до выполнения алгоритма
                                    while ((key = reader.ReadLine()) != null) {
                                        var split = key.Split('|');
                                        key = split[0];
                                        value = split[1];
                                        myDictionary.Insert(key, value);
                                    }
                                    spentTime = DateTime.Now - before;
                                    // Вычисляем время, затраченное на выполнение цикла
                                    Console.WriteLine("added this key in {0} ms", spentTime.TotalMilliseconds);
                                }
                        } else {
                            Console.WriteLine("file not found desu");
                        }
                        break;
                }
                Console.Write("> ");
                enter = Console.ReadLine();
            }
        }
    }
}