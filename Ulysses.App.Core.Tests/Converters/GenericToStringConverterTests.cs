using System;
using System.Globalization;
using NUnit.Framework;
using Ulysses.App.Core.Converters;

namespace Ulysses.App.Core.Tests.Converters
{
    [TestFixture]
    public class GenericToStringConverterTests
    {
        [Test]
        public void ShouldConvertObjectToString()
        {
            // Given
            var converter = new GenericToStringConverter();
            var objectToConvert = Guid.NewGuid();

            // When
            var text = converter.Convert(objectToConvert, null, null, CultureInfo.InvariantCulture);

            // Then
            Assert.AreEqual(objectToConvert.ToString(), text);
        }

        [Test]
        public void ShouldNotConvertBackStringToObject()
        {
            // Given
            var converter = new GenericToStringConverter();
            var objectToConvertBack = (object)string.Empty;

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => converter.ConvertBack(objectToConvertBack, null, null, CultureInfo.InvariantCulture));
        }
    }
}
