using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Principal;

namespace CluedIn.Crawling.FileSystem.Helpers
{
    public enum FileSystemObjectTypes
    {
        Principal,
        NTAccount
    }

    public class LookupCache
    {
        /**********************************************************************************************************
         * FIELDS
         **********************************************************************************************************/

        private ConcurrentDictionary<Tuple<Enum, string>, object> cache = new ConcurrentDictionary<Tuple<Enum, string>, object>();

        private Dictionary<Enum, Func<SecurityIdentifier, object>> callbacks = new Dictionary<Enum, Func<SecurityIdentifier, object>>();

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        public static LookupCache Create()
        {
            var nameCache = new LookupCache();

            nameCache.AddCacheMissCallback(FileSystemObjectTypes.NTAccount, sid =>
            {
                return sid.Translate(typeof(NTAccount));
            });

            nameCache.AddCacheMissCallback(FileSystemObjectTypes.Principal, sid =>
            {
                return sid.GetUserPrincipal();
            });

            return nameCache;
        }

        public T GetName<T>(Enum type, SecurityIdentifier id)
        {
            var key = new Tuple<Enum, string>(type, id.Value);

            object name;

            if (this.cache.TryGetValue(key, out name))
                return (T)name;

            Func<SecurityIdentifier, object> callback;

            if (this.callbacks.TryGetValue(type, out callback))
            {
                try
                {
                    name = callback(id);
                    this.AddToCache(key, name);
                }
                catch (Exception)
                {
                    name = null;
                }

                return (T)name;
            }

            return default(T);
        }

        public void AddToCache(Enum type, SecurityIdentifier id, object name)
        {
            var key = new Tuple<Enum, string>(type, id.Value);
            this.AddToCache(key, name);
        }

        private void AddToCache(Tuple<Enum, string> key, object name)
        {
            this.cache[key] = name;
        }

        public void AddCacheMissCallback(Enum type, Func<SecurityIdentifier, object> callback)
        {
            this.callbacks[type] = callback;
        }
    }
}
