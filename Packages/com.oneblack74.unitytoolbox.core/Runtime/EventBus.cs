using System;
using System.Collections.Generic;

namespace oneblack74.UnityToolbox.Core
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public static void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Delegate>();
            }
            _handlers[type].Add(handler);
        }

        public static void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
            {
                _handlers[type].Remove(handler);
            }
        }

        public static void Publish<T>(T evt)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type)) return;

            // Copy to avoid modification during iteration
            var handlers = new List<Delegate>(_handlers[type]);
            foreach (var handler in handlers)
            {
                (handler as Action<T>)?.Invoke(evt);
            }
        }

        public static void Clear()
        {
            _handlers.Clear();
        }
    }
}
