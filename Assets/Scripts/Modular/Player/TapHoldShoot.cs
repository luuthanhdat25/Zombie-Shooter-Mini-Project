using AbstractClass;
using Enum;
using Manager;
using Projectile;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TapHoldShoot : AbsShoot
    {
        public override void ResetShootValue(GunSO gunSO)
        {
            firingTimer = 0;
        }

        public override void Shoot(Vector3 direction, GunSO gunSO)
        {
            if(direction == Vector3.zero) return;

            firingTimer += Time.fixedDeltaTime;

            if(firingTimer >= FireRateToTimeDelayShoot(gunSO.FireRate))
            {
                //SpawnProjetile(currentGunSO, direction);
                Debug.Log("Tap shoot");
                firingTimer = 0;
            }
        }

        private void SpawnProjetile(GunSO gunSo, Vector3 direction)
        {
            //Debug.Log("Shoot: " + gunSo.Prefab.name);
            //GameObject currentGun = gunObjectList[indexSelectGun];
            //Vector3 shootingPosition = currentGun.GetComponent<GunController>().ShootingPoition();
            //AbstractProjectileMovement projectile = Instantiate(gunSo.ProjectileSO.Prefab, shootingPosition, Quaternion.identity).GetComponent<AbstractProjectileMovement>();
            //projectile.Move(direction, currentGunSO.ProjectileSO.SpeedMove);
        }
    }
}

