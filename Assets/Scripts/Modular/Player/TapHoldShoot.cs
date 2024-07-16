using AbstractClass;
using Enum;
using Manager;
using Projectile;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class TapHoldShoot : AbsShoot
    {
        public override void ResetShootValue(GunSO gunSO)
        {
            firingTimer = 0;
        }

        public override void Shoot(ShootData shootData)
        {
            if(shootData.InitialDirection == Vector3.zero) return;

            firingTimer += Time.fixedDeltaTime;

            if(firingTimer >= FireRateToTimeDelayShoot(shootData.GunSO.FireRate))
            {
                SpawnProjetile(shootData);
                firingTimer = 0;
            }
        }

        private void SpawnProjetile(ShootData shootData)
        {
            Debug.Log("Shoot: " + shootData.GunSO.Prefab.name);
            GameObject newProjectile = Instantiate(shootData.GunSO.ProjectileSO.Prefab, shootData.InitialPosition, Quaternion.identity);
            AbsController absController = newProjectile.GetComponent<AbsController>();
            if(absController == null)
            {
                Debug.LogError(shootData.GunSO.Prefab.name + " doesn't have controller!");
            }
            absController.AbsMovement.Move(shootData.InitialDirection, shootData.GunSO.ProjectileSO.SpeedMove);
        }
    }
}

