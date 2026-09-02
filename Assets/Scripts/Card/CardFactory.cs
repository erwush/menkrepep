using System;
using System.Collections.Generic;
using System.Reflection;

public static class CardFactory
{
    private static Dictionary<string, Type> cardTypes = new();

    public static void Initialize()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        foreach (Type type in assembly.GetTypes())
        {
            if (type.IsAbstract)
                continue;

            if (!typeof(ItemCard).IsAssignableFrom(type))
                continue;

            CardIdAttribute attribute =
                type.GetCustomAttribute<CardIdAttribute>();

            if (attribute == null)
                continue;

            cardTypes.Add(attribute.Id, type);
        }
    }

    public static ItemCard CreateItem(string id)
    {
        if (!cardTypes.TryGetValue(id, out Type type))
        {
            throw new Exception($"Card ID '{id}' not found!");
        }

        return (ItemCard)Activator.CreateInstance(type);
    }
}