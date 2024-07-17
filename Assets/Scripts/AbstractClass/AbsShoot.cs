using RepeatUtil;
using ScriptableObjects;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsShoot : RepeatMonoBehaviour
    {
        protected float firingTimer;
        protected GunSO currentGunSO;

        public virtual void ShootHold(Vector3 initalDirection, Vector3 initalPosition)
        {
            //For override
        }

        public virtual void ShootRelease(Vector3 releasePosition, Vector3 initalPosition)
        {
            //For override
        }

        public virtual void ResetShootValue(GunSO newGunSO)
        {
            currentGunSO = newGunSO;
        }

        protected float FireRateToTimeDelayShoot(float fireRate) => 1f / fireRate;
    }
}
