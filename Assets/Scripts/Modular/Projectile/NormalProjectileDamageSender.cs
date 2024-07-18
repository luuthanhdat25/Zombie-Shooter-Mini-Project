using AbstractClass;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(BoxCollider))]
    public class NormalProjectileDamageSender : AbsDamageSender
    {
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
            AbsController controller = other.GetComponent<AbsController>();
            if (controller == null) return;
            CollisionWithController(controller);
            Destroy(gameObject);
        }
    }
}