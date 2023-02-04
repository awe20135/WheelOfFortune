using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;

namespace WheelOfFortune.Utilities
{
    public class ButtonActivityController : MonoBehaviour
    {
        Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            if (!_button)
                Debug.LogError($"Unefined button component on {_button}");
        }

        private void OnEnable()
        {
            EventManager.Instance.OnStartSpin += ButtonTurnOff;
            EventManager.Instance.OnEndSpin += ButtonturnOn;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnStartSpin -= ButtonTurnOff;
            EventManager.Instance.OnEndSpin -= ButtonturnOn;
        }

        public void OnButtonClick()
        {
            EventManager.Instance.InvokeStartSpin();
        }

        private void ButtonTurnOff()
        {
            if (_button)
                _button.interactable = false;
        }

        private void ButtonturnOn()
        {
            if (_button)
                _button.interactable = true;
        }
    }
}
