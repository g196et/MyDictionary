using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    class MyDictionary2
    {
        string[] keys;
        string[] values;
        AhoCorasickTree aho;

        public MyDictionary2()
        {
            keys = new string[1];
            values = new string[1];
        }

        public void Print()
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Console.WriteLine(keys[i] + " - " + values[i]);
            }
        }

        public bool Find(string key)
        {
            if (aho.Contains(key))
                return true;
            else return false;
        }

        public bool Insert(string key, string value)
        {
            if (aho == null)
            {
                string[] tmp = {key};
                aho = new AhoCorasickTree(tmp);
                Array.Resize(ref keys, keys.Length + 1);
                keys[keys.Length - 1] = key;
                Array.Resize(ref values, values.Length + 1);
                values[values.Length - 1] = value;
                return true;
            }
            if (!aho.Contains(key))
            {
                Array.Resize(ref keys, keys.Length + 1);
                keys[keys.Length - 1] = key;
                Array.Resize(ref values, values.Length + 1);
                values[values.Length - 1] = value;
                aho.AddPatternToTree(key);
                return true;
            }
            else return false;
        }

        public void Remove(string key)
        {
            if (aho == null)
            {
                //return false;
            }
            if (aho.Contains(key))
            {
                //удалить
            }
        }
    }
}
