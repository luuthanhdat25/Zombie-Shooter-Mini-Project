using UnityEngine;
using RepeatUtil.DesignPattern.SingletonPattern;
using System;
using UnityEngine.InputSystem;

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

        /// <summary>
        /// The current Pointer coordinates in window space
        /// </summary>
        /// <returns>Vector 2 of Position</returns>
        public Vector2 GetMousePositionInScreen() => Mouse.current.position.ReadValue();
    }
}