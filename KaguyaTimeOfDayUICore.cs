using UdonSharp;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VRC.SDKBase;
using VRC.Udon;
using KaguyaCode.UI;

namespace KaguyaCode.UI
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class KaguyaTimeOfDayUICore : UdonSharpBehaviour
    {
        [Header("Put Buttons Here")]
        [SerializeField] private KaguyaTimeOfDayUIButton morningButton;
        [SerializeField] private KaguyaTimeOfDayUIButton dayButton;
        [SerializeField] private KaguyaTimeOfDayUIButton sunsetButton;
        [SerializeField] private KaguyaTimeOfDayUIButton nightButton;

        // process for adding more buttons is to add them here and below at (39) for UpdateVisuals()
        // and then add the methods for selecting them below at (82)
        // default doesnt need a button but we could add it if we want
        
        [Header("Put Object with Audiosource Here")]
        [Tooltip("This is local - Plays whenever a button is selected or deselected")]
        [SerializeField] private AudioSource selectionAudio;
        
        [UdonSynced] private int selectedIndex = -1;
        private VRCPlayerApi localPlayer;

        private void Start()
        {
            localPlayer = Networking.LocalPlayer;
            UpdateVisuals();
        }

        public override void OnDeserialization()
        {
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (morningButton != null) 
                morningButton.SetSelected(selectedIndex == 0);
            if (dayButton != null) 
                dayButton.SetSelected(selectedIndex == 1);
            if (sunsetButton != null) 
                sunsetButton.SetSelected(selectedIndex == 2);
            if (nightButton != null) 
                nightButton.SetSelected(selectedIndex == 3);
        }

        public void ClearAllSelection() // this is used for the "reset" or default button
        {
            selectedIndex = -1;
            UpdateVisuals();

            if (!Networking.IsOwner(gameObject))
                Networking.SetOwner(localPlayer, gameObject);
    
             RequestSerialization();
        }

        public void SelectMorning()
        {
            SetActiveButtonTime(0);
        }
        
        public void SelectDay()
        {
            SetActiveButtonTime(1);
        }
        
        public void SelectSunset()
        {
            SetActiveButtonTime(2);
        }
        
        public void SelectNight()
        {
            SetActiveButtonTime(3);
        }

        private void SetActiveButtonTime(int index)
        {
            if (selectionAudio != null) 
                selectionAudio.Play();

            if (selectedIndex == index)
            {
                selectedIndex = -1;
                // Can put something else here if needed when deselecting
            }
            else
            {
                selectedIndex = index; // Sets new button
            }

            if (!Networking.IsOwner(gameObject))
            {
                Networking.SetOwner(localPlayer, gameObject);
            }
            
            RequestSerialization();
            UpdateVisuals();
        }
    }
}
