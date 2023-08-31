using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

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
            WaitForSecs(() =>
            {
                gameObject.SetActive(true);
            }, t, Time.deltaTime, tokenSource);
        }
        public void SetActiveFalse(float t)
        {
            WaitForSecs(() =>
            {
                gameObject.SetActive(false);
            }, t, Time.deltaTime, tokenSource);
        }


        public async void WaitForSecs(UnityAction onDone, float duration, float step, CancellationTokenSource cancellationToken)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                if (!Application.isPlaying || cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                elapsed -= step;
                await Task.Delay((int)step * 1000);
            }
            onDone?.Invoke();
        }

        private void OnDestroy()
        {
            tokenSource.Cancel();
        }
    }
}