using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Utilities.StateMachine.States
{
    public class SpinSlotState : SlotState
    {
        private const float _rotateSpeed = 10; // degree

        private SlotPlayer _cachedSpinController;

        private float _currentSpeed = 0;
        private float _spiningTime = 0;
        private float _spiningTimeBarier = 0;

        public SpinSlotState(SlotPlayer spinController)
        {
            _cachedSpinController = spinController;
        }

        public override void Enter()
        {
            base.Enter();
            _currentSpeed = Random.Range(_rotateSpeed, _rotateSpeed * 2);
            _spiningTimeBarier = Random.Range(8f, 10f);
        }

        public override void Exit()
        {
            base.Exit();
            _spiningTime = 0;
            _currentSpeed = 0;
            _spiningTimeBarier = 0;
            EventManager.Instance.InvokeEndSpin();
        }

        public override void Update()
        {
            base.Update();

            if (_spiningTime > _spiningTimeBarier)
                _cachedSpinController.StateMachine.ChangeState(_cachedSpinController.IdleState);

            Spin();
            _spiningTime += Time.deltaTime;
        }

        private void Spin()
        {
            float rotationChanging = _currentSpeed * (1 / ( Mathf.Exp(_spiningTime)));

            _cachedSpinController.TransformToSpin.Rotate(Vector3.forward * rotationChanging);
        }
    }
}
