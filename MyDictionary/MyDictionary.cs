using System;
using System.Linq;

namespace MyDictionary {

    internal class MyDictionary {

        //Константы для хэширования
        private const int Size = 26;

        private const int RandomConst = 1568;

        private readonly string[] _hashList;

        public MyDictionary () {
            _hashList = new string[50000];
        }

        /// <summary>
        /// Метод хэширования
        /// </summary>
        /// <param name="key">ключ, который будет хэшироваться</param>
        /// <returns></returns>
        private static int GetHash (string key) {
            return key.Select((t, i) => (int)Math.Pow(Size, key.Length - 1 - i) * t).Sum();
        }

        /// <summary>
        /// Метод добавления нового элемента
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">значение</param>
        /// <returns>true - если удалось добавить, иначе false</returns>
        public bool Insert (string key, string value) {
            var hash = GetHash(key);
            //Если нашли с таким же хэшем, значит что-то не так
            if (Find(key))
                return false;
            _hashList[hash] = value;
            return true;
        }

        /// <summary>
        /// Метод поиска по ключу
        /// </summary>
        /// <param name="key">ключ, по которому производится поиск</param>
        /// <returns>true - если удалось найди, иначе false</returns>
        public bool Find (string key) {
            var hash = GetHash(key);
            return _hashList[hash] != null;
        }

        /// <summary>
        /// Метод удаления по ключу
        /// </summary>
        /// <param name="key">ключ, по которому производится удаление</param>
        /// <returns>true - если удалось удалить, иначе false</returns>
        public bool Remove (string key) {
            var hash = GetHash(key);
            if (Find(key) == false)
                return false;
            _hashList[hash] = null;
            return true;
        }

        public void Print () {
            var count = 0;
            foreach (var tmp in _hashList.Where(tmp => tmp != null)) {
                Console.Write(tmp + " ");
                count++;
            }
            Console.WriteLine();
            Console.WriteLine("COUNT = " + count);
        }
    }
}