using RepeatUtil;
using ScriptableObjects;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsShoot : RepeatMonoBehaviour
    {
        protected float firingTimer;
        protected GunSO currentGunSO;
        protected int currentProjectileLayerMark;

        /// <summary>
        /// Call when hold Shoot
        /// </summary>
        /// <param name="initalDirection">Start direction move</param>
        /// <param name="initalPosition">Start position</param>
        /// <param name="numberOfBullet">Number of bullets shoot when shoot</param>
        /// <returns>Return true when shoot successfull</returns>
        public abstract bool ShootHold(Vector3 initalDirection, Vector3 initalPosition, int numberOfBullet);

        /// <summary>
        /// Call when release Shoot
        /// </summary>
        /// <param name="releasePosition">Position in world when release shoot</param>
        /// <param name="initalPosition">Start position</param>
        /// <param name="numberOfBullet">Number of bullets shoot when shoot</param>
        /// <returns>Return true when shoot successfull</returns>
        public abstract bool ShootRelease(Vector3 releasePosition, Vector3 initalPosition, int numberOfBullet);

        public virtual void ResetShootValue(GunSO newGunSO, int projectileLayerMarkIndex)
        {
            currentGunSO = newGunSO;
            currentProjectileLayerMark = projectileLayerMarkIndex;
        }

        protected float FireRateToTimeDelayShoot(float fireRate) => 1f / fireRate;
    }
}
