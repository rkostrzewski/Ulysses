using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Prism.Regions;

namespace Ulysses.App.Tests.Utils.Commands
{
    public class TestRegionCollection : IRegionCollection
    {
        private readonly List<IRegion> _list;

        public TestRegionCollection()
        {
            _list = new List<IRegion>();
        }

        public IEnumerator<IRegion> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(IRegion region)
        {
            _list.Add(region);
        }

        public bool Remove(string regionName)
        {
            throw new NotImplementedException();
        }

        public bool ContainsRegionWithName(string regionName)
        {
            return _list.Any(r => r.Name == regionName);
        }

        public void Add(string regionName, IRegion region)
        {
            throw new NotImplementedException();
        }

        public IRegion this[string regionName]
        {
            get
            {
                return _list.FirstOrDefault(r => r.Name == regionName);
            }
        }
    }
}