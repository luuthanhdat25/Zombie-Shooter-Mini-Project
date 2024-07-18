using AbstractClass;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class MeleeZombieDamageSender : AbsDamageSender
    {
        [SerializeField]
        private float distanceAttack = 1.5f;

        [SerializeField]
        private float attackRadius = 1f;

        public override List<AbsController> CheckCollision()
        {
            Vector3 sphereCastOrigin = transform.position + transform.forward * distanceAttack;
            int playerLayerMask = PlayerPublicInfor.Instance.PlayerLayerMarkIndex;
            Collider[] colliders = Physics.OverlapSphere(sphereCastOrigin, attackRadius, playerLayerMask);
            if (colliders.Length <= 0) return null;
            var controllerList = new List<AbsController>();
            foreach (var collider in colliders)
            {
                var absController = collider.GetComponent<AbsController>();
                if(absController != null) controllerList.Add(absController);
            }
            return controllerList;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 sphereCastOrigin = transform.position + transform.forward * distanceAttack;
            Gizmos.DrawSphere(sphereCastOrigin, attackRadius);
        }
    }
}
