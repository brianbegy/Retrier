using System;
using System.Collections.Generic;

namespace Retrier
{
    /// <summary>
    /// class for retrying functions that return T
    /// </summary>
    /// <typeparam name="T">What type does the function return?</typeparam>
    public static class OfT<T>
    {
        /// <summary>
        /// attempts to run function n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="function">the function to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="delay">How long (ms) to sleep between retries?</param>
        /// <param name="onFail">Invoke this action on failure.  Could be something like log.Error(Exception).</param>
        public static void RetryNTimes(Func<T> function, int maxTimes, int delay = 0, Action<Exception> onFail = null)
        {
            var s = new List<int>();
            for (int i = 0; i < maxTimes; i++)
            {
                s.Add(delay);
            }

            RetryNTimes(function, maxTimes, 0, s.ToArray(), onFail);
        }

        /// <summary>
        /// attempts to run function n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="function">the function to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="backoffSchedule">array of integers for how long to sleep between retries.
        /// [500,1000,1500] means sleep for 500 on first try, 1000 on second try, 1500 on third try.</param>
        /// <param name="onFail">Invoke this action on failure.  Could be something like log.Error(Exception).</param>
        public static void RetryNTimes(Func<T> function, int maxTimes, int[] backoffSchedule, Action<Exception> onFail = null)
        {
            if (backoffSchedule.Length != maxTimes)
            {
                throw new ArgumentException(
                    $"You told us to retry {maxTimes} {Pluralize(maxTimes, "time", "times")} but only gave us enough backoff schedule for {Pluralize(backoffSchedule.Length, "try", "tries")}.");
            }

            RetryNTimes(function, maxTimes, 0, backoffSchedule, onFail);
        }

        /// <summary>
        /// attempts to run function n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="function">the function to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="n">which attempt is this?</param>
        /// <param name="backoffSchedule">array of integers for how long to sleep between retries.
        /// [500,1000,1500] means sleep for 500 on first try, 1000 on second try, 1500 on third try.</param>
        /// <param name="onFail">Invoke this action on failure.  Could be something like log.Error(Exception).</param>
        private static void RetryNTimes(Func<T> function, int maxTimes, int n, int[] backoffSchedule, Action<Exception> onFail)
        {
            try
            {
                function.Invoke();
            }
            catch (Exception ex)
            {
                onFail?.Invoke(ex);

                if (n >= maxTimes)
                {
                    throw;
                }

                System.Threading.Thread.Sleep(backoffSchedule[n]);
                RetryNTimes(function, maxTimes, n + 1, backoffSchedule, onFail);
            }
        }

        /// <summary>
        /// returns singular for 1 plural for all other cases.  Internationalizing this should be done, but not now.
        /// </summary>
        /// <param name="number">how many of the thing</param>
        /// <param name="singular">what to say if 1</param>
        /// <param name="plural">what to say if 0 or >1</param>
        /// <returns>either singular or plural string</returns>
        private static string Pluralize(int number, string singular, string plural)
        {
            if (number == 1)
            {
                return singular;
            }

            return plural;
        }
    }
}