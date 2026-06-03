using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class RowColTests
{
    [Test]
    public void CanBeCreated()
    {
        var tuple = new RowCol(1, 1);
        Assert.That(tuple, Is.Not.Null);
    }

    [Test]
    public void ExposesRowAndColProperties()
    {
        var tuple = new RowCol(1, 2);
        Assert.That(tuple.Row, Is.EqualTo(1));
        Assert.That(tuple.Col, Is.EqualTo(2));
    }
}
