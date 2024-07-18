using AbstractClass;
using Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zombie
{
    public class MeleeZombieDamageSender : AbsDamageSender
    {
        [SerializeField]
        private float attackDistance = 1.5f;

        [SerializeField]
        private float attackRadius = 1f;

        [SerializeField]
        private float capsuleHeight;

        public override List<AbsController> CheckCollision()
        {
            var colliders = GetOverlappingColliders();
            return colliders.Length <= 0 ? null : colliders.Select(collider => collider.GetComponent<AbsController>())
                                                       .Where(controller => controller != null)
                                                       .ToList();
        }

        private Collider[] GetOverlappingColliders()
        {
            int playerLayerMask = PlayerPublicInfor.Instance.PlayerLayerMarkIndex;

            Vector3 sphereCastOrigin = transform.position + new Vector3(0, capsuleHeight / 2, 0f) + transform.forward * attackDistance;
            return Physics.OverlapSphere(sphereCastOrigin, attackRadius, playerLayerMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 sphereCastOrigin = transform.position + new Vector3(0, capsuleHeight / 2, 0f) + transform.forward * attackDistance;
            Gizmos.DrawSphere(sphereCastOrigin, attackRadius);
        }
    }
}
