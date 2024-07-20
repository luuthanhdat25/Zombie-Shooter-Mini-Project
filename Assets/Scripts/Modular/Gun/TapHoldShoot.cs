using AbstractClass;
using Projectile;
using ScriptableObjects;
using UnityEngine;

namespace Gun
{
    public class TapHoldShoot : AbsShoot
    {
        public override void ResetShootValue(GunSO gunSO, int projectileLayerMarkIndex)
        {
            base.ResetShootValue(gunSO, projectileLayerMarkIndex);
            firingTimer = 0;
        }

        public override bool ShootHold(Vector3 initalDirection, Vector3 initalPosition, int numberOfBullet, bool isDeltaTime)
        {
            firingTimer += isDeltaTime? Time.deltaTime: Time.fixedDeltaTime;

            if(firingTimer >= FireRateToTimeDelayShoot(currentGunSO.FireRate))
            {
                SpawnProjetile(initalDirection, initalPosition);
                firingTimer = 0;
                return true;
            }
            return false;
        }

        public override bool ShootRelease(Vector3 releasePosition, Vector3 initalPosition, int numberOfBullet)
        {
            return false;
        }

        private void SpawnProjetile(Vector3 initalDirection, Vector3 initalPosition)
        {
            Transform newProjectile = ProjectilePooling.Instance.GetProjetile(currentGunSO.ProjectileSO, initalPosition, Quaternion.identity); ;
            AbsController projectileController = newProjectile.GetComponent<AbsController>();
            if(projectileController == null)
            {
                Debug.LogError(currentGunSO.Prefab.name + " doesn't have controller!");
            }
            projectileController.SetLayerMark(currentProjectileLayerMark);
            projectileController.AbsStat.SetDamage(currentGunSO.Damage);
            projectileController.AbsMovement.Move(initalDirection, currentGunSO.ProjectileSO.SpeedMove);
        }
    }
}

