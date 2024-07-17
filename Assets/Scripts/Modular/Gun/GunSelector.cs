using AbstractClass;
using Enum;
using Manager;
using Player;
using RepeatUtil;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSelector : RepeatMonoBehaviour
{
    public EventHandler<OnSwitchGunEventArgs> OnSwitchGun;
    public EventHandler<OnReloadEventArgs> OnReloadGun;

    public class OnSwitchGunEventArgs : EventArgs
    {
        public GunSO GunSO;
    }

    public class OnReloadEventArgs : EventArgs
    {
        public int CurrentBullet;
        public int TotalBullet;
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
    }

    public void SwitchGunNext()
    {
        if (gunSOList.Count <= 1) return;
        indexSelectGun++;
        indexSelectGun = indexSelectGun >= gunSOList.Count ? 0 : indexSelectGun;
        
        ActiveGun(indexSelectGun);
    }

    public void UsingGun(Vector3 shootDirection)
    {
        isUnUsingGun = false;
        if (CurrentGunController().IsOutOfBullet())
        {
            Debug.Log("Run of of Bullet"); return;
        }

        int numberOfBullet = CurrentGunController().GetBulletCanUse();
        Debug.Log("Using: " + numberOfBullet);

        if(numberOfBullet != 0)
        {
            if(CurrentAbsShoot().ShootHold(shootDirection, CurrentShootPosition(), numberOfBullet))
            {
                CurrentGunController().DeductBullet(numberOfBullet);
            }
        }
        else
        {
            CurrentGunController().Reload();
        }
    }

    public void UnUsingGun(Vector3 releasePosition)
    {
        if (isUnUsingGun) return;

        isUnUsingGun = true;
        if (CurrentGunController().IsOutOfBullet()) {
            Debug.Log("Run of of Bullet"); return;
        }

        int numberOfBullet = CurrentGunController().GetBulletCanUse();
        Debug.Log("Unusing: " + numberOfBullet);

        if (numberOfBullet != 0)
        {
            if (CurrentAbsShoot().ShootRelease(releasePosition, CurrentShootPosition(), numberOfBullet))
            {
                CurrentGunController().DeductBullet(numberOfBullet);
            }
        }
        else
        {
            CurrentGunController().Reload();
        }
    }

    private GunController CurrentGunController() => gunControllerList[indexSelectGun];
    private AbsShoot CurrentAbsShoot() => shootTypeDictionary[gunSOList[indexSelectGun].ShootType];
    private Vector3 CurrentShootPosition() => gunControllerList[indexSelectGun].ShootingPoition();
}
