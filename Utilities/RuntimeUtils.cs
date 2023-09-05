using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public static class RuntimeUtils
    {
        /// <summary>
        /// Asynchronous loop that runs for a specified duration, calling a step function at each step.
        /// </summary>
        /// <param name="stepFunctionCallback">Action to be called at each step of the loop.</param>
        /// <param name="OnDone">Action to be called when the loop is over.</param>
        /// <param name="duration">Duration of the loop in seconds.</param>
        /// <param name="step">step time in second to call the stepFunction</param>
        /// <param name="cancellationToken"><see cref="CancellationTokenSource"/> to stop the loop prematurely.</param>
        public async static void DoLoopAsync(Action<float> stepFunctionCallback, Action OnDone, float duration, float step, CancellationTokenSource cancellationToken)
        {
            await Task.Delay((int)(step * 1000));
            float elapsed = 0;
            while (elapsed <= duration)
            {
                if (!Application.isPlaying || cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                elapsed += step;
                float compensation = Time.time;
                await Task.Run(() => { stepFunctionCallback?.Invoke(elapsed); });
                await Task.Delay((int)(step * 1000));
            }
            OnDone?.Invoke();
        }

        static PooledMonoBehaviour monoBehaviour;
        public static void CreateUpdater()
        {
            if (!monoBehaviour)
                new GameObject("Mono", typeof(PooledMonoBehaviour));
        }
    }
}