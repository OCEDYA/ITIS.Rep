using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string? FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];
            
        return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (left >= phrases.Count || !phrases[left].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return new string[0];
        
        var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        var actualCount = Math.Min(count, right - left);
        
        var result = new string[actualCount];
        for (var i = 0; i < actualCount; i++)
            result[i] = phrases[left + i];
        
        return result;
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        return Math.Max(0, right - left);
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        var phrases = new List<string>();
        var result = AutocompleteTask.GetTopByPrefix(phrases, "test", 5);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TopByPrefix_ReturnsCorrectCount()
    {
        var phrases = new List<string> { "apple", "application", "banana", "appetite", "apricot" };
        var result = AutocompleteTask.GetTopByPrefix(phrases, "app", 3);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo("apple"));
        Assert.That(result[1], Is.EqualTo("application"));
        Assert.That(result[2], Is.EqualTo("appetite"));
    }

    [Test]
    public void TopByPrefix_ReturnsLessWhenNotEnough()
    {
        var phrases = new List<string> { "apple", "banana" };
        var result = AutocompleteTask.GetTopByPrefix(phrases, "app", 5);
        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo("apple"));
    }

    [Test]
    public void TopByPrefix_ReturnsEmpty_WhenNoMatches()
    {
        var phrases = new List<string> { "apple", "banana" };
        var result = AutocompleteTask.GetTopByPrefix(phrases, "xyz", 5);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        var phrases = new List<string> { "apple", "banana", "cherry" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "");
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void CountByPrefix_ReturnsZero_WhenNoMatches()
    {
        var phrases = new List<string> { "apple", "banana", "cherry" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "xyz");
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void CountByPrefix_ReturnsCorrectCount()
    {
        var phrases = new List<string> { "apple", "application", "banana", "appetite", "apricot" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "app");
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void CountByPrefix_WorksWithSingleElement()
    {
        var phrases = new List<string> { "apple" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "app");
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void TopByPrefix_ReturnsCorrectOrder()
    {
        var phrases = new List<string> { "zebra", "apple", "application", "banana", "appetite" };
        var sortedPhrases = phrases.OrderBy(p => p, StringComparer.InvariantCultureIgnoreCase).ToList();
        var result = AutocompleteTask.GetTopByPrefix(sortedPhrases, "app", 2);
        Assert.That(result.Length, Is.EqualTo(2));
        Assert.That(result[0], Is.EqualTo("apple"));
        Assert.That(result[1], Is.EqualTo("application"));
    }
}
