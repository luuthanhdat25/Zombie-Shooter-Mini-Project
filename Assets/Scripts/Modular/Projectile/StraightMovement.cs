using UnityEngine;
using AbstractClass;

namespace Projectile
{
    public class StraightMovement : AbsMovement
    {
        [SerializeField]
        private new Rigidbody rigidbody;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInParent<Rigidbody>(ref rigidbody, gameObject);
        }

        public override void Move(Vector3 moveDirection, float speed)
        {
            rigidbody.velocity = moveDirection.normalized * speed;
        }

        public override void Rotate(Vector3 rotateDirection)
        {
        }
    }
}
