using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KeyInputVR.HapticFeedback
{
    public class ControllerFeedback : MonoBehaviour
    {
        [SerializeField]
        private XRBaseInteractable _interactable;

        [SerializeField]
        private HapticFeedback _hapticOnActivated;

        void Start()
        {
            if(_interactable != null)
            {
                _interactable.selectEntered.AddListener(_hapticOnActivated.TriggerFeedback);
            }
        }
    }
}
