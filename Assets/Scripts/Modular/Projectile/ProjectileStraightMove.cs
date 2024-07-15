using UnityEngine;

namespace Projectile
{
    [RequireComponent (typeof(Rigidbody))]
    public class ProjectileStraightMove : AbstractProjectileMovement
    {
        [SerializeField]
        private Rigidbody rigidbody;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            if (rigidbody != null) return;
            rigidbody = GetComponent<Rigidbody>();
        }

        public override void Move(Vector3 directionMove, float speed)
        {
            rigidbody.velocity = directionMove.normalized * speed;
        }
    }
}
