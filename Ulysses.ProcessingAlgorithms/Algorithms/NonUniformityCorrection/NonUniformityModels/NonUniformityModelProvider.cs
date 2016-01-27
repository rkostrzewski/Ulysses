using System.IO;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Exceptions;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class NonUniformityModelProvider : INonUniformityModelProvider
    {
        public NonUniformityModel GetNonUniformityModel(string sourceFilePath, ImageModel imageModel)
        {
            var fileInfo = new FileInfo(sourceFilePath);
            var imageLength = imageModel.Width * imageModel.Height;
            var bytesToRead = 2 * imageLength * sizeof (double);

            if (fileInfo.Length != bytesToRead)
            {
                throw new InvalidNonUniformityModelSourceFormatException();
            }

            var gainCoefficientsValues = new double[imageModel.Width * imageModel.Height];
            var offsetCoefficientsValues = new double[imageModel.Width * imageModel.Height];

            using (var fileStream = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);

                    for (var i = 0; i < imageLength; i++)
                    {
                        gainCoefficientsValues[i] = binaryReader.ReadDouble();
                    }

                    for (var i = 0; i < imageLength; i++)
                    {
                        offsetCoefficientsValues[i] = binaryReader.ReadDouble();
                    }
                }
            }

            return new NonUniformityModel(gainCoefficientsValues, offsetCoefficientsValues, imageModel);
        }
    }
}