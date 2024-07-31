using System;
using System.Runtime.InteropServices.JavaScript;
using System.Collections.Generic;
using System.Collections.ObjectModel;

Console.WriteLine("Hello, Browser!");


public partial class MyClass
{
    [JSExport]
    internal static string Greeting()
    {
        var text = $"Hello, World! Greetings from {GetHRef()}";
        Console.WriteLine(text);
        World w = new World();
        Entity e = w.CreateEntity();
        Position pos = default(Position);
        e.AddComponent(pos);

        return text;
    }

    [JSImport("window.location.href", "main.js")]
    internal static partial string GetHRef();
}


public struct Position : Component
{
    public int x, y;
}

public interface Component { }

public interface Entity
{
    public int id { get; }
    public void AddComponent<T>(T component) where T : Component;
    public T GetComponent<T>() where T : Component;
}

class EntityImpl : Entity
{
    public int id { get; }

    private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();

    public EntityImpl(int id)
    {
        this.id = id;
    }

    public void AddComponent<T>(T component) where T : Component
    {
        _components[typeof(T).ToString()] = component;
    }

    public T GetComponent<T>() where T : Component
    {
        _components.TryGetValue(typeof(T).ToString(), out var component);
        return (T)component;
    }
}

public class World
{
    private List<Entity> _entities = new List<Entity>();
    private List<int> _freeIndexes = new List<int>();

    public ReadOnlyCollection<Entity> entities
    {
        get => this._entities.AsReadOnly();
    }

    public Entity CreateEntity()
    {
        Entity entity = new EntityImpl(this.entities.Count);
        this._entities.Add(entity);
        return entity;
    }

}
