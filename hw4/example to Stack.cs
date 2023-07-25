using System;
using hw4;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

Stack stack = new Stack();
stack.Push("Apple");
stack.Push("Banana");
stack.Push("Orange");

Console.WriteLine("Count: " + stack.Count);
Console.WriteLine("Peek: " + stack.Peek());

object item1 = stack.Pop();
object item2 = stack.Pop();

Console.WriteLine("Popped items: " + item1 + ", " + item2);
Console.WriteLine("Count: " + stack.Count);

Console.WriteLine("Contains 'Apple': " + stack.Contains("Apple"));

stack.Clear();
Console.WriteLine("Count after clearing: " + stack.Count);
