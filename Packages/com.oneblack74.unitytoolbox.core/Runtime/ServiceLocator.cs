using System;
using System.Collections.Generic;

namespace oneblack74.UnityToolbox.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                throw new InvalidOperationException($"[ServiceLocator] Service {type.Name} already registered.");
            }
            _services[type] = service;
        }

        public static void Unregister<T>()
        {
            _services.Remove(typeof(T));
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"[ServiceLocator] Service {type.Name} not found.");
        }

        public static bool TryGet<T>(out T service)
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }
            service = default;
            return false;
        }

        public static void Clear()
        {
            _services.Clear();
        }
    }
}
