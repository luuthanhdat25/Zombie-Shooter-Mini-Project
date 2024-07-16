using AbstractClass;
using Manager;
using RepeatUtil.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Player
{
    public class PlayerController : AbsController
    {
        [Space]
        [Header("Player Controller")]
        [SerializeField]
        private PlayerSO playerSO;

        [SerializeField]
        private GunSelector gunSelector;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent<GunSelector>(ref gunSelector, gameObject);
        }


        private void FixedUpdate()
        {
            Vector3 moveDirection = GetMoveDirectionFromInput();
            absMovement.Move(moveDirection, playerSO.MoveSpeed);

            if (InputManager.Instance.IsShootPressed())
            {
                Vector3 shootDireciton = GetRotateDirectionFromMouse();
                absMovement.Rotate(shootDireciton);
                gunSelector.UsingGun(shootDireciton);
            }
            else
            {
                gunSelector.UsingGun(Vector3.zero);
                absMovement.Rotate(moveDirection);
            }
        }

        private Vector3 GetMoveDirectionFromInput()
        {
            Vector2 inputMoveVector2 = InputManager.Instance.GetRawInputNormalized();
            Vector3 moveDirectionVector3 = GetMoveDirectionVector3(inputMoveVector2);
            return GetIsometricVectorFromNormalVector(moveDirectionVector3);
        }

        private Vector3 GetMoveDirectionVector3(Vector2 vector2)
        {
            return new Vector3(vector2.x, 0.0f, vector2.y);
        }

        private Vector3 GetIsometricVectorFromNormalVector(Vector3 normal)
        {
            return Quaternion.Euler(0, 45, 0) * normal;
        }

        private Vector3 GetRotateDirectionFromMouse()
        {
            Vector2 mouseDirection = CameraManager.Instance.GetNormalizedMouseDirectionToScreenCenter();
            Vector3 directionVector3 = GetMoveDirectionVector3(mouseDirection);
            return GetIsometricVectorFromNormalVector(directionVector3);
        }
    }
}
