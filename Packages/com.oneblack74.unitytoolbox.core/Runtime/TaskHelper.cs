using System;
using Cysharp.Threading.Tasks;

namespace oneBlack74.UnityToolbox.Core
{
    /// <summary>
    /// Utility helpers for async task operations.
    /// </summary>
    public static class TaskHelper
    {
        // Default timeout in seconds
        private const float _defaultTimeout = 10f;
        
        // Default retry attempts
        private const int _defaultRetryAttempts = 3;

        /// <summary>
        /// Waits until the condition is true.
        /// </summary>
        public static async UniTask WaitUntil(Func<bool> condition)
        {
            await UniTask.WaitUntil(condition);
        }

        /// <summary>
        /// Waits while the condition is true.
        /// </summary>
        public static async UniTask WaitWhile(Func<bool> condition)
        {
            await UniTask.WaitWhile(condition);
        }

        /// <summary>
        /// Runs a task with a timeout. Throws TimeoutException if exceeded.
        /// </summary>
        public static async UniTask RunWithTimeout(Func<UniTask> taskFactory, float seconds = _defaultTimeout)
        {
            await taskFactory().Timeout(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Runs a task with a timeout and returns a result.
        /// </summary>
        public static async UniTask<T> RunWithTimeout<T>(Func<UniTask<T>> taskFactory, float seconds = _defaultTimeout)
        {
            return await taskFactory().Timeout(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Retries a task on failure up to maxAttempts times.
        /// </summary>
        public static async UniTask Retry(Func<UniTask> taskFactory, int maxAttempts = _defaultRetryAttempts, float delayBetweenAttempts = 0f)
        {
            int attempts = 0;
            
            while (true)
            {
                try
                {
                    await taskFactory();
                    return;
                }
                catch (Exception ex)
                {
                    attempts++;
                    
                    if (attempts >= maxAttempts)
                        throw new Exception($"Task failed after {maxAttempts} attempts. Last error: {ex.Message}", ex);

                    if (delayBetweenAttempts > 0f)
                        await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenAttempts));
                }
            }
        }

        /// <summary>
        /// Retries a task on failure and returns a result.
        /// </summary>
        public static async UniTask<T> Retry<T>(Func<UniTask<T>> taskFactory, int maxAttempts = _defaultRetryAttempts, float delayBetweenAttempts = 0f)
        {
            int attempts = 0;
            
            while (true)
            {
                try
                {
                    return await taskFactory();
                }
                catch (Exception ex)
                {
                    attempts++;
                    
                    if (attempts >= maxAttempts)
                        throw new Exception($"Task failed after {maxAttempts} attempts. Last error: {ex.Message}", ex);

                    if (delayBetweenAttempts > 0f)
                        await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenAttempts));
                }
            }
        }

        /// <summary>
        /// Fires and forgets a task with optional error handling.
        /// </summary>
        public static void SafeFireAndForget(UniTask task, Action<Exception> onError = null)
        {
            FireAndForgetInternal(task, onError).Forget();
        }

        private static async UniTaskVoid FireAndForgetInternal(UniTask task, Action<Exception> onError)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (onError != null)
                    onError(ex);
                else
                    UnityEngine.Debug.LogError($"[TaskHelper] Unhandled exception: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
