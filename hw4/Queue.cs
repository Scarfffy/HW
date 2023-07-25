using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class Queue
    {
        private object[] items;
        private int front;
        private int rear;
        private int count;

        public Queue()
        {
            items = new object[4];
            front = 0;
            rear = -1;
            count = 0;
        }

        public void Enqueue(object item)
        {
            if (count == items.Length)
            {
                ResizeArray();
            }

            rear = (rear + 1) % items.Length;
            items[rear] = item;
            count++;
        }

        public object Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            object item = items[front];
            items[front] = null;
            front = (front + 1) % items.Length;
            count--;

            return item;
        }

        public void Clear()
        {
            items = new object[4];
            front = 0;
            rear = -1;
            count = 0;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % items.Length;
                if (items[index] != null && items[index].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public object Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return items[front];
        }

        public object[] ToArray()
        {
            object[] array = new object[count];

            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % items.Length;
                array[i] = items[index];
            }

            return array;
        }

        public int Count
        {
            get { return count; }
        }

        private void ResizeArray()
        {
            object[] newArray = new object[items.Length * 2];
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % items.Length;
                newArray[i] = items[index];
            }
            items = newArray;
            front = 0;
            rear = count - 1;
        }
    }

}
