using hw4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

LinkedList linkedList = new LinkedList();
linkedList.Add("Apple");
linkedList.Add("Banana");
linkedList.AddFirst("Orange");
linkedList.Insert(2, "Grape");

Console.WriteLine("Elements in the list:");
for (int i = 0; i < linkedList.Count; i++)
{
    Console.WriteLine(linkedList.ToArray()[i]);
}

Console.WriteLine("Contains 'Banana': " + linkedList.Contains("Banana"));

object[] array = linkedList.ToArray();
Console.WriteLine("Array:");
foreach (object item in array)
{
    Console.WriteLine(item);
}

Console.WriteLine("First element: " + linkedList.First);
Console.WriteLine("Last element: " + linkedList.Last);
