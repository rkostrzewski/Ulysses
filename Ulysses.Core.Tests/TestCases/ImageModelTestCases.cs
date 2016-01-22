using System.Collections.Generic;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests.TestCases
{
    public static class ImageModelTestCases
    {
        public static IEnumerable<object[]> EqualityTestCases
        {
            get
            {
                yield return new object[] { new ImageModel(1, 1, ImageBitDepth.Bpp12), new ImageModel(1, 1, ImageBitDepth.Bpp12), true };
                yield return new object[] { new ImageModel(2, 2, ImageBitDepth.Bpp8), new ImageModel(2, 2, ImageBitDepth.Bpp8), true };
                yield return new object[] { new ImageModel(1, 2, ImageBitDepth.Bpp12), new ImageModel(1, 2, ImageBitDepth.Bpp12), true };
                yield return new object[] { new ImageModel(2, 1, ImageBitDepth.Bpp12), new ImageModel(2, 1, ImageBitDepth.Bpp12), true };
                yield return new object[] { new ImageModel(1, 2, ImageBitDepth.Bpp8), new ImageModel(1, 2, ImageBitDepth.Bpp8), true };
                yield return new object[] { new ImageModel(2, 1, ImageBitDepth.Bpp8), new ImageModel(2, 1, ImageBitDepth.Bpp8), true };
                yield return new object[] { new ImageModel(1, 1, ImageBitDepth.Bpp12), new ImageModel(2, 2, ImageBitDepth.Bpp12), false };
                yield return new object[] { new ImageModel(1, 2, ImageBitDepth.Bpp8), new ImageModel(2, 2, ImageBitDepth.Bpp8), false };
                yield return new object[] { new ImageModel(1, 2, ImageBitDepth.Bpp12), new ImageModel(2, 2, ImageBitDepth.Bpp12), false };
                yield return new object[] { new ImageModel(2, 2, ImageBitDepth.Bpp8), new ImageModel(2, 2, ImageBitDepth.Bpp12), false };
                yield return new object[] { new ImageModel(2, 2, ImageBitDepth.Bpp12), new ImageModel(2, 2, ImageBitDepth.Bpp8), false };
                yield return new object[] { new ImageModel(1, 2, ImageBitDepth.Bpp8), null, false };
            }
        }
    }
}