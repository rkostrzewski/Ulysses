using System;
using System.Collections.Generic;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.FileSystem.ImageReaders;

namespace Ulysses.ImageProviders.FileSystem
{
    public class FileSystemImageProvider : IImageProvider
    {
        private readonly bool _infitlyLoopOverFiles;
        private readonly IEnumerator<string> _filePathsEnumerator;
        private readonly IImageReader _imageReader;

        public FileSystemImageProvider(ImageModel imageModel, IEnumerable<string> filePaths, bool infitlyLoopOverFiles)
        {
            _infitlyLoopOverFiles = infitlyLoopOverFiles;
            _filePathsEnumerator = filePaths.GetEnumerator();

            switch (imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    _imageReader = new BitmapImageReader(imageModel);
                    break;
                case ImageBitDepth.Bpp12:
                    _imageReader = new RawImageReader(imageModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool TryToObtainImage(out Image image)
        {
            if (!_filePathsEnumerator.MoveNext())
            {
                var nextFilePending = false;

                if (_infitlyLoopOverFiles)
                {
                    _filePathsEnumerator.Reset();
                    nextFilePending = _filePathsEnumerator.MoveNext();
                }

                if (!nextFilePending)
                {
                    image = null;
                    return false;
                }
            }

            image = _imageReader.Read(_filePathsEnumerator.Current);

            return true;
        }

        public void Dispose()
        {
        }
    }
}