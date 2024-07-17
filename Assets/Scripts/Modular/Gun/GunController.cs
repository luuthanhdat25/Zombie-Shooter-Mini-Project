using AbstractClass;
using ScriptableObjects;
using UnityEngine;

public class GunController : AbsController
{
    [Header("Gun Controller")]
    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    private GunSO gunSO;

    private int totalBullet;
    private int currentBullet;

    public void Start()
    {
        if (gunSO == null) Debug.LogError($"{gameObject.name} doesn't have GunSO");
        totalBullet = gunSO.NumberBulletMax;
        currentBullet = gunSO.NumberBulletReload;
    }

    public int GetBulletCanUse()
    {
        return currentBullet < gunSO.NumberBulletShootOneTime ? currentBullet : gunSO.NumberBulletShootOneTime;
    }

    public bool CanReload() => totalBullet > 0;

    public void Reload()
    {
        if(!CanReload()) return;
        Debug.Log("Reload");
        int neededBullets = gunSO.NumberBulletReload - currentBullet;
        int numberBulletsReload = Mathf.Min(neededBullets, totalBullet);

        currentBullet += numberBulletsReload;
        totalBullet -= numberBulletsReload;
    }

    public void DeductBullet(int valueDeduct)
    {
        if(currentBullet >= valueDeduct)
        {
            currentBullet -= valueDeduct;
        }
        else
        {
            currentBullet = 0;
        }
    }

    public bool IsOutOfBullet() => currentBullet <= 0 && totalBullet <= 0;
    public Vector3 ShootingPoition() => shootingPoint.position;
}
