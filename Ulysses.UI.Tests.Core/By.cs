namespace Ulysses.UI.Tests.Core
{
    public class By
    {
        public static SearchCriteriaModel ControlId(string id, bool contains = false)
        {
            return new SearchCriteriaModel(SearchProperty.ControlId, id, contains);
        }

        public static SearchCriteriaModel Name(string name, bool contains = false)
        {
            return new SearchCriteriaModel(SearchProperty.Name, name, contains);
        }

        public static SearchCriteriaModel ClassName(string name, bool contains = false)
        {
            return new SearchCriteriaModel(SearchProperty.ClassName, name, contains);
        }
    }
}