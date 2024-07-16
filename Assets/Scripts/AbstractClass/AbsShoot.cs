using RepeatUtil;
using ScriptableObjects;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsShoot : RepeatMonoBehaviour
    {
        protected float firingTimer;

        public abstract void ShootHold(ShootData shootData);

        public abstract void ShootRelease(ShootData shootData);
        
        public abstract void ResetShootValue(GunSO newGunSO);

        protected float FireRateToTimeDelayShoot(float fireRate) => 1f / fireRate;
    }
}
