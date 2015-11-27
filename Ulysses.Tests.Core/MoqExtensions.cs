using System.Collections.Generic;
using Moq.Language.Flow;

namespace Ulysses.Tests.Core
{
    public static class MoqExtensions
    {
        public static void ReturnsInOrder<T, TResult>(this IReturnsThrows<T, TResult> setup, params TResult[] results)
            where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }
    }
}