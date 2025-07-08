using UdonSharp;
using UnityEngine;
using TMPro;

namespace KaguyaCode.UI
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class KaguyaTimeOfDayUIButton : UdonSharpBehaviour
    // this script is used for the buttons in the Kaguya Time of Day UI
    // put this script on the button GameObjects
    {
        // UI Components
        [Header("UI Components")]
        [Tooltip("Drag the text object here")]
        [SerializeField] private TextMeshProUGUI buttonText;
        private RectTransform textRectTransform;
        
        // Default Colors and Scaling
        [Header("Colors and Scaling")]
        [Tooltip("Color when the button is not selected")]
        [SerializeField] private Color normalColor = Color.gray;
        [Tooltip("Color when the button is selected")]
        [SerializeField] private Color selectedColor = Color.white;
        [Tooltip("Size increase for button when selected")]
        [SerializeField] private float selectedSize = 1.05f;
        private Vector3 originalScale;

        private void Start()
        {
            if (buttonText != null)
            {
                textRectTransform = buttonText.GetComponent<RectTransform>();
                if (textRectTransform != null)
                {
                    // uses original scale as reference
                    originalScale = textRectTransform.localScale;
                }
            }
            SetSelected(false);
        }

        public void SetSelected(bool isSelected)
        {
            if (buttonText != null)
            {
                // color
                if (isSelected)
                {
                    buttonText.color = selectedColor;
                }
                else
                {
                    buttonText.color = normalColor;
                }
                
                // scaling
                if (textRectTransform != null)
                {
                    if (isSelected)
                    {
                        textRectTransform.localScale = originalScale * selectedSize;
                    }
                    else
                    {
                        textRectTransform.localScale = originalScale;
                    }
                }
            }
        }
    }
}
