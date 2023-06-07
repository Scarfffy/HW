using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class LinkedList
    {
        private Node first;
        private Node last;
        private int count;

        public LinkedList()
        {
            first = null;
            last = null;
            count = 0;
        }

        public void Add(object item)
        {
            Node newNode = new Node(item);

            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                last.Next = newNode;
                last = newNode;
            }

            count++;
        }

        public void AddFirst(object item)
        {
            Node newNode = new Node(item);

            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                newNode.Next = first;
                first = newNode;
            }

            count++;
        }

        public void Insert(int index, object item)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                AddFirst(item);
            }
            else if (index == count)
            {
                Add(item);
            }
            else
            {
                Node newNode = new Node(item);
                Node previousNode = GetNode(index - 1);

                newNode.Next = previousNode.Next;
                previousNode.Next = newNode;

                count++;
            }
        }

        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
        }

        public bool Contains(object item)
        {
            Node currentNode = first;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public object[] ToArray()
        {
            object[] array = new object[count];
            Node currentNode = first;

            for (int i = 0; i < count; i++)
            {
                array[i] = currentNode.Value;
                currentNode = currentNode.Next;
            }

            return array;
        }

        public int Count
        {
            get { return count; }
        }

        public object First
        {
            get
            {
                if (first == null)
                {
                    throw new InvalidOperationException("The list is empty.");
                }

                return first.Value;
            }
        }

        public object Last
        {
            get
            {
                if (last == null)
                {
                    throw new InvalidOperationException("The list is empty.");
                }

                return last.Value;
            }
        }

        private Node GetNode(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            Node currentNode = first;

            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }
    }

    public class Node
    {
        public object Value { get; set; }
        public Node Next { get; set; }

        public Node(object value)
        {
            Value = value;
            Next = null;
        }
    }

}
