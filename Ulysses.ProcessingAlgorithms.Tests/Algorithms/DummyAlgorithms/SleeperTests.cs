using System;
using System.Diagnostics;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.DummyAlgorithms;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;

namespace Ulysses.ProcessingAlgorithms.Tests.Algorithms.DummyAlgorithms
{
    [TestFixture]
    public class SleeperTests
    {
        [Test]
        public void ShouldDoNothingForSetAmountOfTime()
        {
            // Given
            const double accuracyOfTimeMeasurementInMilliseconds = 25d;
            const double millisecondsToSleep = 500d;
            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            
            var template = new SleeperTemplate { SleepTimeInMilliseconds = (int)millisecondsToSleep };
            var sleeper = new Sleeper(template);

            // When
            stopwatch.Start();
            sleeper.ProcessImage(null);
            stopwatch.Stop();

            // Then
            Assert.AreEqual(millisecondsToSleep, stopwatch.ElapsedMilliseconds, accuracyOfTimeMeasurementInMilliseconds);
        }
    }
}
