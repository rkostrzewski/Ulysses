using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        private readonly NonUniformityModel _nonUniformityModel;
        private readonly ImageModel _imageModel;

        public TwoPointNonUniformityCorrectionAlgorithm(TwoPointNonUniformityCorrectionTemplate template)
        {
            _imageModel = template.ImageModel;
            _nonUniformityModel = template.NonUniformityModel;
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            var outputImage = inputImage * _nonUniformityModel.GainCoefficients + _nonUniformityModel.OffsetCoefficients ;

            return outputImage.ToImage();
        }
    }
}