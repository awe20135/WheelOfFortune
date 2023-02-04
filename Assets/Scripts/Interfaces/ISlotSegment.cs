using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Interfaces
{
    /// <summary>
    /// Slot segment interface
    /// </summary>
    /// <typeparam name="T">Value to initialization the slot segment</typeparam>
    public interface ISlotSegment<T>
    {
        /// <summary>
        /// Initialize slot segment on the other script 
        /// </summary>
        /// <param name="initValue">Value to initialize</param>
        public void InitializeSlotSegment(T initValue);
    }
}
