using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class DoublyLinkedList
    {
        private Node first;
        private Node last;
        private int count;

        public DoublyLinkedList()
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
                newNode.Previous = last;
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
                first.Previous = newNode;
                first = newNode;
            }

            count++;
        }

        public void Remove(object item)
        {
            Node currentNode = first;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    Node previousNode = currentNode.Previous;
                    Node nextNode = currentNode.Next;

                    if (previousNode != null)
                    {
                        previousNode.Next = nextNode;
                    }
                    else
                    {
                        first = nextNode;
                    }

                    if (nextNode != null)
                    {
                        nextNode.Previous = previousNode;
                    }
                    else
                    {
                        last = previousNode;
                    }

                    count--;
                    break;
                }

                currentNode = currentNode.Next;
            }
        }

        public void RemoveFirst()
        {
            if (first == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (first == last)
            {
                first = null;
                last = null;
            }
            else
            {
                first = first.Next;
                first.Previous = null;
            }

            count--;
        }

        public void RemoveLast()
        {
            if (last == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (first == last)
            {
                first = null;
                last = null;
            }
            else
            {
                last = last.Previous;
                last.Next = null;
            }

            count--;
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

        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
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
    }

    public class Node
    {
        public object Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(object value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

}
