using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles.Xml.XPath
{
    internal static class ParentedMap
    {
        public static ParentedMap<TKey, TValue> Create<TKey, TValue>(ParentedMap<TKey, TValue> parent = null)
        {
            return ParentedMap<TKey, TValue>.Create(parent);
        }
    }

    internal abstract class ParentedMap<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        private ParentedMap() { }

        public void Add(TKey key, TValue value)
        {
            dict.Add(key, value);
        }

        public Opt<TValue> GetOwnOpt(TKey key)
        {
            TValue value;
            var hasValue = dict.TryGetValue(key, out value);
            return Opt.Create(hasValue, value);
        }

        protected abstract ParentedMap<TKey, TValue> Parent { get; }

        protected abstract Opt<TValue> GetInheritedOpt(TKey key);

        public Opt<TValue> GetOpt(TKey key)
        {
            var own = GetOwnOpt(key);
            return own.Any() ? own : GetInheritedOpt(key);
        }

        private class RootMap<TKeyx, TValuex> : ParentedMap<TKeyx, TValuex>
        {
            protected override ParentedMap<TKeyx, TValuex> Parent { get { return null; } }

            protected override Opt<TValuex> GetInheritedOpt(TKeyx key)
            {
                return Opt.Empty<TValuex>();
            }
        }

        private class ChildMap<TKeyx, TValuex> : ParentedMap<TKeyx, TValuex>
        {
            private readonly ParentedMap<TKeyx, TValuex> parent;

            internal ChildMap(ParentedMap<TKeyx, TValuex> parent)
            {
                this.parent = Checker.NotNull(parent, "parent");
            }

            protected override ParentedMap<TKeyx, TValuex> Parent { get { return parent; } }

            protected override Opt<TValuex> GetInheritedOpt(TKeyx key)
            {
                return parent.GetOpt(key);
            }
        }

        public static ParentedMap<TKey, TValue> Create(ParentedMap<TKey, TValue> parent = null)
        {
            if (parent == null)
            {
                return new RootMap<TKey, TValue>();
            }
            else
            {
                return new ChildMap<TKey, TValue>(parent);
            }
        }
    }
}
