namespace Ulysses.UI.Tests.Core
{
    public class SearchCriteriaModel
    {
        public SearchCriteriaModel(SearchProperty property, string value, bool contains)
        {
            Property = property;
            Value = value;
            Contains = contains;
        }

        public SearchProperty Property { get; private set; }
        public string Value { get; private set; }
        public bool Contains { get; private set; }
    }
}