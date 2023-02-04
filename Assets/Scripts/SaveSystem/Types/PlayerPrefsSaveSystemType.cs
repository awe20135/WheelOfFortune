using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.SaveSystem.Type
{
    public class PlayerPrefsSaveSystemType : ISaveSystemType
    {
        private string _playerPrefsKey;

        public void SetPlayerPrefsKey(string playerPrefsKey)
        {
            _playerPrefsKey = playerPrefsKey;
        }

        public object Load()
        {
            object result = null;

            float floatResult = PlayerPrefs.GetFloat(_playerPrefsKey);
            int intResult = PlayerPrefs.GetInt(_playerPrefsKey);
            string stringResult = PlayerPrefs.GetString(_playerPrefsKey);

            if (floatResult != default)
                result = floatResult;

            else if (intResult != default)
                result = intResult;

            else if (stringResult != "")
                result = stringResult;

            else
                Debug.LogWarning($"Undefined PlayerPrefs data to load with key: {_playerPrefsKey}");

            return result;
        }

        public void Save(object objectToSave)
        {
            System.Type typeOfObject = objectToSave.GetType();

            if (typeOfObject.Equals(typeof(float)))
                PlayerPrefs.SetFloat(_playerPrefsKey, (float)objectToSave);

            else if (typeOfObject.Equals(typeof(int)))
                PlayerPrefs.SetInt(_playerPrefsKey, (int)objectToSave);

            else if (typeOfObject.Equals(typeof(string)))
                PlayerPrefs.SetString(_playerPrefsKey, (string)objectToSave);

            else
            {
                Debug.LogWarning($"Object hasn`t been save. Undefined Type of objectToSave to use PlayerPrefs save type");
                return;
            }

            PlayerPrefs.Save();
        }
    }
}
