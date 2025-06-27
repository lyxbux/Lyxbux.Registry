using System.Diagnostics.CodeAnalysis;

namespace Lyxbux.Registry
{
    public static class AppRegistry
    {
        private static Dictionary<string, object>? objects;

        [MemberNotNullWhen(true, nameof(objects))]
        public static bool IsStarted()
        {
            return objects != null;
        }
        public static bool Start()
        {
            if (IsStarted())
            {
                return false;
            }

            objects = new Dictionary<string, object>();
            return true;
        }
        public static bool Stop()
        {
            if (!IsStarted())
            {
                return false;
            }

            objects.Clear();
            objects = null;
            return true;
        }
        public static bool Register(string name, object context, bool force = false)
        {
            if (!IsStarted())
            {
                return false;
            }

            if (objects.ContainsKey(name))
            {
                if (force)
                {
                    objects[name] = context;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            objects.Add(name, context);
            return true;
        }
        public static bool Unregister(string name)
        {
            if (!IsStarted())
            {
                return false;
            }

            if (!objects.ContainsKey(name))
            {
                return false;
            }

            return objects.Remove(name);
        }
        public static bool Get(string name, out object? context)
        {
            if (!IsStarted())
            {
                context = null;
                return false;
            }

            if (!objects.ContainsKey(name))
            {
                context = null;
                return false;
            }

            context = objects[name];
            return true;
        }
        public static bool Get<T>(string name, out T? context)
        {
            if (!IsStarted())
            {
                context = default;
                return false;
            }

            if (!objects.ContainsKey(name))
            {
                context = default;
                return false;
            }

            context = (T)objects[name];
            return true;
        }
    }
}
