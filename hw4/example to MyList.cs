using hw4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

MyList myList = new MyList();
myList.Add("Apple");
myList.Add("Banana");
myList.Insert(1, "Orange");
myList.Remove("Apple");

Console.WriteLine("Elements in the list:");
for (int i = 0; i < myList.Count; i++)
{
    Console.WriteLine(myList[i]);
}

Console.WriteLine("Index of 'Orange': " + myList.IndexOf("Orange"));
Console.WriteLine("Contains 'Banana': " + myList.Contains("Banana"));

object[] array = myList.ToArray();
Console.WriteLine("Array:");
foreach (object item in array)
{
    Console.WriteLine(item);
}

myList.Reverse();
Console.WriteLine("Reversed list:");
for (int i = 0; i < myList.Count; i++)
{
    Console.WriteLine(myList[i]);
}