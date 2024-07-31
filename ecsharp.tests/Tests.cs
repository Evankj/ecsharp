using NUnit.Framework;
namespace ecsharp.tests;


public class ComponentA : Component { }
public class ComponentB : Component { }

[TestFixture]
public class ECSTests
{
    World _world;

    [SetUp]
    public void Setup()
    {
        _world = new World();
    }

    [Test]
    public void CreateEntity_ShouldCreateEntityWithUniqueID()
    {
        var entity1 = _world.CreateEntity();
        var entity2 = _world.CreateEntity();

        Assert.That(entity1.id, Is.Not.EqualTo(entity2.id));
        Assert.That(entity1.id, Is.EqualTo(0));
        Assert.That(entity2.id, Is.EqualTo(1));
    }

    [Test]
    public void AddComponent_ShouldStoreComponent()
    {
        var entity = _world.CreateEntity();
        var componentA = new ComponentA();

        entity.AddComponent(componentA);

        Assert.That(componentA, Is.EqualTo(entity.GetComponent<ComponentA>()));
    }

    [Test]
    public void GetComponent_ShouldReturnCorrectComponent()
    {
        var entity = _world.CreateEntity();
        var componentA = new ComponentA();
        entity.AddComponent(componentA);

        var componentB = new ComponentB();
        entity.AddComponent(componentB);

        Assert.That(componentA, Is.EqualTo(entity.GetComponent<ComponentA>()));
        Assert.That(componentB, Is.EqualTo(entity.GetComponent<ComponentB>()));
    }

    [Test]
    public void GetComponent_NonExistentComponent_ShouldReturnNull()
    {
        var entity = _world.CreateEntity();

        Assert.That(entity.GetComponent<ComponentA>(), Is.Null);
    }

    // [Test]
    // public void WorldEntities_ShouldReturnAllEntities()
    // {
    //     var entity1 = _world.CreateEntity();
    //     var entity2 = _world.CreateEntity();
    //
    //     var entities = _world.entities;
    //
    //     Assert.That(entity1, Contains.Item(entities.ToList()));
    //     Assert.That(entity2, Contains.Item(entities.ToList()));
    //     Assert.That(entities.Count, Is.EqualTo(2));
    // }
}

