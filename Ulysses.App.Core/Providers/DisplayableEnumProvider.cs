using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Ulysses.App.Core.Providers
{
    public class DisplayableEnumProvider<T>
    {
        private readonly CultureInfo _cultureInfo;
        private readonly ResourceManager _resourceManager;

        public DisplayableEnumProvider(ResourceManager resourceManager, CultureInfo cultureInfo = null)
        {
            if (!typeof (T).IsEnum)
            {
                throw new InvalidOperationException();
            }

            _resourceManager = resourceManager;
            _cultureInfo = cultureInfo;
        }

        public IEnumerable<Displayable<T>> GetDisplayableEnums()
        {
            return Enum.GetValues(typeof (T)).Cast<T>().Select(e =>
            {
                var localizedText = _resourceManager.GetString(e.ToString(), _cultureInfo);
                return new Displayable<T> { Value = e, DisplayText = localizedText ?? e.ToString() };
            });
        }
    }
}