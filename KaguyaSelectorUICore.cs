using UdonSharp;
using UnityEngine;

namespace KaguyaCode.UI
{

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class KaguyaSelectorUICore : UdonSharpBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] private KagutaSelectorUIButton morningButton;
        [SerializeField] private KagutaSelectorUIButton dayButton;
        [SerializeField] private KagutaSelectorUIButton sunsetButton;
        [SerializeField] private KagutaSelectorUIButton nightButton;

        // no default button, since its not supposed to be selected
        
        [Header("Selector")]
        [SerializeField] private RectTransform selectorTransform;
        [SerializeField] private Transform defaultPosition;
        
        [Header("Animation")]
        [SerializeField] private float moveDuration = 0.3f;
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private AudioSource clickSound;
        
        private bool isAnimating;
        private float animationTimer;
        private Vector3 animationStartPos;
        private Vector3 animationTargetPos;
        
        private KagutaSelectorUIButton currentButton;
        private bool isDefaultState = true; // i dont remember what this is for

        private void Start()
        {
            selectorTransform.position = defaultPosition.position;
        }

        public void ToggleMorning() => ToggleButton(morningButton);
        public void ToggleDay() => ToggleButton(dayButton);
        public void ToggleSunset() => ToggleButton(sunsetButton);
        public void ToggleNight() => ToggleButton(nightButton);

        private void ToggleButton(KagutaSelectorUIButton button) // sound
        {
            if (clickSound) clickSound.Play();
            
            if (currentButton == button)
            {
                ReturnToDefault();
                return;
            }
            
            if (currentButton != null)
                currentButton.SetState(false, false);
            
            button.SetState(true, false);
            currentButton = button;
            isDefaultState = false;
            
            StartSelectorAnimation(button.transform.position);
        }

        private void ReturnToDefault()
        {
            if (currentButton != null)
                currentButton.SetState(false, false);
            
            currentButton = null;
            isDefaultState = true;
            StartSelectorAnimation(defaultPosition.position);
        }

        private void StartSelectorAnimation(Vector3 targetPos) //this just isnt working????
        {
            animationStartPos = selectorTransform.position;
            animationTargetPos = targetPos;
            animationTimer = 0f;
            isAnimating = true;
        }

        private void Update()
        {
            if (!isAnimating) return;
            
            animationTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(animationTimer / moveDuration);
            float curveProgress = animationCurve.Evaluate(progress);
            
            selectorTransform.position = Vector3.Lerp(
                animationStartPos, 
                animationTargetPos, 
                curveProgress
            );
            
            if (progress >= 1f)
            {
                isAnimating = false;
            }
        }
    }
}