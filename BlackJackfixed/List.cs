using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackfixed
{
     public class List
    {
        private int LastIndex { get; set; }
        private int Count { get; }
        private object[] _array;
        private int _capacity = 5;

        public List(int capacity)
        {
            _capacity = capacity;
            InitArray();
        }
        public List()
        {
            InitArray();
        }




        private void InitArray()
        {

            _array = new object[_capacity];

        }
        private void RenitArray()
        {
            object[] newArray = _array;
            _array = new object[_capacity];
            if (_array != null)
            {
                for (int i = 0; i < newArray.Length; i++)
                {
                    _array[i] = newArray[i];
                }
            }
        }

        public void Add(int newValue)
        {
            if (LastIndex == _capacity)
            {
                _capacity *= 2;
                RenitArray();
            }
            _array[LastIndex] = newValue;
            LastIndex++;
        }

        public void Insert(int index, object obj)
        {
            if (index < _array.Length)
            {
                if (LastIndex == index)
                {
                    _array[index] = obj;
                    LastIndex++;
                }
                else
                {
                    Console.WriteLine("Bad input");
                }
            }
            else if (index == _capacity && index == LastIndex)
            {
                _capacity *= 2;
                RenitArray();
                _array[index] = obj;
                LastIndex++;
            }
            else
            {
                Console.WriteLine("Insert false");
            }
        }

        public void Remove(object value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == value)
                {
                    _array[i] = null;
                }
            }
        }
        public void RemoveAt(int index)
        {
            if (index < _array.Length)
            {
                _array[index] = null;
            }

        }

        public void Clear()
        {
            _array = new object[0];
        }

        public bool Contains(object obj)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == obj) return true;
            }
            return false;
        }

        public int IndexOf(object obj)
        {
            int index;
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == obj)
                {
                    index = (int)_array[i];
                    return index;
                }

            }
            return -1;
        }
        public void Reverse()
        {
            for (int i = 0, j = _array.Length - 1; i < j; i++, j--)
            {
                object temp = _array[i];
                _array[i] = _array[j];
                _array[j] = temp;
            }
        }


        public object[] ToArray()
        {
            var newArr = new object[Count];
            for (int i = 0; i < _array.Length; i++)
            {
                newArr[i] = _array[i];
            }
            return newArr;
        }

    }
}
