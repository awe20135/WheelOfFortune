using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Utilities.StateMachine.States;

namespace WheelOfFortune.Utilities.StateMachine
{
    public class SlotStateMachine
    {
        public SlotState CurrentState { get; set; }

        public void Initialize(SlotState startSlotState)
        {
            CurrentState = startSlotState;
            CurrentState.Enter();
        }

        public void ChangeState(SlotState newSlotState)
        {
            CurrentState.Exit();
            CurrentState = newSlotState;
            CurrentState.Enter();
        }
    }
}
