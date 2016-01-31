using System.Collections.Generic;
using System.Linq;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests.TestCases
{
    public static class ImageTestCases
    {
        public static IEnumerable<object[]> EqualityTestCases
        {
            get
            {
                yield return new object[] { new Image(new ImageModel(25, 25, ImageBitDepth.Bpp8)), new Image(new ImageModel(25, 25, ImageBitDepth.Bpp8)), true };
                yield return
                    new object[]
                    {
                        new Image(Enumerable.Range(0, 20).Select(i => (Pixel)i).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8)),
                        new Image(Enumerable.Range(0, 20).Select(i => (Pixel)i).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8)),
                        true
                    };
                var image = new Image(Enumerable.Range(0, 20).Select(i => (Pixel)i).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8));
                yield return new object[] { image, image, true };
                yield return new object[] { new Image(new ImageModel(25, 25, ImageBitDepth.Bpp8)), new Image(new ImageModel(25, 25, ImageBitDepth.Bpp12)), false };
                yield return new object[] { new Image(new ImageModel(25, 20, ImageBitDepth.Bpp8)), new Image(new ImageModel(25, 25, ImageBitDepth.Bpp8)), false };
                yield return
                    new object[]
                    {
                        new Image(Enumerable.Range(0, 20).Select(i => (Pixel)i).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8)),
                        new Image(Enumerable.Range(0, 20).Select(i => (Pixel)(2 * i)).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8)),
                        false
                    };
                yield return new object[] { new Image(Enumerable.Range(0, 20).Select(i => (Pixel)i).ToArray(), new ImageModel(4, 5, ImageBitDepth.Bpp8)), null, false };
            }
        }
    }
}