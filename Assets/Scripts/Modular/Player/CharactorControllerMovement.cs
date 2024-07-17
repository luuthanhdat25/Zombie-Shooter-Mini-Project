using AbstractClass;
using Manager;
using UnityEngine;

namespace Player
{
    public class CharactorControllerMovement : AbsMovement
    {
        [SerializeField]
        private CharacterController charactorController;
        
        [Tooltip("Acceleration and deceleration")]
        [SerializeField]
        [Range(0.5f, 100f)]
        private float speedChangeRate = 10.0f;

        [Header("Rotation")]
        [Range(0.0f, 3f)]
        [SerializeField]
        private float rotationSmoothTime = 0.12f;

        private float rotationVelocity;
        private float targetRotation;
        private float currentSpeed;

        public float SpeedChangeRate => speedChangeRate;

        public const float SPEED_OFFSET = 0.1F;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInParent<CharacterController>(ref charactorController, gameObject);
        }

        public override void Move(Vector3 moveDirection, float moveSpeed)
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

        public override void Rotate(Vector3 rotateDirection)
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