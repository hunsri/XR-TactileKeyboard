using System;
using KeyInputVR.KeyMaps;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

namespace KeyInputVR.Keyboard
{
    public class KeyInfo : MonoBehaviour
    {
        private IKeyMap _keyMap;

        [SerializeField]
        private Key _key;

        [SerializeField]
        private KeyLabel _keyLabel;

        [SerializeField]
        private KeyMarker _keyMarker;

        private bool _isShifted;

        public IXRSelectInteractor SelectedBy {get; private set; } = null;

        public event Action<IKeyMap, Key> OnKeyActivated = delegate { };

        // Start is called before the first frame update
        void Start()
        {
            if(_key == Key.None)
            {
                Debug.LogWarning("Key type of '"+ transform.name +"' isn't set!", gameObject);
            }

            if(_keyMap == null)
            {
                Debug.LogWarning("KeyMap of '"+ transform.name +"' isn't set!", gameObject);
            }
        }

        private void OnValidate()
        {
            UpdateLabel();
        }

        public Key GetKey()
        {
            return _key;
        }

        public void ActivateKey()
        {
            OnKeyActivated(_keyMap, _key);
        }

        public void EnterSelection(SelectEnterEventArgs eventArgs)
        {
            if(SelectedBy == null)
            {
                SelectedBy = eventArgs.interactorObject;
            }

            ActivateKey();
        }

        public void ExitSelection(SelectExitEventArgs eventArgs)
        {
            SelectedBy = null;
        }

        public void SetKeyMap(IKeyMap keyMap)
        {
            _keyMap = keyMap;
        }

        public void SetShiftState(bool shift)
        {   
            _isShifted = shift;
            UpdateLabel();
        }

        public void LockAppearanceToActive()
        {
            _keyMarker.MarkAsActive();
        }

        public void ReleaseFromActiveAppearance()
        {
            _keyMarker.UnmarkAsActive();
        }

        private void UpdateLabel()
        {
            if(_keyLabel != null && _keyMap != null)
            {   
                _keyLabel.UpdateLabel(_keyMap, _key, _isShifted);
            }
        }

        public void SetFeedbackSoundState(bool enabled)
        {
            SelectEnterEvent selectEnterEvent = transform.GetComponentInChildren<XRBaseInteractable>().selectEntered;
            SelectExitEvent selectExitEvent = transform.GetComponentInChildren<XRBaseInteractable>().selectExited;
            SelectEnterEvent firstSelectEnterEvent = transform.GetComponentInChildren<XRBaseInteractable>().firstSelectEntered;
            SelectExitEvent lastSelectExitEvent = transform.GetComponentInChildren<XRBaseInteractable>().lastSelectExited;

            SoundOnEvent(selectEnterEvent, enabled);
            SoundOnEvent(selectExitEvent, enabled);
            SoundOnEvent(firstSelectEnterEvent, enabled);
            SoundOnEvent(lastSelectExitEvent, enabled);
        }

        private void SoundOnEvent(UnityEventBase unityEvent, bool enabled)
        {
            for(int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                try
                {
                    AudioSource audio = (AudioSource) unityEvent.GetPersistentTarget(i);
                    audio.mute = !enabled;
                }
                catch
                {

                }
            }
        }
    }
}
