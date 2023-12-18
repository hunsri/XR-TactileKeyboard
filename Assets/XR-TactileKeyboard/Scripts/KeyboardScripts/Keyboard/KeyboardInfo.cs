using System.Collections.Generic;
using KeyInputVR.KeyMaps;
using UnityEngine;

namespace KeyInputVR.Keyboard
{
    public class KeyboardInfo : MonoBehaviour
    {
        private static readonly KeyMappingType s_fallbackMapping = KeyMappingType.GERMAN_QWERTZ;

        [SerializeField]
        private KeyMappingType _selectedMapping;

        private IKeyMap _keyMap;

        private bool _isShifted;
        private bool _isCapsLocked;

        [SerializeField]
        private bool _isSwitchSoundFeedbackEnabled = true;

        public bool IsShifted {get {return _isShifted;} set {SetShiftState(value);}}
        public bool IsCapsLocked {get {return _isCapsLocked;} set{SetCapsLockState(value);}}
        public bool IsSoundFeedbackEnabled {get {return _isSwitchSoundFeedbackEnabled;} set{SetSoundFeedbackEnabled(value);}}

        // Logical XOR
        // Returns true if Shifted OR CapsLocked, but not if both or neither are true
        public bool IsSetToUppercase {get {return IsShifted ^ IsCapsLocked;}}

        [SerializeField]
        public List<KeyInfo> KeyInfos = new List<KeyInfo>();
        
        public List<KeyInfo> RegularKeys {get;} = new List<KeyInfo>();

        public List<KeyInfo> ShiftKeys {get;} = new List<KeyInfo>();

        public List<KeyInfo> CapsKeys {get;} = new List<KeyInfo>();

        private void Awake()
        {
            ApplyMapping(_selectedMapping);

            foreach(KeyInfo info in KeyInfos)
            {   
                info.SetKeyMap(_keyMap);

                KeyType keyType = _keyMap.GetKeyMap()[info.GetKey()].KeyType;

                switch(keyType)
                {
                    case KeyType.REGULAR:
                        RegularKeys.Add(info);
                        break;
                    case KeyType.SHIFT:
                        ShiftKeys.Add(info);
                        break;
                    case KeyType.CAPSLOCK:
                        CapsKeys.Add(info);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Start()
        {
            ApplySoundStateToSwitches();
        }

        private void OnValidate()
        {
            ApplyMapping(_selectedMapping);

            ApplySoundStateToSwitches();
        }

        private void ApplyMapping(KeyMappingType mapping)
        {
            if(_selectedMapping == KeyMappingType.NONE)
            {
                _selectedMapping = s_fallbackMapping;
                Debug.LogWarning("No keyboard mapping selected for '"+ transform.name +"!' Using fallback mapping '"+ _selectedMapping.ToString() +"'", gameObject);
            }
            switch(_selectedMapping)
            {
                case KeyMappingType.GERMAN_QWERTZ:
                    _keyMap = new GermanKeyMap();
                    break;
                default:
                    break;
            }
        }

        private void SetShiftState(bool shifted)
        {
            _isShifted = shifted;
            
            ApplyStateChangesToLabels();
        }

        public void SetCapsLockState(bool capsLocked)
        {
            _isCapsLocked = capsLocked;

            // Note that this implementation follows a ShiftLock rather than a CapsLock
            ApplyStateChangesToLabels();
        }

        private void SetSoundFeedbackEnabled(bool enabled)
        {
            _isSwitchSoundFeedbackEnabled = enabled;

            ApplySoundStateToSwitches();
        }

        public void DisplayShiftKeysAsActive(bool active)
        {
            foreach(KeyInfo infos in ShiftKeys)
            {
                if(active)
                    infos.LockAppearanceToActive();
                else
                    infos.ReleaseFromActiveAppearance();    
            }
        }

        public void DisplayCapsLockKeysAsActive(bool active)
        {
            foreach(KeyInfo infos in CapsKeys)
            {
                if(active)
                    infos.LockAppearanceToActive();
                else
                    infos.ReleaseFromActiveAppearance();   
            }
        }

        private void ApplyStateChangesToLabels()
        {
            foreach(KeyInfo info in RegularKeys)
            {
                info.SetShiftState(IsSetToUppercase);
            }
        }

        private void ApplySoundStateToSwitches()
        {
            foreach(KeyInfo info in RegularKeys)
            {
                info.SetFeedbackSoundState(IsSoundFeedbackEnabled);
            }
        }
    }
}
