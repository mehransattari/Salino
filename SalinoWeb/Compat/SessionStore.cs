using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// In-process object session, equivalent to classic ASP.NET InProc session state.
    /// ASP.NET Core's ISession only stores bytes; the legacy code stores live objects
    /// (e.g. List&lt;ShopCartItem&gt;) and casts them back, so values are kept in memory
    /// keyed by the ASP.NET Core session id.
    /// </summary>
    public sealed class SessionStore
    {
        public static readonly TimeSpan IdleTimeout = TimeSpan.FromMinutes(20);
        private const string InitKey = "__salino_session";

        private readonly HttpContext _context;
        private readonly IMemoryCache _cache;
        private ConcurrentDictionary<string, object> _items;

        public SessionStore(HttpContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        private ConcurrentDictionary<string, object> Items
        {
            get
            {
                if (_items != null) return _items;

                var session = _context.Session;
                // Writing a value makes ASP.NET Core issue the session cookie and keep the id stable.
                if (session.GetString(InitKey) == null) session.SetString(InitKey, "1");

                _items = _cache.GetOrCreate("session:" + session.Id, entry =>
                {
                    entry.SlidingExpiration = IdleTimeout;
                    return new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                });
                return _items;
            }
        }

        public object this[string key]
        {
            get => Items.TryGetValue(key, out var value) ? value : null;
            set
            {
                if (value == null) Items.TryRemove(key, out _);
                else Items[key] = value;
            }
        }

        public void Remove(string key) => Items.TryRemove(key, out _);

        public void Clear() => Items.Clear();

        public void Abandon()
        {
            Items.Clear();
            _context.Session.Clear();
        }

        public int Count => Items.Count;

        public static SessionStore For(HttpContext context)
        {
            const string itemKey = "__salino_session_store";
            if (context.Items[itemKey] is SessionStore existing) return existing;
            var store = new SessionStore(context, context.RequestServices.GetRequiredService<IMemoryCache>());
            context.Items[itemKey] = store;
            return store;
        }
    }
}
