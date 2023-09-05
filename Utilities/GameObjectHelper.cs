using System;
using System.Threading;
using UnityEngine;

namespace Utilities
{
    public class GameObjectHelper : MonoBehaviour
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public void InvertActive(float t)
        {
            if (gameObject.activeSelf)
                SetActiveFalse(t);
            else
                SetActiveTrue(t);
        }

        public void SetActiveTrue(float t)
        {
            WaitForSecs(() => gameObject.SetActive(true), t);
        }

        public void SetActiveFalse(float t)
        {
            WaitForSecs(() => gameObject.SetActive(false), t);
        }

        public void WaitForSecs(Action OnDone, float delay)
        {
            RuntimeUtils.DoLoopAsync(null, OnDone, delay, Time.deltaTime, tokenSource);
        }

        private void OnDestroy()
        {
            tokenSource.Cancel();
        }
    }
}