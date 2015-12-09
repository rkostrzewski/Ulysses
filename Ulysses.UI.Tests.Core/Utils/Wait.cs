using System;
using System.Threading;

namespace Ulysses.UI.Tests.Core.Utils
{
    public class Wait
    {
        private static readonly TimeSpan DefaultPollingTime = TimeSpan.FromMilliseconds(250);
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(5);

        public Wait()
        {
            PollingTime = DefaultPollingTime;
        }

        public Wait(TimeSpan pollingTime)
        {
            PollingTime = pollingTime;
        }

        public TimeSpan PollingTime { get; }

        public bool Until(Func<bool> function)
        {
            return Until(function, DefaultTimeout);
        }

        public bool Until(Func<bool> function, TimeSpan timeout)
        {
            var elapsedTime = TimeSpan.Zero;

            while (elapsedTime < timeout)
            {
                if (function.Invoke())
                {
                    return true;
                }

                Thread.Sleep(PollingTime);
                elapsedTime += PollingTime;
            }

            return false;
        }
    }
}