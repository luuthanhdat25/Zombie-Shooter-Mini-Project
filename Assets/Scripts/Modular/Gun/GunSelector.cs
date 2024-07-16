using AbstractClass;
using Enum;
using Manager;
using Player;
using RepeatUtil;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : RepeatMonoBehaviour
{
    [SerializeField]
    private List<GunSO> gunSOList;

    [SerializeField]
    private Transform gunHoldTransform;

    [SerializeField]
    private bool holdGunOnStart;

    private List<GameObject> gunObjectList;
    private int indexSelectGun;
    private Dictionary<ShootType, AbsShoot> shootTypeDictionary;

    protected override void Awake()
    {
        base.Awake();
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
        if (gunSOList.Count == 0)
        {
            Debug.LogError("Player doen't have any Gun!");
        }

        gunObjectList = new();
        foreach (var gunSO in gunSOList)
        {
            GameObject gun = Instantiate(gunSO.Prefab, gunHoldTransform.position, Quaternion.identity);
            gun.transform.parent = gunHoldTransform;
            gunObjectList.Add(gun);
        }

        if (holdGunOnStart) ActiveVisualGun(indexSelectGun);
    }

    private void ActiveVisualGun(int indexSelectGun)
    {
        if (indexSelectGun < 0 || indexSelectGun >= gunSOList.Count) return;
        gunObjectList.ForEach(gun => gun.SetActive(false));
        gunObjectList[indexSelectGun].SetActive(true);
    }

    public void SwitchGunNext()
    {
        if (gunSOList.Count <= 1) return;
        indexSelectGun++;
        indexSelectGun = indexSelectGun >= gunSOList.Count ? 0 : indexSelectGun;

        GunSO gunSO = gunSOList[indexSelectGun];
        shootTypeDictionary[gunSO.ShootType].ResetShootValue(gunSO);
        ActiveVisualGun(indexSelectGun);
    }

    public Vector3 ProjectileSpawnPosition()
    {
        GameObject currentGun = gunObjectList[indexSelectGun];
        Vector3 shootingPosition = currentGun.GetComponent<GunController>().ShootingPoition();
        return shootingPosition;
    }

    public void UsingGun(Vector3 shootDirection)
    {
        GunSO currentGun = gunSOList[indexSelectGun];
        ShootData shootData = new ShootData
        {
            InitialDirection = shootDirection,
            InitialPosition = ProjectileSpawnPosition(),
            GunSO = currentGun
        };
        shootTypeDictionary[currentGun.ShootType].Shoot(shootData);
    }
}
