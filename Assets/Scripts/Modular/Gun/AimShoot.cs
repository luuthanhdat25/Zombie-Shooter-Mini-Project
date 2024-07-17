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

    public override void ResetShootValue(GunSO gunSO)
    {
        base.ResetShootValue(gunSO);
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
        AbsController absController = newProjectile.GetComponent<AbsController>();
        if (absController == null)
        {
            Debug.LogError(currentGunSO.Prefab.name + " doesn't have controller!");
        }
        //Caculate destination
        Vector3 destination = releasePosition;
        absController.AbsMovement.Move(destination, currentGunSO.ProjectileSO.SpeedMove);
    }

    public float AimProcessNormalize()
    {
        if (currentGunSO.AimDuration == 0) Debug.LogError($"{currentGunSO.Name} aim duration is 0!");
        return Mathf.Clamp01(aimTimer / currentGunSO.AimDuration);
    }
}
