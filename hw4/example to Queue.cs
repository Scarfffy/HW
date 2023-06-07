using hw4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

Queue queue = new Queue();
queue.Enqueue("Apple");
queue.Enqueue("Banana");
queue.Enqueue("Orange");

Console.WriteLine("Count: " + queue.Count);
Console.WriteLine("Peek: " + queue.Peek());

object item1 = queue.Dequeue();
object item2 = queue.Dequeue();

Console.WriteLine("Dequeued items: " + item1 + ", " + item2);
Console.WriteLine("Count: " + queue.Count);

Console.WriteLine("Contains 'Apple': " + queue.Contains("Apple"));

queue.Clear();
Console.WriteLine("Count after clearing: " + queue.Count);

