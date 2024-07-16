using AbstractClass;
using Manager;
using UnityEngine;

namespace Player
{
    public class KeyBoardMouseMovement : AbsMovement
    {
        [SerializeField]
        private CharacterController charactorController;
        
        [Tooltip("Acceleration and deceleration")]
        [SerializeField]
        [Range(0.5f, 100f)]
        private float speedChangeRate = 10.0f;

        [Header("Rotation")]
        [Range(0.0f, 0.3f)]
        [SerializeField]
        private float rotationSmoothTime = 0.12f;

        private float rotationVelocity;
        private float targetRotation;
        private float currentSpeed;

        public float CurrentSpeed => currentSpeed;
        public float SpeedChangeRate => speedChangeRate;

        public const float SPEED_OFFSET = 0.1F;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInParent<CharacterController>(ref charactorController, gameObject);
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = GetMoveDirectionFromInput();
            Move(moveDirection);

            if (InputManager.Instance.IsShootPressed())
            {
                Vector3 shootDireciton = GetRotateDirectionFromMouse();
                Rotate(shootDireciton);
                //playerShootShoot(shootDireciton);
            }
            else
            {
                //playerShoot.Shoot(Vector3.zero);
                Rotate(moveDirection);
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

        protected override void Move(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero) currentSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(charactorController.velocity.x, 0.0f, charactorController.velocity.z).magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < moveSpeed - SPEED_OFFSET ||
                currentHorizontalSpeed > moveSpeed + SPEED_OFFSET)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                currentSpeed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed,
                    Time.deltaTime * speedChangeRate);

                // round speed to 3 decimal places
                currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;
            }
            else
            {
                currentSpeed = moveSpeed;
            }

            charactorController.Move(moveDirection * (currentSpeed * Time.deltaTime));
        }

        protected override void Rotate(Vector3 rotateDirection)
        {
            if (rotateDirection != Vector3.zero)
            {
                targetRotation = Mathf.Atan2(rotateDirection.x, rotateDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    rotationSmoothTime);

                transform.parent.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }
    }
}