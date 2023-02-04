using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Events
{
    public class EventManager
    {
        // Singleton init
        private static readonly Lazy<EventManager> _instance = new Lazy<EventManager>(() => new EventManager());
        private EventManager() { }
        public static EventManager Instance { get { return _instance.Value; } }

        // Events init
        public delegate void SpinDelegate();

        public event SpinDelegate OnStartSpin;
        public event SpinDelegate OnEndSpin;

        // Events invoke
        public void InvokeStartSpin()
        {
            OnStartSpin?.Invoke();
        }

        public void InvokeEndSpin()
        {
            OnEndSpin?.Invoke();
        }
    }
}
