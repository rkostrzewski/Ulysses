using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.Misc;

namespace Ulysses.ProcessingAlgorithms.Algorithms.Misc
{
    public class SaveImage : IImageProcessingAlgorithm
    {
        private readonly SaveImageTemplate _template;
        private readonly ImageModel _imageModel;
        private const string FileNameFormat = "camera_image_{0}.{1}";
        private const string DateTimeFormat = "yyyy_MM_dd_T_HH_mm_ss_ffff";
        private const string RawFormatExtension = "raw";
        private const string PngFormatExtension = "png";

        public SaveImage(SaveImageTemplate template)
        {
            _template = template;
            _imageModel = _template.ImageModel;
        }


        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            var currentTime = DateTime.Now;

            if (_template.SaveAsRaw)
            {
                SaveAsRaw(inputImage, currentTime);
            }

            if (_template.SaveAsPng)
            {
                SaveAsPng(inputImage, currentTime);
            }

            return inputImage;
        }

        private void SaveAsRaw(Image inputImage, DateTime currentTime)
        {
            var fileName = string.Format(FileNameFormat, currentTime.ToString(DateTimeFormat), RawFormatExtension);
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            using(var binaryWriter = new BinaryWriter(fileStream))
            {
                for (var i = 0; i < _imageModel.Width * _imageModel.Height; i++)
                {
                    binaryWriter.Write((ushort)inputImage.ImagePixels[i]);
                }
            }
        }

        private void SaveAsPng(Image inputImage, DateTime currentTime)
        {
            var width = inputImage.ImageModel.Width;
            var height = inputImage.ImageModel.Height;
            var sourceBitDepth = inputImage.ImageModel.ImageBitDepth;
            var pixels = inputImage.ImagePixels.Select(p => (byte)p.ConvertToBitDepth(sourceBitDepth, ImageBitDepth.Bpp8)).ToArray();

            
            var rectangle = new Int32Rect(0, 0, width, height);
            var stride = width;
            var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Gray8, BitmapPalettes.Gray256);

            bitmap.Lock();
            bitmap.WritePixels(rectangle, pixels, stride, 0);
            bitmap.Unlock();

            var fileName = string.Format(FileNameFormat, currentTime.ToString(DateTimeFormat), PngFormatExtension);
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {

                var pngEncoder = new PngBitmapEncoder { Interlace = PngInterlaceOption.On };
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmap));
                pngEncoder.Save(fileStream);
            }
        }
    }
}
