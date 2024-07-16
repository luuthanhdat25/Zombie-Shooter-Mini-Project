using Manager;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private enum AnimatorParameter
        {
            Speed,
            Shoot
        }

        [SerializeField]
        private CharactorControllerMovement playerMovement;

        private int animIDSpeed;
        private int animIDShoot;

        private Animator animator;
        private float animationBlend;

        private bool isShootOn = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            AssignAnimationIDs();
        }

        private void AssignAnimationIDs()
        {
            animIDSpeed = Animator.StringToHash(AnimatorParameter.Speed.ToString());
            animIDShoot = Animator.StringToHash(AnimatorParameter.Shoot.ToString());
        }

        private void LateUpdate()
        {
            UpdateAnimationBlend();
            if(InputManager.Instance.IsShootPressed())
            {
                if (!isShootOn)
                {
                    animator.SetBool(animIDShoot, true);
                    isShootOn = true;
                }
            }
            else
            {
                isShootOn = false;
                animator.SetBool(animIDShoot, false);
            }
        }

        private void UpdateAnimationBlend()
        {
            //animationBlend = Mathf.Lerp(animationBlend, playerMovement.CurrentSpeed, Time.deltaTime * playerMovement.SpeedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;
            animator.SetFloat(animIDSpeed, animationBlend);
        }
    }

}
