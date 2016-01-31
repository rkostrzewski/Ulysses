using System.IO;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.ImageProviders.FileSystem.ImageReaders
{
    public class RawImageReader : IImageReader
    {
        private readonly ImageModel _imageModel;

        public RawImageReader(ImageModel imageModel)
        {
            _imageModel = imageModel;

            if (_imageModel.ImageBitDepth != ImageBitDepth.Bpp12)
            {
                throw new ImageModelMismatchException(GetType());
            }
        }

        public Image Read(string filePath)
        {
            var imageLength = _imageModel.Width * _imageModel.Height;
            var pixelValues = new ushort[imageLength];

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            using (var binaryReader = new BinaryReader(fileStream))
            {
                if (fileStream.Length != imageLength * sizeof(ushort))
                {
                    throw new ImageModelMismatchException(typeof (RawImageReader));
                }

                for (var i = 0; i < imageLength; i++)
                {
                    pixelValues[i] = binaryReader.ReadUInt16();
                }
            }

            return new Image(pixelValues, _imageModel);
        }
    }
}