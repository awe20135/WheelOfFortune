using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Interfaces;
using WheelOfFortune.SaveSystem.Type;
using WheelOfFortune.SaveSystem;

namespace WheelOfFortune.GameEngine
{
    public class ScoreController : MonoBehaviour
    {
        private const string _totalScoreSaveLabel = "TotalScore";

        private int _totalScore;
        [SerializeField] private GameObject _wheelObject;
        [SerializeField] private GameObject[] _prizeObjects;

        private Dictionary<GameObject, IInitializator> _cachedPrizeInitializators = new Dictionary<GameObject, IInitializator>();
        private IScoreViewer _scoreViewer;
        private PlayerPrefsSaveSystemType _saveSystemType;

        private void Start()
        {
            _saveSystemType = new PlayerPrefsSaveSystemType();
            _saveSystemType.SetPlayerPrefsKey(_totalScoreSaveLabel);

            foreach (var prizeObject in _prizeObjects)
            {
                CachedInitializator(prizeObject);
            }
            _prizeObjects = null;

            _scoreViewer = GetComponent<IScoreViewer>();
            if (_scoreViewer == null)
                Debug.LogError($"Undefined IScoreView in {this}");

            object loadedScore = SaveSystem.SaveSystem.Instance.Load(_saveSystemType);

            if (loadedScore != null)
                _totalScore = (int)loadedScore;
            else
                _totalScore = 0;

            _scoreViewer.ShowScore(_totalScore);
        }

        private void OnEnable()
        {
            EventManager.Instance.OnEndSpin += GetPrize;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnEndSpin -= GetPrize;
        }

        private void OnDestroy()
        {
            _saveSystemType.SetPlayerPrefsKey(_totalScoreSaveLabel);
            SaveSystem.SaveSystem.Instance.Save(_saveSystemType, _totalScore);
        }

        private void CachedInitializator(GameObject prizeObject)
        {
            if (!_cachedPrizeInitializators.ContainsKey(prizeObject))
            {
                IInitializator initializator = prizeObject.GetComponentInChildren<IInitializator>();
                if (initializator == null)
                    Debug.LogError($"Undefined IInitializator in {prizeObject} children");
                else
                    _cachedPrizeInitializators.Add(prizeObject, initializator);
            }
        }

        private void GetPrize()
        {
            _totalScore += GetPrizeValue();
            _scoreViewer.ShowScore(_totalScore);
        }

        private int GetPrizeValue()
        {
            int prizeValue = -1;
            float currentAngle = 360-_wheelObject.transform.localEulerAngles.z;

            foreach (var cachedInitializorPair in _cachedPrizeInitializators)
            {
                float lowerBound = cachedInitializorPair.Key.transform.localEulerAngles.z;
                float upperBound = lowerBound + (360f / 16f);
                if (lowerBound <= currentAngle && currentAngle < upperBound)
                {
                    prizeValue = cachedInitializorPair.Value.GetPrizeValue();
                    break;
                }
            }

            if (prizeValue < 1000)
                Debug.LogError($"Incorect prize value: {prizeValue}");

            return prizeValue;
        }
    }
}
