using System;
using System.Collections.Generic;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.FileSystem.ImageReaders;
using Image = Ulysses.Core.Models.Image;

namespace Ulysses.ImageProviders.FileSystem
{
    public class FileSystemImageProvider : IImageProvider
    {
        private readonly IEnumerator<string> _filePathsEnumerator;
        private readonly IImageReader _imageReader;
        private readonly ImageModel _imageModel;

        public FileSystemImageProvider(ImageModel imageModel, IEnumerable<string> filePaths)
        {
            _imageModel = imageModel;
            _filePathsEnumerator = filePaths.GetEnumerator();

            switch (_imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    _imageReader = new BitmapImageReader(_imageModel);
                    break;
                case ImageBitDepth.Bpp12:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool TryToObtainImage(out Image image)
        {
            if (!_filePathsEnumerator.MoveNext())
            {
                image = null;
                return false;
            }

            image = GetImageFromFile(_filePathsEnumerator.Current);

            return true;
        }

        private Image GetImageFromFile(string filePath)
        {
            switch (_imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    return _imageReader.Read(filePath);
                case ImageBitDepth.Bpp12:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}