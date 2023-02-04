using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Utilities.StateMachine.States
{
    /// <summary>
    /// Slot state class by state machine pattern
    /// </summary>
    public abstract class SlotState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
