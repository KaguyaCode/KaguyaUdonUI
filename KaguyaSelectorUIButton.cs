using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

namespace KaguyaCode.UI

{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class KagutaSelectorUIButton : UdonSharpBehaviour
    {
        [Header("Components")]
        [SerializeField] private RectTransform buttonTransform;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private AudioSource audioSource;
        private RectTransform iconRectTransform;
        private RectTransform textRectTransform;

        [Header("Normal State")]
        [SerializeField] private Vector2 buttonNormalSize = new Vector2(-2f, 12f);
        [SerializeField] private Color normalColor = Color.gray;
        [SerializeField] private Vector2 iconNormalPos;
        [SerializeField] private Vector2 textNormalPos;

        [Header("Selected State")]
        [SerializeField] private Vector2 buttonSelectedSize = new Vector2(0f, 14f);
        [SerializeField] private Color selectedColor = Color.white;
        [SerializeField] private Vector2 iconSelectedPos;
        [SerializeField] private Vector2 textSelectedPos;

        private void Start()
        {
            if (icon != null)
                iconRectTransform = icon.GetComponent<RectTransform>();

            if (text != null)
                textRectTransform = text.GetComponent<RectTransform>();

            SetState(false, false);
        }

        public void SetState(bool isSelected, bool playSound = true)
        {
            // Sound stuff
            if (isSelected && playSound && audioSource != null)
                audioSource.Play();

            Vector2 targetSize = isSelected ? buttonSelectedSize : buttonNormalSize;
            Color targetColor = isSelected ? selectedColor : normalColor;
            Vector2 targetIconPos = isSelected ? iconSelectedPos : iconNormalPos;
            Vector2 targetTextPos = isSelected ? textSelectedPos : textNormalPos;

            buttonTransform.sizeDelta = targetSize;

            // Icon stuff, this might not work
            if (icon != null)
            {
                icon.color = targetColor;
                if (iconRectTransform != null)
                    iconRectTransform.anchoredPosition = targetIconPos;
            }

            // Text color stuff
            if (text != null)
            {
                text.color = targetColor;
                if (textRectTransform != null)
                    textRectTransform.anchoredPosition = targetTextPos;
            }
        }
    }
}