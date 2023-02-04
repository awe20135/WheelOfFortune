using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Utilities.StateMachine.States;

namespace WheelOfFortune.Utilities.StateMachine
{
    public class SlotPlayer : MonoBehaviour
    {
        private Transform _transformToSpin;
        private SlotStateMachine _stateMachine;
        private IdleSlotState _idleState;
        private SpinSlotState _spinState;

        public Transform TransformToSpin { get => _transformToSpin; }
        public SlotStateMachine StateMachine { get => _stateMachine; }
        public IdleSlotState IdleState { get => _idleState; }
        public SpinSlotState SpinState { get => _spinState; }

        private void Start()
        {
            _transformToSpin = GetComponent<Transform>();

            if (!_transformToSpin)
            {
                Debug.LogError($"Undefined Transform component in {this}");
                return;
            }

            _stateMachine = new SlotStateMachine();
            _idleState = new IdleSlotState();
            _spinState = new SpinSlotState(this);

            _stateMachine.Initialize(_idleState);
        }

        private void Update()
        {
            if (_stateMachine.CurrentState == _idleState)
                return;

            _stateMachine.CurrentState.Update();
        }

        private void OnEnable()
        {
            EventManager.Instance.OnStartSpin += StartSpin;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnStartSpin -= StartSpin;
        }

        public void StartSpin()
        {
            _stateMachine.ChangeState(_spinState);
        }
    }
}
