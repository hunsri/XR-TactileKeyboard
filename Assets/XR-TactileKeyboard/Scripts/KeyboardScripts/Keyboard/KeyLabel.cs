using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using KeyInputVR.KeyMaps;
using System.Collections.Generic;

namespace KeyInputVR.Keyboard
{
    public class KeyLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        internal void UpdateLabel(IKeyMap keyMap, Key key, bool shifted)
        {   
            KeyDefinition keyDefinition = keyMap.GetKeyMap()[key];

            _label.text = shifted ? keyDefinition.ShiftedLabel : keyDefinition.BaseLabel;
        }

    }
}
