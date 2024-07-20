using AbstractClass;
using NPBehave;
using Player;
using UnityEngine;

namespace Zombie
{
    public class MeleeZomibieController : ZombieController
    {
        [SerializeField]
        private float delayTargetPlayer = 1f;

        [SerializeField]
        private float delayAnimationToAttack = 1f;

        private const float DELAY_CHECK_ATTACK_CONDIDION = 1F;

        public override void ActiveBehaviourTree()
        {
            base.ActiveBehaviourTree();
            MoveToAttackPosition();
        }

        protected override Root CreateBehaviourTree()
        {
            return behaviorTree = 
            new Root(
                new Repeater(
                    new Sequence(
                        new Wait(delayTargetPlayer),
                        new Action(MoveToAttackPosition),

                        new Wait(DELAY_CHECK_ATTACK_CONDIDION),

                        new Condition(CanAttack, 
                            new Sequence(
                                new Action(ActiveAttackAnimation),
                                new Wait(delayAnimationToAttack),
                                new Action(Attack)
                            )
                        )
                    )
                )
            );
        }

        private void MoveToAttackPosition()
        {
            if (AbsAnimator == null || AbsMovement == null) return;
            AbsAnimator.SetBool("Attack", false);
            AbsAnimator.SetBool("Running", true);
            AbsMovement.Move(PlayerPublicInfor.Instance.Position, AbsStat.GetMoveSpeed());
        }

        private bool CanAttack()
        {
            if (agent == null) return false;
            if (agent.pathPending) return false; 
            if (agent.remainingDistance > agent.stoppingDistance) return false; 
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f; 
        }

        private void ActiveAttackAnimation()
        {
            if (AbsAnimator == null || AbsMovement == null) return;
            AbsAnimator.SetBool("Attack", true);
            AbsAnimator.SetBool("Running", false);
            Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
            Vector3 rotateDirection = (playerPosition - transform.position).normalized;
            AbsMovement.Rotate(rotateDirection);
        }

        private void Attack()
        {
            if (AbsDamageSender == null) return;
            var controllerAttack = AbsDamageSender.CheckCollision();
            if (controllerAttack != null && controllerAttack.Count != 0)
            {
                foreach (var controller in controllerAttack)
                {
                    AbsDamageSender.CollisionWithController(controller);
                }
            }
        }
    }
}
