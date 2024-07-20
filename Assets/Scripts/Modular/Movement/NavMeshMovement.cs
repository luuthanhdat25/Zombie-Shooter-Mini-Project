using AbstractClass;
using UnityEngine;
using UnityEngine.AI;

namespace Movement{
    public class NavMeshMovement : AbsMovement
    {
        [SerializeField]
        private NavMeshAgent agent;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInParent<NavMeshAgent>(ref agent, gameObject);
        }

        public override void Move(Vector3 moveDirectionOrDestination, float speed)
        {
            agent.SetDestination(moveDirectionOrDestination);
            agent.speed = speed;
        }

        public override void Rotate(Vector3 rotateDirection)
        {
            if (rotateDirection == Vector3.zero) return;
            float targetRotation = Mathf.Atan2(rotateDirection.x, rotateDirection.z) * Mathf.Rad2Deg;
            transform.parent.rotation = Quaternion.Euler(0.0f, targetRotation, 0.0f);
        }

        public override void ResetMovement()
        {
            agent.velocity = Vector3.zero;
        }
    }
}

