using UnityEngine;

namespace WheelOfFortune.Interfaces
{
    public interface IInitializator
    {
        public int GetPrizeValue();

        public GameObject GetInitObject();
    }
}
