using AbstractClass;
using Player;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(BoxCollider))]
    public class AOEProjectileDamageSender : AbsDamageSender
    {
        [SerializeField]
        private GameObject explosionEffect;

        [SerializeField]
        private float radiusExplosion = 4f;

        [SerializeField]
        private BoxCollider boxCollider;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent<BoxCollider>(ref boxCollider, gameObject);
            boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplosion);
            if (colliders.Length <= 0) return;

            foreach (Collider objCollider in colliders)
            {
                if (objCollider.TryGetComponent<AbsController>(out AbsController absController))
                {
                    if(absController is not PlayerController)
                    {
                        CollisionWithController(absController);
                    }
                }
            }

            ProjectilePooling.Instance.Despawn(transform);
        }
    }
}
