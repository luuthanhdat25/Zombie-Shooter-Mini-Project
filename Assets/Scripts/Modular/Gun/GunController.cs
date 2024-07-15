using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint;

    public Vector3 ShootingPoition() => shootingPoint.position;
}
