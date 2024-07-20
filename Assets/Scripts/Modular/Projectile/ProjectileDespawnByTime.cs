using UnityEngine;

namespace Projectile
{
    public class ProjectileDespawnByTime : MonoBehaviour
    {
        [SerializeField]
        private float timeDespawn = 10f;

        private float timer;

        private void OnEnable() => timer = 0;

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if(timer >= timeDespawn)
            {
                ProjectilePooling.Instance.Despawn(transform);
            }
        }
    }
}
