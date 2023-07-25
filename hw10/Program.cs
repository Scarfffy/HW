using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
//1
[TestFixture]
public class ListTests
{
    [Test]
    public void Add_ItemToList_ShouldIncreaseCount()
    {
        List<int> list = new List<int>();
        list.Add(1);

        Assert.AreEqual(1, list.Count);
    }

    [Test]
    public void Remove_ItemFromList_ShouldDecreaseCount()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        list.Remove(2);

        Assert.AreEqual(2, list.Count);
    }
}

//2
[TestFixture]
public class TreeTests
{
    [Test]
    public void AddNode_ToTree_ShouldIncreaseCount()
    {
        Tree<int> tree = new Tree<int>();
        tree.Add(1);

        Assert.AreEqual(1, tree.Count);
    }

    [Test]
    public void RemoveNode_FromTree_ShouldDecreaseCount()
    {
        Tree<int> tree = new Tree<int>();
        tree.Add(1);
        tree.Add(2);
        tree.Remove(2);

        Assert.AreEqual(1, tree.Count);
    }
}

//3
[TestFixture]
public class LinqTests
{
    [Test]
    public void Linq_Select_ShouldReturnTransformedData()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        var result = numbers.Select(x => x * 2).ToList();

        CollectionAssert.AreEqual(new List<int> { 2, 4, 6, 8, 10 }, result);
    }

    [Test]
    public void Linq_Where_ShouldReturnFilteredData()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        var result = numbers.Where(x => x % 2 == 0).ToList();

        CollectionAssert.AreEqual(new List<int> { 2, 4 }, result);
    }
}