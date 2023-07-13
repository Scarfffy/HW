using hw4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

DoublyLinkedList doublyLinkedList = new DoublyLinkedList();
doublyLinkedList.Add("Apple");
doublyLinkedList.Add("Banana");
doublyLinkedList.AddFirst("Orange");
doublyLinkedList.Remove("Banana");
doublyLinkedList.RemoveFirst();
doublyLinkedList.RemoveLast();

Console.WriteLine("Elements in the list:");
object[] array = doublyLinkedList.ToArray();
foreach (object item in array)
{
    Console.WriteLine(item);
}

Console.WriteLine("Contains 'Apple': " + doublyLinkedList.Contains("Apple"));

Console.WriteLine("First element: " + doublyLinkedList.First);
Console.WriteLine("Last element: " + doublyLinkedList.Last);
Console.WriteLine("Count: " + doublyLinkedList.Count);
