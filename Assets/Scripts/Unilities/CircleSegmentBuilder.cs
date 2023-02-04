using UnityEngine;


namespace WheelOfFortune.Utilities
{
    public class CircleSegmentBuilder : MonoBehaviour
    {
        [SerializeField] private int _segmentCount;
        [SerializeField] private GameObject _segmentObject = null;

        public int SegmentCount { get => _segmentCount; }

        public void CircleSegmentBuild()
        { 
            for (int i = 0; i < _segmentCount; i++)
            {
                GameObject generatedObject = Instantiate(_segmentObject, transform);
                generatedObject.transform.Rotate(Vector3.forward, 360f / (float)_segmentCount * i);
            }
        }
    }
}