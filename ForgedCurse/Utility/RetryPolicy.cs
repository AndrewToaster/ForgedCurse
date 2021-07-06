using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForgedCurse.Utility
{
    /// <summary>
    /// Specifies how to handle actions that can be fixed by retrying (e.g: Web API communication)
    /// </summary>
    public class RetryPolicy
    {
        /// <summary>
        /// The amount of retries
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// The amount of milisecond to wait between each retry
        /// </summary>
        public int DelayAmount { get; set; }

        /// <summary>
        /// The error handler that handles when the policy runs out of retries
        /// </summary>
        public Action<Exception> ExceptionHandler { get; set; }

        /// <summary>
        /// Constructs a new instance of <see cref="RetryPolicy"/>
        /// </summary>
        /// <param name="retryCount">The amount of retries this policy will execute</param>
        /// <param name="delayAmount">The delay between each retry</param>
        /// <param name="errorHandler">The action to execute when the policy runs out of retries</param>
        public RetryPolicy(int retryCount, int delayAmount, Action<Exception> errorHandler)
        {
            if (retryCount < 0)
                throw new ArgumentException("The specified value must be a non-negative integer", nameof(retryCount));

            if (delayAmount < 0)
                throw new ArgumentException("The specified value must be a non-negative integer", nameof(delayAmount));

            RetryCount = retryCount;
            DelayAmount = delayAmount;
            ExceptionHandler = errorHandler;
        }

        /// <summary>
        /// Executes a method using this <see cref="RetryPolicy"/>
        /// </summary>
        /// <param name="method">The method to execute</param>
        /// <returns>Whether or not the method ran within the allowed <see cref="RetryCount"/></returns>
        public bool ExecutePolicy(Action method)
        {
            Exception lastErr = default;

            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    method();
                    return true;
                }
                catch (Exception e)
                {
                    lastErr = e;
                    Task.Delay(DelayAmount).Wait();
                }
            }

            ExceptionHandler(lastErr);
            return false;
        }

        /// <summary>
        /// Asynchronously executes a method using this <see cref="RetryPolicy"/>
        /// </summary>
        /// <param name="method">The method to execute</param>
        /// <returns>Whether or not the method ran within the allowed <see cref="RetryCount"/></returns>
        public async Task<bool> ExecutePolicyAsync(Func<Task> method)
        {
            Exception lastErr = default;

            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    await method();
                    return true;
                }
                catch (Exception e)
                {
                    lastErr = e;
                    await Task.Delay(DelayAmount);
                }
            }

            ExceptionHandler(lastErr);
            return false;
        }


        /// <summary>
        /// Executes a method using this <see cref="RetryPolicy"/>
        /// </summary>
        /// <param name="method">The method to execute</param>
        /// <param name="retVal">The value returned by the executed method</param>
        /// <returns>Whether or not the method ran within the allowed <see cref="RetryCount"/></returns>
        public PolicyResult<T> ExecutePolicy<T>(Func<T> method)
        {
            Exception lastErr = default;

            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    return PolicyResult<T>.Success(method());
                }
                catch (Exception e)
                {
                    lastErr = e;
                    Task.Delay(DelayAmount).Wait();
                }
            }

            ExceptionHandler(lastErr);
            return PolicyResult<T>.Error();
        }


        /// <summary>
        /// Asynchronously executes a method using this <see cref="RetryPolicy"/>
        /// </summary>
        /// <param name="method">The method to execute</param>
        /// <param name="retVal">The value returned by the executed method</param>
        /// <returns>Whether or not the method ran within the allowed <see cref="RetryCount"/></returns>
        public async Task<PolicyResult<T>> ExecutePolicyAsync<T>(Func<Task<T>> method)
        {
            Exception lastErr = default;

            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    return PolicyResult<T>.Success(await method());
                }
                catch (Exception e)
                {
                    lastErr = e;
                    await Task.Delay(DelayAmount);
                }
            }

            ExceptionHandler(lastErr);
            return PolicyResult<T>.Error();
        }

        /// <summary>
        /// Hold information about the result of a policy
        /// </summary>
        /// <typeparam name="T">The generic output of the policy</typeparam>
        public struct PolicyResult<T>
        {
            /// <summary>
            /// Indicates wheter or not the operation was successful
            /// </summary>
            public bool IsSuccess { get; }

            /// <summary>
            /// The value returned by the method (<see langword="default"/> for <see cref="RetryPolicy.ExecutePolicy(Action)"/>)
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// Constructs a new struct <see cref="PolicyResult{T}"/>, with <see cref="IsSuccess"/> set to true
            /// </summary>
            /// <param name="val">The value</param>
            public PolicyResult(T val)
            {
                Value = val;
                IsSuccess = true;
            }

            public static implicit operator T(PolicyResult<T> policyResult)
            {
                return policyResult.Value;
            }

            /// <summary>
            /// Returns a new <see cref="PolicyResult{T}"/> with it's <see cref="IsSuccess"/> being <see langword="true"/> and <see cref="Value"/> being <paramref name="value"/>
            /// </summary>
            public static PolicyResult<T> Success(T value)
            {
                return new PolicyResult<T>(value);
            }

            /// <summary>
            /// Returns a new <see cref="PolicyResult{T}"/> with it's <see cref="IsSuccess"/> being <see langword="false"/> and <see cref="Value"/> being <see langword="default"/>
            /// </summary>
            public static PolicyResult<T> Error()
            {
                return new PolicyResult<T>();
            }
        }
    }
}
