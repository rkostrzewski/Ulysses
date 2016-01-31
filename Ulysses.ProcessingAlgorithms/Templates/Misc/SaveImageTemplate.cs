using System;
using Ulysses.ProcessingAlgorithms.Algorithms.Misc;

namespace Ulysses.ProcessingAlgorithms.Templates.Misc
{
    public class SaveImageTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public bool SaveAsRaw { get; set; }

        public bool SaveAsPng { get; set; }

        public override Type AlgorithmType => typeof (SaveImage);

        public override ImageProcessingAlgorithmGroup Group { get; }
    }
}
