using System;
using JetBrains.Annotations;

namespace Ulysses.Core.Exceptions
{
    [Serializable]
    public sealed class ImageModelMismatchException : Exception
    {
        public ImageModelMismatchException()
        {
        }

        public ImageModelMismatchException(Type sourceType)
        {
            ThrownBy = sourceType;
        }

        [CanBeNull]
        public Type ThrownBy { get; }
    }
}