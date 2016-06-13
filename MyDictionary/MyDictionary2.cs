using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    class MyDictionary2
    {
        AhoCorasickTree aho;

        /// <summary>
        /// Бонусная функция - вывода всех элементов
        /// </summary>
        public void Print()
        {
            aho.Print();
        }

        /// <summary>
        /// Поиск элемента по ключу
        /// </summary>
        /// <param name="key">Ключдля поиска</param>
        /// <returns>Возвращается значение по ключу</returns>
        public string Find(string key)
        {
            string temp;
            if ((temp = aho.Contains(key)) != null)
            {
                return temp;
            }
            else return "false";
        }

        /// <summary>
        /// Вставка элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <returns>true - если всё успешно, иначе false</returns>
        public bool Insert(string key, string value)
        {
            if (aho == null)
            {
                string tmp = key;
                aho = new AhoCorasickTree(tmp, value);
                return true;
            }
            if (aho.Contains(key) == null)
            {
                aho.AddPatternToTree(key, value);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Удаление по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>true - если всё спешно, иначе false</returns>
        public bool Remove(string key)
        {
            string temp;
            if (aho == null)
            {
                return false;
            }
            if ((temp = aho.Contains(key)) != null)
            {
                aho.DelPatternToTree(key);
                return true;
            }
            return false;
        }
    }
}
