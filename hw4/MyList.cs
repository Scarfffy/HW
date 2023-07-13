using hw4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class MyList
    {
        private object[] items;
        private int count;

        public MyList()
        {
            items = new object[4];
            count = 0;
        }

        public MyList(int capacity)
        {
            items = new object[capacity];
            count = 0;
        }

        public void Add(object item)
        {
            if (count == items.Length)
            {
                ResizeItemsArray();
            }

            items[count] = item;
            count++;
        }

        public void Insert(int index, object item)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException();
            }

            if (count == items.Length)
            {
                ResizeItemsArray();
            }

            for (int i = count - 1; i >= index; i--)
            {
                items[i + 1] = items[i];
            }

            items[index] = item;
            count++;
        }

        public void Remove(object item)
        {
            int index = IndexOf(item);

            if (index != -1)
            {
                RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            items[count - 1] = null;
            count--;
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                items[i] = null;
            }

            count = 0;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(object item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public object[] ToArray()
        {
            object[] array = new object[count];
            Array.Copy(items, array, count);
            return array;
        }

        public void Reverse()
        {
            int left = 0;
            int right = count - 1;

            while (left < right)
            {
                object temp = items[left];
                items[left] = items[right];
                items[right] = temp;

                left++;
                right--;
            }
        }

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                items[index] = value;
            }
        }

        public int Count
        {
            get { return count; }
        }

        private void ResizeItemsArray()
        {
            int newCapacity = items.Length * 2;
            object[] newItems = new object[newCapacity];
            Array.Copy(items, newItems, count);
            items = newItems;
        }
    }
}