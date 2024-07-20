using UnityEngine;
using RepeatUtil.DesignPattern.SingletonPattern;
using System;
using UnityEngine.InputSystem;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public Action OnSwitchGun;
        public Action OnReloadGun;

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

        private void Start()
        {
            inputSystemSetting.Player.SwitchGun.performed += (InputAction.CallbackContext context) => OnSwitchGun?.Invoke();
            inputSystemSetting.Player.ReloadGun.performed += (InputAction.CallbackContext context) => OnReloadGun?.Invoke();
            inputSystemSetting.Player.Escape.performed += (InputAction.CallbackContext context) => GameManager.Instance.TogglePauseGame();
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