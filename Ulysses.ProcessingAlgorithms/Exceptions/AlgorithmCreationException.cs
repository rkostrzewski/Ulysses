using System;

namespace Ulysses.ProcessingAlgorithms.Exceptions
{
    [Serializable]
    public sealed class AlgorithmCreationException : Exception
    {
        public Type AlgorithmType { get; }

        public AlgorithmCreationException(Type algorithmType)
        {
            AlgorithmType = algorithmType;
        }
    }
}