using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KeyInputVR.HapticFeedback
{
    [System.Serializable]
    public class HapticFeedback
    {
        [SerializeField, Range(0, 1)]
        private float _intensity = 0.5f;
        [SerializeField]
        private float _duration = 2f;

        public void TriggerFeedback(BaseInteractionEventArgs eventArgs)
        {
            if(eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
            {   
                TriggerFeedback(controllerInteractor.xrController);
            }

            if(eventArgs.interactorObject is XRPokeInteractor pokeInteractor)
            {
                XRBaseController controller = pokeInteractor.GetComponentInParent<XRBaseController>();
                if(controller != null)
                {
                    TriggerFeedback(controller);
                }
            }
        }

        public void TriggerFeedback(XRBaseController controller)
        {
            controller.SendHapticImpulse(_intensity, _duration);
        }

    }
}
