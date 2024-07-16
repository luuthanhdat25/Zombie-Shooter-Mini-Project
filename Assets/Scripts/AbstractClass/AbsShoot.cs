using RepeatUtil;
using ScriptableObjects;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsShoot : RepeatMonoBehaviour
    {
        protected float firingTimer;

        public abstract void Shoot(Vector3 direction, GunSO gunSO);
        
        public abstract void ResetShootValue(GunSO newGunSO);

        protected float FireRateToTimeDelayShoot(float fireRate) => 1f / fireRate;
    }
}
