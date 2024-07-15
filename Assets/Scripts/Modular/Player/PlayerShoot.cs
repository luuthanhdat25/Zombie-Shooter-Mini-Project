using Enum;
using Manager;
using Projectile;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField]
        private List<GunSO> gunSOList;

        [SerializeField]
        private Transform gunHoldTransform;

        private List<GameObject> gunObjectList;
        private int indexSelectGun;
        private float firingTimer;
        private float aimTimer;
        private GunSO currentGunSO;

        private void Awake()
        {
            if (gunSOList.Count == 0)
            {
                Debug.LogError("Player doen't have any Gun!");
            }
            InputManager.Instance.OnSwitchGun += SwitchGun;

            gunObjectList = new();
            foreach (var gunSO in gunSOList)
            {
                GameObject gun = Instantiate(gunSO.Prefab, gunHoldTransform.position, Quaternion.identity);
                gun.transform.parent = gunHoldTransform;
                gunObjectList.Add(gun);
            }

            currentGunSO = gunSOList[indexSelectGun];
            ActiveGun(indexSelectGun);
        }

        private void SwitchGun()
        {
            if (gunSOList.Count <= 1) return;
            indexSelectGun++;
            indexSelectGun = indexSelectGun >= gunSOList.Count ? 0 : indexSelectGun;

            currentGunSO = gunSOList[indexSelectGun];
            ActiveGun(indexSelectGun);
            firingTimer = FireRateToTimeDelayShoot(currentGunSO.FireRate);
            aimTimer = 0;
        }

        private void ActiveGun(int indexSelectGun)
        {
            if (indexSelectGun < 0 || indexSelectGun >= gunSOList.Count) return;
            gunObjectList.ForEach(gun => gun.SetActive(false));
            gunObjectList[indexSelectGun].SetActive(true);
        }

        private float FireRateToTimeDelayShoot(float fireRate) => 1f / fireRate;

        public void Shoot(Vector3 direction)
        {
            if(direction == Vector3.zero)
            {
                aimTimer = 0;
                return;
            }

            firingTimer += Time.fixedDeltaTime;
            aimTimer += Time.fixedDeltaTime;

            if(firingTimer >= FireRateToTimeDelayShoot(currentGunSO.FireRate))
            {
                switch (currentGunSO.ShootType)
                {
                    case ShootType.TapHold:
                        SpawnProjetile(currentGunSO, direction);
                        firingTimer = 0;
                        break;

                    case ShootType.AimRelease:
                        if(aimTimer >= currentGunSO.AimDuration)
                        {
                            //SpawnProjetile(currentGunSO);
                            firingTimer = 0;
                            aimTimer = 0;
                        }
                        break;
                }
            }
        }

        private void SpawnProjetile(GunSO gunSo, Vector3 direction)
        {
            Debug.Log("Shoot: " + gunSo.Prefab.name);
            GameObject currentGun = gunObjectList[indexSelectGun];
            Vector3 shootingPosition = currentGun.GetComponent<GunController>().ShootingPoition();
            AbstractProjectileMovement projectile = Instantiate(gunSo.ProjectileSO.Prefab, shootingPosition, Quaternion.identity).GetComponent<AbstractProjectileMovement>();
            projectile.Move(direction, currentGunSO.ProjectileSO.SpeedMove);
        }
    }
}

