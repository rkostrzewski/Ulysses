using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        private readonly NonUniformityModel _nonUniformityModel;

        public TwoPointNonUniformityCorrectionAlgorithm(TwoPointNonUniformityCorrectionTemplate template)
        {
            _nonUniformityModel = template.NonUniformityModel;
        }

        public Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = inputImagePixels * _nonUniformityModel.GainCoefficients + _nonUniformityModel.OffsetCoefficients ;

            return outputImage.ToImage();
        }
    }
}