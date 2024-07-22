using Enum;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New GunSO", menuName = "ScriptableObjects/GunSO", order = 0)]
    public class GunSO : ScriptableObject
    {
        public string Name;
        public Sprite ImageSprite;
        public GameObject Prefab;
        public ProjectileSO ProjectileSO;
        public SoundSO ShootSoundSO;
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

        [Range(0.1f, 15f)]
        public float AimDuration;

        [Range(1, 1000)]
        public int NumberBulletShootOneTime = 1;

        [Range(1, 1000)]
        public int NumberBulletReload;

        [Range(1, 1000)]
        public int NumberBulletMax;
    }
}

