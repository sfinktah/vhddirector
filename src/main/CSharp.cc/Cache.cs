using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.cc
{
    public static class Cache
    {
        public static Dictionary<string, WeakReference> _cache;

        static Cache()
        {
            _cache = new Dictionary<string, WeakReference>();
        }
        // Returns the number of items in the cache.
        public static int Count
        {
            get
            {
                return _cache.Count;
            }
        }

        public static bool TryGetValue(String key, out object o)
        {
            if (key == null)
            {
                o = null;
                return false;
            }
            WeakReference wr;
            if (!_cache.TryGetValue(key, out wr))
            {
                o = null;
                return false;
            }

            if (wr == null)
            {
                _cache.Remove(key);
                o = null;
                return false;
            }

            o = wr.Target;
            return true;
        }


        public static void Add(string key, object o)
        {
            Object _o;
            if (TryGetValue(key, out _o))
            {
                _cache.Remove(key);
            }
            _cache.Add(key, new WeakReference(o, false));
        }
    }
}
