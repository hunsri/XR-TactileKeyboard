using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Rendering;

namespace KeyInputVR.Keyboard
{
    public class KeyMarker : MonoBehaviour
    {
        [SerializeField]
        private Material _activatedMaterial;

        [SerializeField]
        private MaterialPropertyBlockHelper _materialHelper;
        
        private GameObject _regularModel;
        private GameObject _activatedModel;

        private void Start()
        {
            _regularModel = _materialHelper.rendererTarget.gameObject;
            SetupActivatedModel();
        }

        /// <summary>
        /// Creates a cloned model of the key and applies a specified material to it.
        /// This clone replaces the regular key model in the event of marking the key as active.
        /// For example the CapsLock key can be marked as active when CapsLock is active.
        /// </summary>
        private void SetupActivatedModel()
        {
            //inserting a cloned model at the same point in the hierarchy
            _activatedModel = Instantiate(_regularModel, _regularModel.GetComponentInParent<Transform>().transform.parent.transform);
            _activatedModel.GetComponent<Transform>().localScale = _regularModel.GetComponent<Transform>().localScale;
            _activatedModel.GetComponent<Transform>().localRotation = _regularModel.GetComponent<Transform>().localRotation;
            
            _activatedModel.name = "ModelActivated";
            _activatedModel.GetComponent<Renderer>().material = _activatedMaterial;
            _activatedModel.SetActive(false);
        }

        public void MarkAsActive()
        {   
            _regularModel.SetActive(false);
            _activatedModel.SetActive(true);
        }

        public void UnmarkAsActive()
        {   
            _activatedModel.SetActive(false);
            _regularModel.SetActive(true);
        }
    }
}
