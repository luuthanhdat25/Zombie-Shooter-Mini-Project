using AbstractClass;
using Gun;
using NPBehave;
using Player;
using UnityEngine;

namespace Zombie
{
    public class RangeZombieController : ZombieController
    {
        [SerializeField]
        private string wallLayerMark = "Wall";

        [SerializeField]
        private float distanceAvoidPlayer = 8f;

        [SerializeField]
        private GunSelector gunSelector;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref gunSelector, gameObject);
        }

        protected override Root CreateBehaviourTree()
        {
            return behaviorTree =
            new Root(
                new Repeater(
                    new Sequence(
                        new Action(MoveToViewPlayerPosition),

                        new Condition(IsViewPlayer, 
                            new Sequence(
                                new Action(MoveToShootPosition),

                                new Condition(IsViewPlayer,
                                    new Action(Shoot)
                                )
                            )
                        )
                    )
                )
            );
        }

        private void MoveToViewPlayerPosition()
        {
            if (AbsMovement == null) return;
            EnableRunningAnimation();
            AbsMovement.Move(PlayerPublicInfor.Instance.Position, AbsStat.GetMoveSpeed());
        }

        private void EnableRunningAnimation()
        {
            if (AbsAnimator == null) return;
            AbsAnimator.SetBool("Attack", false);
            AbsAnimator.SetBool("Running", true);
        }

        private bool IsViewPlayer()
        {
            Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            return !Physics.Raycast(
                transform.position,
                direction,
                Vector3.Distance(transform.position, playerPosition),
                1 << LayerMask.NameToLayer(wallLayerMark));
        }

        private void MoveToShootPosition()
        {
            if (AbsAnimator == null || AbsMovement == null) return;
            EnableRunningAnimation();

            Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            Vector3 targetPosition = playerPosition - direction * distanceAvoidPlayer;

            AbsMovement.Move(targetPosition, AbsStat.GetMoveSpeed());
        }

        private void Shoot()
        {
            if (AbsMovement == null || gunSelector == null) return;
            Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
            Vector3 directionNormalize = (playerPosition - transform.position).normalized;
            gunSelector.UsingGun(directionNormalize, true);
            AbsMovement.Rotate(directionNormalize);
        }
    }
}
