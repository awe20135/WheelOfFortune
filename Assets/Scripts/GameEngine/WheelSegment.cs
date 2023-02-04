using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.GameEngine
{
    public class WheelSegment : MonoBehaviour, ISlotSegment<int>
    {
        private Text _text;

        void Start()
        {
            _text = GetComponent<Text>();
            if (!_text)
            {
                Debug.LogError($"Undefined Text component on {gameObject}");
            }
        }

        public void InitializeSlotSegment(int initValue)
        {
            if(!_text)
                _text = GetComponent<Text>();

            _text.text = initValue.ToString();
        }
    }
}
