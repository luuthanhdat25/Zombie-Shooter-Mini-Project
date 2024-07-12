using UnityEngine;
using RepeatUtil.DesignPattern.SingletonPattern;
using System;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        private InputSystemSetting inputSystemSetting;

        protected override void Awake()
        {
            base.Awake();
            this.EnableInputAction();
        }

        private void EnableInputAction()
        {
            inputSystemSetting = new InputSystemSetting();
            inputSystemSetting.Enable();
        }

        public Vector2 GetRawInputNormalized()
        {
            Vector2 inputVector = inputSystemSetting.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public bool IsShootPressed() => inputSystemSetting.Player.Shoot.IsPressed();
    }
}