using RepeatUtil;
using UnityEngine;

namespace Projectile
{
    public abstract class AbstractProjectileMovement: RepeatMonoBehaviour
    {
        public abstract void Move(Vector3 directionMove, float speed);
    }
}
