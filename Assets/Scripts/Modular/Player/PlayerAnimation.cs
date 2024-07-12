using System.Net.NetworkInformation;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private enum AnimatorParameter
        {
            Speed
        }

        private int animIDSpeed;
        private Animator animator;
        private float animationBlend;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            AssignAnimationIDs();
        }

        private void AssignAnimationIDs()
        {
            animIDSpeed = Animator.StringToHash(AnimatorParameter.Speed.ToString());
        }

        private void Start()
        {
            playerMovement = PlayerController.Instance.PlayerMovement;
        }

        private void LateUpdate()
        {
            UpdateAnimationBlend();
        }

        private void UpdateAnimationBlend()
        {
            animationBlend = Mathf.Lerp(animationBlend, playerMovement.CurrentSpeed, Time.deltaTime * playerMovement.SpeedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;
            animator.SetFloat(animIDSpeed, animationBlend);
        }
    }

}
