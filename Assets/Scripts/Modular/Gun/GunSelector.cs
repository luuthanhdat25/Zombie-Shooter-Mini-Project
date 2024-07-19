using AbstractClass;
using RepeatUtil;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class GunSelector : RepeatMonoBehaviour
    {
        public EventHandler<OnSwitchGunEventArgs> OnSwitchGun;
        public EventHandler<OnReloadEventArgs> OnUpdatedReloadTimer;
        public EventHandler<OnUpdatedBulletEventArgs> OnUpdatedBullet;

        public class OnSwitchGunEventArgs : EventArgs
        {
            public GunSO GunSO;
        }

        public class OnReloadEventArgs : EventArgs
        {
            public float ReloadtimerNormalize;
        }

        public class OnUpdatedBulletEventArgs : EventArgs
        {
            public float CurrentBullet;
            public float TotalBullet;
        }

        [System.Serializable]
        public class GunSOAndShootComponent
        {
            public GunSO GunSO;
            public AbsShoot AbsShoot;
        }

        [SerializeField]
        private List<GunSOAndShootComponent> gunSOAndComponentList;

        [SerializeField]
        private Transform gunHoldTransform;

        [SerializeField]
        private int projectileLayerMarkIndex;

        [SerializeField]
        private bool holdGunOnStart;

        [SerializeField]
        private bool infiniteBullet;

        private List<GunController> gunControllerList;
        private int indexSelectGun;
        private bool isUnUsingGun = true;
        private float reloadTimer;
        private bool isReloading;

        public bool IsUnUsingGun => isUnUsingGun;
        public void SetInfiniteBullet(bool isTrue) => infiniteBullet = isTrue;

        protected override void Awake()
        {
            base.Awake();
            foreach (var item in gunSOAndComponentList)
            {
                LoadComponent(ref item.AbsShoot, gameObject);
            }
        }

        private void Start()
        {
            InitializeGunControllers();
            if (holdGunOnStart) ActiveGun(0);
        }

        private void InitializeGunControllers()
        {
            gunControllerList = new();
            foreach (var item in gunSOAndComponentList)
            {
                GameObject gun = Instantiate(item.GunSO.Prefab, gunHoldTransform.position, Quaternion.identity);
                gun.transform.parent = gunHoldTransform;
                GunController gunController = gun.GetComponent<GunController>();
                gunControllerList.Add(gunController);
            }
        }

        private void ActiveGun(int indexSelectGun)
        {
            if (indexSelectGun < 0 || indexSelectGun >= gunSOAndComponentList.Count) return;

            gunControllerList.ForEach(gunController => gunController.gameObject.SetActive(false));
            gunControllerList[indexSelectGun].gameObject.SetActive(true);

            gunSOAndComponentList[indexSelectGun].AbsShoot.ResetShootValue(gunSOAndComponentList[indexSelectGun].GunSO, projectileLayerMarkIndex);
        
            OnSwitchGun?.Invoke(this, new OnSwitchGunEventArgs
            {
                GunSO = gunSOAndComponentList[indexSelectGun].GunSO
            });
            OnUpdatedBullet?.Invoke(this, new OnUpdatedBulletEventArgs
            {
                CurrentBullet = CurrentGunController().CurrentBullet,
                TotalBullet = CurrentGunController().TotalBullet
            });
        }

        public void SwitchGunNext()
        {
            if (gunSOAndComponentList.Count <= 1) return;

            indexSelectGun = (indexSelectGun + 1) % gunSOAndComponentList.Count;
            ActiveGun(indexSelectGun);

            isReloading = false;
            reloadTimer = 0;

            if (!CurrentGunController().IsOutOfBullet() && CurrentGunController().CurrentBullet == 0)
            {
                Reload();
            }
            OnUpdatedReloadTimer?.Invoke(this, new OnReloadEventArgs
            {
                ReloadtimerNormalize = 0
            });
        }

        public bool Reload()
        {
            if (CurrentGunController().IsFullCurrentBullet() || infiniteBullet || isReloading) return false;
            isReloading = true;
            reloadTimer = 0;
            OnUpdatedReloadTimer?.Invoke(this, new OnReloadEventArgs
            {
                ReloadtimerNormalize = 0
            });
            return true;
        }

        private void FixedUpdate() => HandleReload();

        private void HandleReload()
        {
            if (!isReloading) return;

            reloadTimer += Time.fixedDeltaTime;
            if (reloadTimer >= CurrentGunSO().ReloadDuration)
            {
                isReloading = false;
                reloadTimer = 0;
                CurrentGunController().Reload();
                OnUpdatedBullet?.Invoke(this, new OnUpdatedBulletEventArgs
                {
                    CurrentBullet = CurrentGunController().CurrentBullet,
                    TotalBullet = CurrentGunController().TotalBullet
                });
            }

            OnUpdatedReloadTimer?.Invoke(this, new OnReloadEventArgs
            {
                ReloadtimerNormalize = reloadTimer / CurrentGunSO().ReloadDuration
            });
        }

        public void UsingGun(Vector3 shootDirection, bool isDeltaTime)
        {
            if (isReloading) return;

            isUnUsingGun = false;
            if (CurrentGunController().IsOutOfBullet() && !infiniteBullet) return;
            int numberOfBullets = GetNumberBulletShoot();
            if (numberOfBullets != 0)
            {
                if (CurrentAbsShoot().ShootHold(shootDirection, CurrentShootPosition(), numberOfBullets, isDeltaTime))
                {
                    if (!infiniteBullet)
                    {
                        CurrentGunController().DeductCurrentBullet(numberOfBullets);
                    }
                    if (!CurrentGunController().IsOutOfBullet() && CurrentGunController().CurrentBullet == 0)
                    {
                        Reload();
                    }
                    OnUpdatedBullet?.Invoke(this, new OnUpdatedBulletEventArgs
                    {
                        CurrentBullet = CurrentGunController().CurrentBullet,
                        TotalBullet = CurrentGunController().TotalBullet
                    });
                }
            }
            else
            {
                Reload();
            }
        }

        private int GetNumberBulletShoot()
        {
            return infiniteBullet ? CurrentGunSO().NumberBulletShootOneTime : CurrentGunController().GetBulletCanUse();
        }

        public void UnUsingGun(Vector3 releasePosition)
        {
            if (isReloading) return;
            if (isUnUsingGun) return;

            isUnUsingGun = true;
            if (CurrentGunController().IsOutOfBullet() && !infiniteBullet) return;

            int numberOfBullets = GetNumberBulletShoot();
            if (numberOfBullets != 0)
            {
                if (CurrentAbsShoot().ShootRelease(releasePosition, CurrentShootPosition(), numberOfBullets))
                {
                    if (!infiniteBullet)
                    {
                        CurrentGunController().DeductCurrentBullet(numberOfBullets);
                    }
                    if (!CurrentGunController().IsOutOfBullet() && CurrentGunController().CurrentBullet == 0)
                    {
                        Reload();
                    }
                    OnUpdatedBullet?.Invoke(this, new OnUpdatedBulletEventArgs
                    {
                        CurrentBullet = CurrentGunController().CurrentBullet,
                        TotalBullet = CurrentGunController().TotalBullet
                    });
                }
            }
            else
            {
                Reload();
            }
        }

        public GunSO CurrentGunSO() => gunSOAndComponentList[indexSelectGun].GunSO;

        private GunController CurrentGunController() => gunControllerList[indexSelectGun];

        private AbsShoot CurrentAbsShoot() => gunSOAndComponentList[indexSelectGun].AbsShoot;

        private Vector3 CurrentShootPosition() => gunControllerList[indexSelectGun].ShootingPoition();
    }
}
