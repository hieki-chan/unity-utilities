using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    [DisallowMultipleComponent]
    public class Swiper : UIBehaviour
    {
        public bool AllowLoop { get { return m_AllowLoop; } set { m_AllowLoop = value; } }

        public Button leftButton;
        public Button rightButton;

        public GameObject[] m_Pages;

        [SerializeField] private bool m_AllowLoop;
        int value;

        protected override void Start()
        {
            base.Start();
            leftButton.onClick.AddListener(MoveLeft);
            rightButton.onClick.AddListener(MoveRight);
            SetActivePage();
        }

        public void MoveLeft()
        {
            if (value == 0)
                if (m_AllowLoop)
                    value = m_Pages.Length - 1;
                else
                    return;
            else
                value--;

            SetActivePage();
        }

        public void MoveRight()
        {
            if (value == m_Pages.Length - 1)
                if (m_AllowLoop)
                    value = 0;
                else
                    return;
            else
                value++;

            SetActivePage();
        }

        void SetActivePage()
        {
            for (int i = 0; i < m_Pages.Length; i++)
            {
                if (i == value)
                    m_Pages[i].SetActive(true);
                else
                    m_Pages[i].SetActive(false);
            }
        }
    }
}