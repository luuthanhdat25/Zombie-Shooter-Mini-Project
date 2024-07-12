using Manager;
using RepeatUtil.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField]
        private PlayerMovement playerMovement;

        [SerializeField]
        private PlayerRotation playerRotation;

        private Quaternion isometricQuaternion;

        public PlayerMovement PlayerMovement => playerMovement;

        protected override void Awake()
        {
            base.Awake();
            isometricQuaternion = Quaternion.Euler(0, 45, 0);
        }

        private void Update()
        {
            Vector3 moveDirection = GetMoveDirection();
            playerMovement.Move(moveDirection);
            playerRotation.Rotate(moveDirection);
        }

        private Vector3 GetMoveDirection()
        {
            Vector2 inputMoveVector2 = InputManager.Instance.GetRawInputNormalized();
            Vector3 moveDirectionNormal = new Vector3(inputMoveVector2.x, 0.0f, inputMoveVector2.y);
            Vector3 moveDirectionIsometric = isometricQuaternion * moveDirectionNormal;
            return moveDirectionIsometric;
        }
    }
}
