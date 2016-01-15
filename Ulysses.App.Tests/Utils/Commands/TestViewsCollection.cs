using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Prism.Regions;

namespace Ulysses.App.Tests.Utils.Commands
{
    public class TestViewsCollection : IViewsCollection
    {
        private readonly IEnumerable<object> _views;

        public TestViewsCollection()
        {
            _views = new List<object>
            {
                new TestView()
            };
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _views.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _views.GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool Contains(object value)
        {
            return _views.Contains(value);
        }
    }
}