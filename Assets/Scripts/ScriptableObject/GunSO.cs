using Enum;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New GunSO", menuName = "ScriptableObjects/GunSO", order = 0)]
    public class GunSO : ScriptableObject
    {
        public string Name;
        public GameObject Prefab;
        public ProjectileSO ProjectileSO;
        public GunType GunType;
        public ShootType ShootType;

        [Space]
        [Header("Stats")]
        [Range(0.1f, 100f)] 
        public float FireRate;

        [Range(1, 1000)]
        public int Damage;

        [Range(0.1f, 50f)]
        public float ReloadDuration;

        [Range(1f, 20f)]
        public float AimDuration;
    }
}

