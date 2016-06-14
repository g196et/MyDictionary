namespace MyDictionary
{
    internal class MyDictionary2
    {
        private AhoCorasickTree _aho;

        /// <summary>
        /// Бонусная функция - вывода всех элементов
        /// </summary>
        public void Print()
        {
            _aho.Print();
        }

        /// <summary>
        /// Поиск элемента по ключу
        /// </summary>
        /// <param name="key">Ключдля поиска</param>
        /// <returns>Возвращается значение по ключу</returns>
        public string Find(string key)
        {
            string temp;
            return (temp = _aho.Contains(key)) != null ? temp : "false";
        }

        /// <summary>
        /// Вставка элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <returns>true - если всё успешно, иначе false</returns>
        public bool Insert(string key, string value)
        {
            if (_aho == null)
            {
                var tmp = key;
                _aho = new AhoCorasickTree(tmp, value);
                return true;
            }
            if (_aho.Contains(key) != null) return false;
            _aho.AddPatternToTree(key, value);
            return true;
        }

        /// <summary>
        /// Удаление по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>true - если всё спешно, иначе false</returns>
        public bool Remove(string key)
        {
            if (_aho?.Contains(key) == null) return false;
            _aho.DelPatternToTree(key);
            return true;
        }
    }
}