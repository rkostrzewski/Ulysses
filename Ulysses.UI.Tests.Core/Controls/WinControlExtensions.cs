using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace Ulysses.UI.Tests.Core.Controls
{
    public static class WinControlExtensions
    {
        internal static void SetSearchProperties(this WinControl control, ICollection<SearchCriteriaModel> searchCriteria)
        {
            if (!searchCriteria.Any())
            {
                throw new ArgumentException();
            }

            var properties = searchCriteria.Select(c => new PropertyExpression(c.Property.ToString(), c.Value, GetPropertyExpressionOperator(c))).ToArray();
            control.SearchProperties.AddRange(properties);
        }

        private static PropertyExpressionOperator GetPropertyExpressionOperator(SearchCriteriaModel criteria)
        {
            return criteria.Contains ? PropertyExpressionOperator.Contains : PropertyExpressionOperator.EqualTo;
        }
    }
}