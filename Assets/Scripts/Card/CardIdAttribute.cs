using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CardIdAttribute : Attribute
{
    public string Id { get; }

    public CardIdAttribute(string id)
    {
        Id = id;
    }
}