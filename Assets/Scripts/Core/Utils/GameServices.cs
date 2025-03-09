using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<TInterface, TImplementation>(TImplementation service)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var type = typeof(TInterface);
        if (!_services.ContainsKey(type))
        {
            _services[type] = service;
        }
        else
        {
            Debug.LogWarning($"Сервис {type} уже зарегистрирован.");
        }
    }

    public static T Get<T>() where T : class
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        Debug.LogError($"Сервис {type} не найден.");
        return null;
    }
}