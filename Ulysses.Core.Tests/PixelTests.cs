using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.Core.Tests.TestCases;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class PixelTests
    {
        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.ByteTestCases))]
        public void ShouldCorrectlyConvertFromByte(byte value, Pixel expected)
        {
            // Given
            // When
            var actual = (Pixel)value;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.UshortTestCases))]
        public void ShouldCorrectlyConvertFromUshort(ushort value, Pixel expected)
        {
            // Given
            // When
            var actual = (Pixel)value;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.IntTestCases))]
        public void ShouldCorrectlyConvertFromInt(int value, Pixel expected)
        {
            // Given
            // When
            var actual = (Pixel)value;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.ByteTestCases))]
        public void ShouldCorrectlyConvertToByte(byte expected, Pixel value)
        {
            // Given
            // When
            var actual = (byte)value;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.UshortTestCases))]
        public void ShouldCorrectlyConvertToUshort(ushort expected, Pixel value)
        {
            // Given
            // When
            var actual = (ushort)value;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.IntTestCases))]
        public void ShouldCorrectlyConvertToInt(int source, Pixel value)
        {
            // Given
            // When
            var actual = (int)value;
            var expected = source > 0 ? source < ushort.MaxValue ? source : ushort.MaxValue : 0;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfPixelsUsingOperator(Pixel first, Pixel second, bool expected)
        {
            // Given
            // When
            var actual = first == second;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfPixelsUsingEqualsCall(Pixel first, Pixel second, bool expected)
        {
            // Given
            // When
            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfPixelAndObjectUsingEqualsCall(Pixel first, object second, bool expected)
        {
            // Given
            // When

            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkInequalityOfPixels(Pixel first, Pixel second, bool notExpected)
        {
            // Given
            // When

            var actual = first != second;
            
            // Then
            Assert.AreNotEqual(notExpected, actual);
        }

        [Test]
        [TestCaseSource(typeof (PixelTestCases), nameof(PixelTestCases.GetHashCodeTestCases))]
        public void ShouldCorrectlyReturnHashCode(Pixel value, int expected)
        {
            // Given
            // When

            var actual = value.GetHashCode();

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.AdditionTestCases))]
        public void ShouldCorrectlyAddTwoPixels(Pixel first, Pixel second, Pixel expected)
        {
            // Given
            // When

            var actual = first + second;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.SubtractionTestCases))]
        public void ShouldCorrectlySubtractTwoPixels(Pixel first, Pixel second, Pixel expected)
        {
            // Given
            // When

            var actual = first - second;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PixelTestCases), nameof(PixelTestCases.MultiplicationTestCases))]
        public void ShouldCorrectlyMultiplyTwoPixels(Pixel first, Pixel second, Pixel expected)
        {
            // Given
            // When

            var actual = first * second;

            // Then
            Assert.AreEqual(expected, actual);
        }
    }
}
