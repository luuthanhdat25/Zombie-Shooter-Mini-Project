using AbstractClass;
using Enum;
using Projectile;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShoot : AbsShoot
{
    private float aimTimer;
    private float aimDuration;

    public override void ResetShootValue(GunSO gunSO)
    {
        firingTimer = FireRateToTimeDelayShoot(gunSO.FireRate);
        aimTimer = 0;
        aimDuration = gunSO.AimDuration;
    }

    public override void ShootHold(ShootData shootData)
    {
        firingTimer += Time.fixedDeltaTime;
        aimTimer += Time.fixedDeltaTime;
    }

    public override void ShootRelease(ShootData shootData)
    {
        if (firingTimer >= FireRateToTimeDelayShoot(shootData.GunSO.FireRate)
            && aimTimer >= aimDuration)
        {
            SpawnProjetile(shootData);
        }
        aimTimer = 0;
        firingTimer = 0;
    }

    private void SpawnProjetile(ShootData shootData)
    {
        Debug.Log("Shoot: " + shootData.GunSO.Prefab.name);
        //GameObject currentGun = gunObjectList[indexSelectGun];
        //Vector3 shootingPosition = currentGun.GetComponent<GunController>().ShootingPoition();
        //AbstractProjectileMovement projectile = Instantiate(gunSo.ProjectileSO.Prefab, shootingPosition, Quaternion.identity).GetComponent<AbstractProjectileMovement>();
        //projectile.Move(direction, currentGunSO.ProjectileSO.SpeedMove);
    }

    public float AimProcessNormalize()
    {
        if (aimDuration == 0) return 1;
        return Mathf.Clamp01(aimTimer / aimDuration);
    }
}
