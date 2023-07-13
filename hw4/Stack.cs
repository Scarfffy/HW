using System;
using hw4;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class Stack
    {
        private object[] items;
        private int top;

        public Stack()
        {
            items = new object[4];
            top = -1;
        }

        public void Push(object item)
        {
            if (top == items.Length - 1)
            {
                ResizeArray();
            }

            top++;
            items[top] = item;
        }

        public object Pop()
        {
            if (top == -1)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            object item = items[top];
            items[top] = null;
            top--;

            return item;
        }

        public void Clear()
        {
            items = new object[4];
            top = -1;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i <= top; i++)
            {
                if (items[i] != null && items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public object Peek()
        {
            if (top == -1)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return items[top];
        }

        public object[] ToArray()
        {
            object[] array = new object[top + 1];
            Array.Copy(items, array, top + 1);
            return array;
        }

        public int Count
        {
            get { return top + 1; }
        }

        private void ResizeArray()
        {
            object[] newArray = new object[items.Length * 2];
            Array.Copy(items, newArray, items.Length);
            items = newArray;
        }
    }

}
