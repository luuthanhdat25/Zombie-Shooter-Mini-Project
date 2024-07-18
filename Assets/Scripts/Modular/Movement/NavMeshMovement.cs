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
            return;
        }
    }
}

