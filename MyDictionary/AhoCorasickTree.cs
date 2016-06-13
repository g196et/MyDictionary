using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    public class AhoCorasickTree
    {
        private readonly AhoCorasickTreeNode _rootNode;

        public AhoCorasickTree() { }

        public AhoCorasickTree(string keyword, string value)
        {
            //Обрабатываем ошибки
            if (keyword == null) throw new ArgumentNullException("keywords");
            if (keyword.Length == 0) throw new ArgumentException("should contain keywords");

            //Устанавливаем корень
            _rootNode = new AhoCorasickTreeNode();
            _rootNode.Failure = _rootNode;

            //Добавляем первый паттерн
            AddPatternToTree(keyword, value);

            //SetFailures();
        }

        /// <summary>
        /// Содержание слова в дереве
        /// </summary>
        /// <param name="text">Првоеряемое слово</param>
        /// <returns>Значение для словаря</returns>
        public string Contains(string text)
        {
            var currentNode = _rootNode;

            var length = text.Length;
            //Идём по всем символам слова
            for (var i = 0; i < length; i++)
            {
                while (true)
                {
                    //Проверяем, есть ли следующий символ в потомках узла
                    var node = currentNode.GetNode(text[i]);
                    if (node == null)
                    {
                        currentNode = currentNode.Failure;
                        return null;
                    }
                    else
                    {
                        if (i == length - 1)
                        {
                            return node.Results;
                        }
                        currentNode = node;
                        break;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Метод добавления слова в дерево
        /// </summary>
        /// <param name="pattern">Добавляемое слово</param>
        /// <param name="value">Его значение для словаря</param>
        public void AddPatternToTree(string pattern, string value)
        {
            var latestNode = _rootNode;
            var tempNode = _rootNode;
            var length = pattern.Length;
            //Проверяем все символы из слова
            for (var i = 0; i < length; i++)
            {
                if ((tempNode = latestNode.GetNode(pattern[i])) != null)
                {
                    latestNode = tempNode;
                }
                else
                {
                    //Если такого имвола ещё нет в потомках, добавляем его
                    latestNode = latestNode.AddNode(pattern[i]);
                    var failure = latestNode.Parent.Failure;
                    var key = latestNode.Key;
                    while (failure.GetNode(key) == null && failure != _rootNode)
                    {
                        failure = failure.Failure;
                    }

                    failure = failure.GetNode(key);
                    if (failure == null || failure == latestNode)
                    {
                        failure = _rootNode;
                    }

                    latestNode.Failure = failure;
                    if (!latestNode.IsFinished)
                    {
                        latestNode.IsFinished = failure.IsFinished;
                    }
                }
            }
            //Заполняем последний узел
            latestNode.IsFinished = true;
            latestNode.Results = pattern;
            latestNode.value = value;
        }

        /// <summary>
        /// Метод удаления слова из дерева
        /// </summary>
        /// <param name="pattern">Удаляемое слово</param>
        public void DelPatternToTree(string pattern)
        {
            var latestNode = _rootNode;
            var length = pattern.Length;
            //Ищем нужный узел
            for (var i = 0; i < length; i++)
            {
                latestNode = latestNode.GetNode(pattern[i]);
            }
            //Обнуляем его значения
            latestNode.Results = null;
            latestNode.value = null;
            latestNode.IsFinished = false;
        }

        public void Print()
        {
            var queue = new Queue<AhoCorasickTreeNode>();
            queue.Enqueue(_rootNode);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var node in currentNode.Nodes)
                {
                    queue.Enqueue(node);
                }
                if ((currentNode == _rootNode) || (currentNode.Results == null)) continue;
                Console.WriteLine(currentNode.Results + " - " + currentNode.value);
            }
        }

        private class AhoCorasickTreeNode
        {
            public readonly AhoCorasickTreeNode Parent;
            public AhoCorasickTreeNode Failure;
            //Является ли конечным
            public bool IsFinished;
            //Что хранит в себе
            public string Results;
            public readonly char Key;
            public string value;

            private int[] _buckets;
            private int _count;
            private Entry[] _entries;

            internal AhoCorasickTreeNode()
                : this(null, ' ')
            {
            }

            private AhoCorasickTreeNode(AhoCorasickTreeNode parent, char key)
            {
                Key = key;
                Parent = parent;

                _buckets = new int[0];
                _entries = new Entry[0];
                Results = null;
            }

            public AhoCorasickTreeNode[] Nodes
            {
                get { return _entries.Select(x => x.Value).ToArray(); }
            }

            public AhoCorasickTreeNode AddNode(char key)
            {
                var node = new AhoCorasickTreeNode(this, key);

                var newSize = _count + 1;
                Resize(newSize);

                var targetBucket = key % newSize;
                _entries[_count].Key = key;
                _entries[_count].Value = node;
                _entries[_count].Next = _buckets[targetBucket];
                _buckets[targetBucket] = _count;
                _count++;

                return node;
            }

            public AhoCorasickTreeNode DelNode()
            {
                var node = this.Parent;
                //переделать
                var newSize = _count - 1;
                Resize(newSize);

                _count--;

                return node;
            }

            public AhoCorasickTreeNode GetNode(char key)
            {
                if (_count == 0) return null;

                var bucketIndex = key % _count;
                for (var i = _buckets[bucketIndex]; i >= 0; i = _entries[i].Next)
                {
                    if (_entries[i].Key == key)
                    {
                        return _entries[i].Value;
                    }
                }

                return null;
            }

            private void Resize(int newSize)
            {
                var newBuckets = new int[newSize];
                for (var i = 0; i < newSize; i++)
                {
                    newBuckets[i] = -1;
                }

                var newEntries = new Entry[newSize];
                Array.Copy(_entries, 0, newEntries, 0, _entries.Length);

                // rebalancing buckets for existing entries
                for (var i = 0; i < _entries.Length; i++)
                {
                    var bucket = newEntries[i].Key % newSize;
                    newEntries[i].Next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }

                _buckets = newBuckets;
                _entries = newEntries;
            }

            private struct Entry
            {
                public char Key;
                public int Next;
                public AhoCorasickTreeNode Value;
            }
        }
    }
}