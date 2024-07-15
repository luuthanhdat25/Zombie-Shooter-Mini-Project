using Manager;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player")]
        [Range(0.5f,10)]
        [SerializeField]
        private float moveSpeed;

        
        [Tooltip("Acceleration and deceleration")]
        [SerializeField]
        private float speedChangeRate = 10.0f;

        private float currentSpeed;
        private CharacterController charactorController;

        public float CurrentSpeed => currentSpeed;
        public float SpeedChangeRate => speedChangeRate;

        public const float SPEED_OFFSET = 0.1F;

        private void Awake()
        {
            charactorController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 moveDirection)
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
    }
}