using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Utilities.UI
{
    public class SwitchToggle : Selectable, IPointerClickHandler
    {
        public SwitchToggleEvent OnSwitch { get { return m_onSwitch; } private set { m_onSwitch = value; } }
        public bool isOn => _isOn;

        [System.Serializable]
        public class SwitchToggleEvent : UnityEvent<bool> { }
        [SerializeField] private bool _isOn;
        [SerializeField] private RectTransform handleRectTransform;
        private Vector2 handlePos;

        [Header("Color")]

        [SerializeField] private Color backgroundActiveColor = Color.gray;
        [SerializeField] private Color handleActiveColor = Color.white;
        [SerializeField] private Color backgroundDefaultColor = new Color(0.8f, 0.8f, 0.8f, 1), handleDefaultColor = Color.white;

        private Image backgroundImage, handleImage;

        [SerializeField, Range(.0f, 1.4f)] private float duration = .25f;

        [SerializeField] private SwitchToggleEvent m_onSwitch;

        protected override void Awake()
        {
            handlePos = handleRectTransform.anchoredPosition;
            backgroundImage = handleRectTransform.parent.GetComponent<Image>();
            handleImage = handleRectTransform.GetComponent<Image>();

            OnSwitch.AddListener(OnSwitchToggle);
            float saveDuration = duration;
            duration = 0f;
            OnSwitchToggle(isOn);
            duration = saveDuration;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _isOn = !_isOn;
            OnSwitch?.Invoke(isOn);
        }

        private void OnSwitchToggle(bool on)
        {
            //In case Do tween is not imported, replace this code

            /*
            handleRectTransform.anchoredPosition = on ? handlePos * -1 : handlePos;
            backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
            handleImage.color = on ? handleActiveColor : handleDefaultColor;
            */

            handleRectTransform.DOAnchorPos(on ? handlePos * -1 : handlePos, duration).SetEase(Ease.InOutBack);
            backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, duration);
            handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, duration);
        }
    }
}