using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

namespace KeyInputVR.Keyboard.Filter
{
    /// <summary>
    /// Filter to only allow one interactor selecting an interactable at a time.
    /// This prevents rapid firing in case that two interactors are trying to select the same interactable.
    /// </summary>
    public class KeySelectionFilter : MonoBehaviour, IXRSelectFilter
    {
        public bool canProcess => IsActive();


        [SerializeField, Tooltip("The key that the filter may be applied to. Will search in parent if left empty.")]
        private KeyInfo _keyInfo;

        void Awake()
        {
            if(_keyInfo == null)
            {
                _keyInfo = GetComponentInParent<KeyInfo>();
                WarnIfNotFound();
            }
        }

        private void WarnIfNotFound()
        {
            if(_keyInfo == null)
            {
                Debug.LogWarning("No key reference for filter of '"+ transform.name +"' found! This might prevent key selection!", gameObject);
            }
        }

        private bool IsActive()
        {
            return isActiveAndEnabled;
        }

        public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
        {
            if(_keyInfo == null)
                return false;

            if(_keyInfo.SelectedBy == null)
            {
                return true;
            }

            if(_keyInfo.SelectedBy == interactor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
