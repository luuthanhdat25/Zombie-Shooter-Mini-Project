using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [Range(0.0f, 0.3f)]
        [SerializeField]
        private float rotationSmoothTime;
        
        private float targetRotation;
        private float rotationVelocity;

        public void Rotate(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
            {
                targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    rotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }
    }
}
