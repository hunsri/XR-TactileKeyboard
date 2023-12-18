using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace KeyInputVR.ControllerInput
{
    public enum HandType
    {
        Left,
        Right
    }

    public class XRHandController : MonoBehaviour
    {
        [SerializeField]
        private HandType _handType;

        private Animator _animator;
        private InputDevice _inputDevice;

        private bool _inputFound = false;

        private float _pointingValue;

        [SerializeField]
        private GameObject _littleInteractor;
        [SerializeField]
        private GameObject _ringInteractor;
        [SerializeField]
        private GameObject _middleInteractor;
        [SerializeField]
        private GameObject _indexInteractor;
        [SerializeField]
        private GameObject _thumbInteractor;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            if(!_inputFound)
            {
                //doing this here instead, since in Start it sometimes won't register correctly
                _inputDevice = GetInputDevice();
                _inputFound = true;
            }

            AnimateHand();
            ManageFingerColliders();
        }

        private InputDevice GetInputDevice()
        {
            InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

            if(_handType == HandType.Left)
            {
                characteristics = characteristics | InputDeviceCharacteristics.Left;
            }
            else
            {
                characteristics = characteristics | InputDeviceCharacteristics.Right;
            }

            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(characteristics, inputDevices);

            return inputDevices[0];
        }

        private void AnimateHand()
        {
            _inputDevice.TryGetFeatureValue(CommonUsages.grip, out _pointingValue);

            _animator.SetFloat("Pointing", _pointingValue);
        }

        private void ManageFingerColliders()
        {
            if(_pointingValue > 0.1f)
            {
                DisableInteractorsForPointing();
            }
            else
            {
                EnableInteractorsAfterPointing();
            }
        }

        private void DisableInteractorsForPointing()
        {
            _littleInteractor.SetActive(false);
            _ringInteractor.SetActive(false);
            _middleInteractor.SetActive(false);
            _thumbInteractor.SetActive(false);
        }

        private void EnableInteractorsAfterPointing()
        {
            _littleInteractor.SetActive(true);
            _ringInteractor.SetActive(true);
            _middleInteractor.SetActive(true);
            _thumbInteractor.SetActive(true);
        }
    }
}