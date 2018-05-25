using System;
using System.Collections.Generic;
using System.Linq;

namespace Retrier
{
    /// <summary>
    /// class for doing simple retries that do not return anything.
    /// </summary>
    public static class Simple
    {
        /// <summary>
        /// attempts to run action n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="action">the action to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="delay">How long (ms) to sleep between retries?</param>
        /// <param name="onFail">Invoke this action on failure.  Could be something like log.Error(Exception).</param>
        public static void RetryNTimes(Action action, int maxTimes, int delay = 500, Action<Exception> onFail = null)
        {
            OfT<int>.RetryNTimes(CreateFunction(action), maxTimes, delay);
        }

        /// <summary>
        /// attempts to run action n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="action">the action to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="backoffShedule">array of integers for how long to sleep between retries.
        /// [500,1000,1500] means sleep for 500 on first try, 1000 on second try, 1500 on third try.</param>
        /// <param name="onFail">Invoke this action on failure.  Could be something like log.Error(Exception).</param>
        public static void RetryNTimes(Action action, int maxTimes, int[] backoffShedule, Action<Exception> onFail = null)
        {
            OfT<int>.RetryNTimes(CreateFunction(action), maxTimes, backoffShedule, onFail);
        }

        /// <summary>
        /// creates an arbitrary function from an action
        /// </summary>
        /// <param name="a">the action</param>
        /// <returns>a function that returns 0 or throws an exception</returns>
        private static Func<int> CreateFunction(Action a)
        {
            return new Func<int>(() =>
            {
                a.Invoke();
                return 0;
            });
        }
    }
}
