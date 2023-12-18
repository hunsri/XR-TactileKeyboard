using UnityEngine;
using UnityEngine.InputSystem;

namespace KeyInputVR.KeyMaps
{
    public class KeyDefinition
    {
        public KeyType KeyType {get;}
        public string BaseOutput {get;}
        public string ShiftedOutput {get;}

        public string BaseLabel {get;}
        public string ShiftedLabel {get;}

        public KeyDefinition(Key key, string label)
        {
            KeyType = key.ToKeyType();
            BaseLabel = label;
            ShiftedLabel = label;
        }

        public KeyDefinition(string baseOutput, string shiftedOutput)
        {
            KeyType = KeyType.REGULAR;
            BaseOutput = baseOutput;
            ShiftedOutput = shiftedOutput;
            BaseLabel = baseOutput;
            ShiftedLabel = shiftedOutput;
        }
    }
}
