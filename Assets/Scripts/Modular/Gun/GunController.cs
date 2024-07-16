using ScriptableObjects;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint;

    private GunSO gunSO;

    private int projectileCount;

    public void SetGunSO(GunSO gunSO)
    {
        this.gunSO = gunSO;
        projectileCount = gunSO.MaxProjectile;
    }

    public void ResetProjectile() => projectileCount = gunSO.MaxProjectile;

    public int GetProjects(int requestValue)
    {
        if (projectileCount < requestValue) return 0;
        projectileCount -= requestValue;
        return requestValue;
    }

    public bool IsReload()
    {
        return projectileCount <= 0;
    }

    public Vector3 ShootingPoition() => shootingPoint.position;
}
