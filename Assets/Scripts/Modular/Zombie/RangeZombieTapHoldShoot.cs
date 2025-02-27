using AbstractClass;
using Player;
using Projectile;
using ScriptableObjects;
using Sound;
using UnityEngine;

namespace Zombie
{
    public class RangeZombieTapHoldShoot : AbsShoot
    {
        [SerializeField]
        private AbsController absController;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref absController, gameObject);
        }


        public override void ResetShootValue(GunSO gunSO, int projectileLayerMarkIndex)
        {
            base.ResetShootValue(gunSO, projectileLayerMarkIndex);
            firingTimer = FireRateToTimeDelayShoot(currentGunSO.FireRate);
        }

        public override bool ShootHold(Vector3 initalDirection, Vector3 initalPosition, int numberOfBullet, bool isDeltaTime)
        {
            firingTimer += isDeltaTime ? Time.deltaTime : Time.fixedDeltaTime;

            if (firingTimer >= FireRateToTimeDelayShoot(currentGunSO.FireRate))
            {
                SpawnProjetile(initalDirection, initalPosition);
                firingTimer = 0;
                return true;
            }
            return false;
        }

        public override bool ShootRelease(Vector3 releasePosition, Vector3 initalPosition, int numberOfBullet)
        {
            return false;
        }

        private void SpawnProjetile(Vector3 initalDirection, Vector3 initalPosition)
        {
            Transform newProjectile = ProjectilePooling.Instance.GetProjectile(currentGunSO.ProjectileSO, initalPosition, Quaternion.identity); ;
            AbsController projectileController = newProjectile.GetComponent<AbsController>();
            if (projectileController == null)
            {
                Debug.LogError(currentGunSO.Prefab.name + " doesn't have controller!");
            }
            projectileController.SetLayerMark(currentProjectileLayerMark);
            projectileController.AbsStat.SetDamage(absController.AbsStat.GetDamage());
            projectileController.AbsMovement.Move(initalDirection, currentGunSO.ProjectileSO.SpeedMove);
            SoundPooling.Instance.CreateSound(currentGunSO.ShootSoundSO, PlayerPublicInfor.Instance.Position, -0.05f, 0.05f);
        }
    }
}
