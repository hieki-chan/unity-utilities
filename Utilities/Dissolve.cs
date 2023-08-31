using UnityEngine;

namespace Utilities
{
    public class Dissolve : MonoBehaviour
    {
        public float dissolveTime = 3f;
        float m_Timer;

        void OnEnable()
        {
            m_Timer = 0;
        }

        void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= dissolveTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}