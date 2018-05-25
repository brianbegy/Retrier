using System;

namespace Retrier
{
    /// <summary>
    /// class for retrying functions that return T
    /// </summary>
    /// <typeparam name="T">What type does the function return?</typeparam>
    public static class OfT<T>
    {
        /// <summary>
        /// attempts to run action n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="func">the function to call</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <returns>the return value of func</returns>        
        public static T RetryNTimes(Func<T> func, int maxTimes)
        {
            return RetryNTimes(func, maxTimes, 1);
        }

        /// <summary>
        /// attempts to run action n maxTimes and throws an exception on attempt n+1
        /// </summary>
        /// <param name="func">the function to call</param>
        /// <param name="maxTimes">How many times have we already tried this?</param>
        /// <param name="n">what attempt is this?</param>
        /// <returns>the return value of func</returns>        
        private static T RetryNTimes(Func<T> func, int maxTimes, int n)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception)
            {
                // n should never > maxTimes, but why not?
                if (n >= maxTimes)  
                {
                    throw;
                }

                return RetryNTimes(func, maxTimes, n + 1);
            }
        }
    }
}
