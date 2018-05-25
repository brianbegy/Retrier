using System;

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
        /// <param name="n">which attempt is this?</param>
        public static void RetryNTimes(Action action, int maxTimes, int n)
        {
            n++;
            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
                if (n > maxTimes)
                {
                    throw;
                }

                RetryNTimes(action, maxTimes, n);
            }
        }
        
        /// <summary>
        /// attempts to run action n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="action">the action to execute</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        private static void RetryNTimes(Action action, int maxTimes)
        {
            RetryNTimes(action, maxTimes, 0);
        }
    }
}
