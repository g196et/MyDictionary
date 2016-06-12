using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    class MyDictionary
    {
        //Константы для хэширования
        const int size = 26;
        const int randomConst = 1568;

        string[] hashList;

        public MyDictionary()
        {
            hashList = new string[50000];
        }

        /// <summary>
        /// Метод хэширования
        /// </summary>
        /// <param name="key">ключ, который будет хэшироваться</param>
        /// <returns></returns>
        int GetHash(string key)
        {
            int hash = 0;
            for (int i = 0 ; i < key.Length ; i++)
            {
                hash += (int)Math.Pow(size, key.Length -1 - i) * (int)(key[i]);
            }
            return hash;
        }

        /// <summary>
        /// Метод добавления нового элемента
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">значение</param>
        /// <returns>true - если удалось добавить, иначе false</returns>
        public bool Insert(string key, string value)
        {
            int hash = GetHash(key);
            //Если нашли с таким же хэшем, значит что-то не так
            if (Find(key) == true)
                return false;
            hashList[hash] = value;
            return true;
        }

        /// <summary>
        /// Метод поиска по ключу
        /// </summary>
        /// <param name="key">ключ, по которому производится поиск</param>
        /// <returns>true - если удалось найди, иначе false</returns>
        public bool Find(string key) 
        {
            int hash = GetHash(key);
            if (hashList[hash] == null)
                return false;
            else return true;
        }

        /// <summary>
        /// Метод удаления по ключу
        /// </summary>
        /// <param name="key">ключ, по которому производится удаление</param>
        /// <returns>true - если удалось удалить, иначе false</returns>
        public bool Remove(string key)
        {
            int hash = GetHash(key);
            if (Find(key) == false)
                return false;
            hashList[hash] = null;
            return true;
        }

        public void Print()
        {
            int count = 0;
            foreach (string tmp in hashList)
                if (tmp != null)
                {
                    Console.Write(tmp + " ");
                    count++;
                }
            Console.WriteLine();
            Console.WriteLine("COUNT = " + count);
        }

    }
}
