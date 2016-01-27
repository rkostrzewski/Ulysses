using System.Collections.Generic;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests.TestCases
{
    public static class PixelTestCases
    {
        public static IEnumerable<object[]> ByteTestCases
        {
            get
            {
                yield return new object[] { (byte)0, new Pixel(0) };
                yield return new object[] { (byte)2, new Pixel(2) };
                yield return new object[] { (byte)(byte.MaxValue - 1), new Pixel(254) };
                yield return new object[] { byte.MaxValue, new Pixel(255) };
            }
        }

        public static IEnumerable<object[]> UshortTestCases
        {
            get
            {
                yield return new object[] { (ushort)0, new Pixel(0) };
                yield return new object[] { (ushort)2, new Pixel(2) };
                yield return new object[] { (ushort)255, new Pixel(255) };
                yield return new object[] { (ushort)1024, new Pixel(1024) };
                yield return new object[] { (ushort)(ushort.MaxValue - 1), new Pixel(ushort.MaxValue - 1) };
                yield return new object[] { ushort.MaxValue, new Pixel(ushort.MaxValue) };
            }
        }

        public static IEnumerable<object[]> IntTestCases
        {
            get
            {
                yield return new object[] { int.MinValue, new Pixel(0) };
                yield return new object[] { -1, new Pixel(0) };
                yield return new object[] { 0, new Pixel(0) };
                yield return new object[] { 1, new Pixel(1) };
                yield return new object[] { int.MaxValue - 1, new Pixel(ushort.MaxValue) };
                yield return new object[] { int.MaxValue, new Pixel(ushort.MaxValue) };
            }
        }

        public static IEnumerable<object[]> EqualityTestCases
        {
            get
            {
                yield return new object[] { new Pixel(0), new Pixel(0), true };
                yield return new object[] { new Pixel(1), new Pixel(1), true };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue), true };
                yield return new object[] { new Pixel(0), new Pixel(1), false };
                yield return new object[] { new Pixel(1), new Pixel(0), false };
                yield return new object[] { new Pixel(1), new Pixel(2), false };
                yield return new object[] { new Pixel(2), new Pixel(1), false };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(ushort.MinValue), false };
                yield return new object[] { new Pixel(ushort.MinValue), new Pixel(ushort.MaxValue), false };
                yield return new object[] { null, new Pixel(5), false };
                yield return new object[] { new Pixel(5), null, false };
            }
        }

        public static IEnumerable<object[]> GetHashCodeTestCases
        {
            get
            {
                yield return new object[] { new Pixel(0), ((ushort)0).GetHashCode() };
                yield return new object[] { new Pixel(1), ((ushort)1).GetHashCode() };
                yield return new object[] { new Pixel(5), ((ushort)5).GetHashCode() };
                yield return new object[] { new Pixel(ushort.MaxValue), ushort.MaxValue.GetHashCode() };
            }
        }

        public static IEnumerable<object[]> AdditionTestCases
        {
            get
            {
                yield return new object[] { new Pixel(0), new Pixel(0), new Pixel(0) };
                yield return new object[] { new Pixel(1), new Pixel(0), new Pixel(1) };
                yield return new object[] { new Pixel(0), new Pixel(1), new Pixel(1) };
                yield return new object[] { new Pixel(5), new Pixel(5), new Pixel(10) };
                yield return new object[] { new Pixel(9), new Pixel(7), new Pixel(16) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(0), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(0), new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(1), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(1), new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue) };
            }
        }

        public static IEnumerable<object[]> SubtractionTestCases
        {
            get
            {
                yield return new object[] { new Pixel(0), new Pixel(0), new Pixel(0) };
                yield return new object[] { new Pixel(1), new Pixel(0), new Pixel(1) };
                yield return new object[] { new Pixel(0), new Pixel(1), new Pixel(0) };
                yield return new object[] { new Pixel(5), new Pixel(5), new Pixel(0) };
                yield return new object[] { new Pixel(9), new Pixel(7), new Pixel(2) };
                yield return new object[] { new Pixel(6), new Pixel(8), new Pixel(0) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(0), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(0), new Pixel(ushort.MaxValue), new Pixel(0) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(1), new Pixel(ushort.MaxValue - 1) };
                yield return new object[] { new Pixel(1), new Pixel(ushort.MaxValue), new Pixel(0) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue), new Pixel(0) };
            }
        }

        public static IEnumerable<object[]> MultiplicationTestCases
        {
            get
            {
                yield return new object[] { new Pixel(0), new Pixel(0), new Pixel(0) };
                yield return new object[] { new Pixel(1), new Pixel(0), new Pixel(0) };
                yield return new object[] { new Pixel(0), new Pixel(1), new Pixel(0) };
                yield return new object[] { new Pixel(5), new Pixel(5), new Pixel(25) };
                yield return new object[] { new Pixel(9), new Pixel(7), new Pixel(63) };
                yield return new object[] { new Pixel(6), new Pixel(8), new Pixel(48) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(0), new Pixel(0) };
                yield return new object[] { new Pixel(0), new Pixel(ushort.MaxValue), new Pixel(0) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(1), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(1), new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue) };
                yield return new object[] { new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue), new Pixel(ushort.MaxValue) };
            }
        }
    }
}