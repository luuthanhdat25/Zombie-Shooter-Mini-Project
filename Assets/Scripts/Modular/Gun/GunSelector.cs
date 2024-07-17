using AbstractClass;
using Enum;
using Player;
using RepeatUtil;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private List<GunSO> gunSOList;

    [SerializeField]
    private Transform gunHoldTransform;

    [SerializeField]
    private bool holdGunOnStart;

    private List<GunController> gunControllerList;
    private int indexSelectGun;
    private Dictionary<ShootType, AbsShoot> shootTypeDictionary;
    private bool isUnUsingGun = true;
    private float reloadTimer;
    private bool isReloading;

    protected override void Awake()
    {
        base.Awake();
        if (gunSOList.Count == 0) Debug.LogError("Player doen't have any Gun!");
        
        TapHoldShoot tapHoldShoot = GetComponent<TapHoldShoot> ();
        AimShoot aimShoot = GetComponent<AimShoot>();
        if(tapHoldShoot == null || aimShoot == null)
        {
            Debug.LogError("Some Shoot component null");
        }
        shootTypeDictionary = new()
        {
            { ShootType.TapHold,  tapHoldShoot},
            { ShootType.AimRelease, aimShoot}
        };
    }

    private void Start()
    {
        gunControllerList = new();
        foreach (var gunSO in gunSOList)
        {
            GameObject gun = Instantiate(gunSO.Prefab, gunHoldTransform.position, Quaternion.identity);
            gun.transform.parent = gunHoldTransform;
            GunController gunController = gun.GetComponent<GunController>();
            gunControllerList.Add(gunController);
        }

        if (holdGunOnStart) ActiveGun(0);
    }

    private void ActiveGun(int indexSelectGun)
    {
        if (indexSelectGun < 0 || indexSelectGun >= gunSOList.Count) return;

        gunControllerList.ForEach(gunController => gunController.gameObject.SetActive(false));
        gunControllerList[indexSelectGun].gameObject.SetActive(true);
        
        shootTypeDictionary[gunSOList[indexSelectGun].ShootType].ResetShootValue(gunSOList[indexSelectGun]);
        OnSwitchGun?.Invoke(this, new OnSwitchGunEventArgs
        {
            GunSO = gunSOList[indexSelectGun]
        });
        OnUpdatedBullet?.Invoke(this, new OnUpdatedBulletEventArgs
        {
            CurrentBullet = CurrentGunController().CurrentBullet,
            TotalBullet = CurrentGunController().TotalBullet
        });
    }

    public void SwitchGunNext()
    {
        if (gunSOList.Count <= 1) return;
        indexSelectGun++;
        indexSelectGun = indexSelectGun >= gunSOList.Count ? 0 : indexSelectGun;
        
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

    public void UsingGun(Vector3 shootDirection)
    {
        if (isReloading) return;

        isUnUsingGun = false;
        if (CurrentGunController().IsOutOfBullet()) return;

        int numberOfBullet = CurrentGunController().GetBulletCanUse();
        Debug.Log("Using: " + numberOfBullet);

        if(numberOfBullet != 0)
        {
            if(CurrentAbsShoot().ShootHold(shootDirection, CurrentShootPosition(), numberOfBullet))
            {
                CurrentGunController().DeductCurrentBullet(numberOfBullet);
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

    public void Reload()
    {
        isReloading = true;
        reloadTimer = 0;
        OnUpdatedReloadTimer?.Invoke(this, new OnReloadEventArgs
        {
            ReloadtimerNormalize = 0
        });
    }

    public void UnUsingGun(Vector3 releasePosition)
    {
        if (isReloading) return;
        if (isUnUsingGun) return;

        isUnUsingGun = true;
        if (CurrentGunController().IsOutOfBullet()) return;


        int numberOfBullet = CurrentGunController().GetBulletCanUse();
        Debug.Log("Unusing: " + numberOfBullet);

        if (numberOfBullet != 0)
        {
            if (CurrentAbsShoot().ShootRelease(releasePosition, CurrentShootPosition(), numberOfBullet))
            {
                CurrentGunController().DeductCurrentBullet(numberOfBullet);
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


    private GunSO CurrentGunSO() => gunSOList[indexSelectGun];
    private GunController CurrentGunController() => gunControllerList[indexSelectGun];
    private AbsShoot CurrentAbsShoot() => shootTypeDictionary[gunSOList[indexSelectGun].ShootType];
    private Vector3 CurrentShootPosition() => gunControllerList[indexSelectGun].ShootingPoition();
}
