using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Interfaces;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.GameEngine 
{
    public class SegmentInitializator : MonoBehaviour, IInitializator
    {
        private static readonly List<int> _segmentsPrize = new List<int>();

        [SerializeField] private GameObject _slotSegmentGameObject;

        public int SegmentPrize { get => _segmentPrize; }
        private int _segmentPrize = 0;

        private void Start()
        {
            if (_slotSegmentGameObject == null)
            {
                Debug.LogError("Undefined GameObject with ISlotSegment component");
                return;
            }

            SegmentInitialize();
        }

        private void OnDestroy()
        {
            if(_segmentsPrize.Count != 0)
                _segmentsPrize.Clear();
        }

        private void SegmentInitialize()
        {
            ISlotSegment<int> _slotSegment = _slotSegmentGameObject.GetComponent<ISlotSegment<int>>();
            _segmentPrize = GeneratePrize();          
            _slotSegment.InitializeSlotSegment(_segmentPrize);
        }

        // 10 -> 1000, multipler = 100
        private int GeneratePrize()
        {
            int generatedPrize = 0;
            while (generatedPrize == 0)
            {
                generatedPrize = Random.Range(10, 1001) * 100;

                for (int prizeIndex = 0; prizeIndex < _segmentsPrize.Count; prizeIndex++)
                {
                    if(_segmentsPrize[prizeIndex] - 1000 < generatedPrize  && generatedPrize < _segmentsPrize[prizeIndex] + 1000)
                    {
                        generatedPrize = 0;
                        break;
                    }
                }
            }

            _segmentsPrize.Add(generatedPrize);
            return generatedPrize;
        }

        public int GetPrizeValue()
        {
            return _segmentPrize;
        }

        public GameObject GetInitObject()
        {
            return _slotSegmentGameObject;
        }
    }
}
