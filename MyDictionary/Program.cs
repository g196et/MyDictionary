using System;
using System.Diagnostics;
using System.IO;

namespace MyDictionary {

    internal class Program {
        private static int i;

        private static void Main (string[] args) {
            var tchar = new char[10];
            var myDictionary = new MyDictionary();
            Console.Write("Insert — i, Find — f, Remove — r, Print — p , File — file, Exit — e\n> ");
            var enter = Console.ReadLine();
            while (enter != "e") {
                string key;
                string value;
                long ts;
                Stopwatch stopWatch = new Stopwatch();
                switch (enter) {
                    case "i":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        Console.Write("Value = ");
                        value = Console.ReadLine();
                        if (!myDictionary.Insert(key, value))
                            Console.WriteLine("this key exists already");
                        else {
                            stopWatch.Start();
                            myDictionary.Insert(key, value);
                            stopWatch.Stop();
                            ts = stopWatch.ElapsedTicks;
                            Console.WriteLine("successfully added this key in {0} ticks", ts);
                        }
                        break;

                    case "f":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        try {
                            if (myDictionary.Find(key) == "false")
                                Console.WriteLine("key not found");
                            else {
                                stopWatch.Start();
                                var temp = myDictionary.Find(key);
                                stopWatch.Stop();
                                ts = stopWatch.ElapsedTicks;
                                Console.WriteLine("{0} (found this key in {1} ticks)", temp, ts);
                            }
                        } catch (Exception) { Console.WriteLine("dictionary is empty"); }
                        break;

                    case "r":
                        Console.Write("Key = ");
                        key = Console.ReadLine();
                        stopWatch.Start();
                        if (myDictionary.Remove(key)) {
                            stopWatch.Stop();
                            ts = stopWatch.ElapsedTicks;
                            Console.WriteLine("Item removed in {0} ticks", ts);
                        } else Console.WriteLine("Item not found");
                        break;

                    case "p":
                        try {
                            myDictionary.Print();
                        } catch (Exception) { Console.WriteLine("dictionary is empty"); }
                        break;

                    case "file":
                        Console.Write("filename = ");
                        var file = Console.ReadLine();
                        try {
                            if (File.Exists(file)) {
                                if (file != null)
                                    using (var reader = new StreamReader(file)) {
                                        i = 0;
                                        stopWatch.Start();
                                        while ((key = reader.ReadLine()) != null) {
                                            var split = key.Split('|');
                                            key = split[0];
                                            value = split[1];
                                            myDictionary.Insert(key, value);
                                            i++;
                                        }
                                        stopWatch.Stop();
                                        ts = stopWatch.ElapsedMilliseconds;
                                        Console.WriteLine("loaded and added {0} keys from this file in {1} ms", i, ts);
                                    }
                            } else {
                                Console.WriteLine("file not found desu");
                            }
                        } catch (Exception) { Console.WriteLine("error"); }
                        break;
                }
                Console.Write("> ");
                enter = Console.ReadLine();
            }
        }
    }
}