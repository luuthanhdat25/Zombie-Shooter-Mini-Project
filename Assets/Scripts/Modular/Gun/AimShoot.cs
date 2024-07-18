using AbstractClass;
using ScriptableObjects;
using UnityEngine;

namespace Gun
{
    public class AimShoot : AbsShoot
    {
        private float aimTimer;

        public override void ResetShootValue(GunSO gunSO, int projectileLayerMarkIndex)
        {
            base.ResetShootValue(gunSO, projectileLayerMarkIndex);
            firingTimer = FireRateToTimeDelayShoot(gunSO.FireRate);
            aimTimer = 0;
        }

        public override bool ShootHold(Vector3 initalDirection, Vector3 initalPosition, int numberOfBullet)
        {
            firingTimer += Time.fixedDeltaTime;
            aimTimer += Time.fixedDeltaTime;
            return false;
        }

        public override bool ShootRelease(Vector3 releasePosition, Vector3 initalPosition, int numberOfBullet)
        {
            bool isShoot = false;
            if (firingTimer >= FireRateToTimeDelayShoot(currentGunSO.FireRate)
                && aimTimer >= currentGunSO.AimDuration)
            {
                SpawnProjetile(releasePosition, initalPosition);
                isShoot = true;
            }
            aimTimer = 0;
            firingTimer = 0;
            return isShoot;
        }

        private void SpawnProjetile(Vector3 releasePosition, Vector3 initalPosition)
        {
            Debug.Log("Shoot: " + currentGunSO.Prefab.name);
            GameObject newProjectile = Instantiate(currentGunSO.ProjectileSO.Prefab, initalPosition, Quaternion.identity);
            AbsController projectileController = newProjectile.GetComponent<AbsController>();
            if (projectileController == null)
            {
                Debug.LogError(currentGunSO.Prefab.name + " doesn't have controller!");
            }
            projectileController.SetLayerMark(currentProjectileLayerMark);
            projectileController.AbsDamageSender.SetDamage(currentGunSO.Damage);
            projectileController.AbsMovement.Move(releasePosition, currentGunSO.ProjectileSO.SpeedMove);
        }

        public float AimProcessNormalize()
        {
            if (currentGunSO.AimDuration == 0) Debug.LogError($"{currentGunSO.Name} aim duration is 0!");
            return Mathf.Clamp01(aimTimer / currentGunSO.AimDuration);
        }
    }
}
