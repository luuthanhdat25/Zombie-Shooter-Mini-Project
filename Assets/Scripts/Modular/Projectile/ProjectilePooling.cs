using RepeatUtil.DesignPattern.SingletonPattern;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectilePooling : Singleton<ProjectilePooling>
    {
        public virtual Transform GetProjectile(ProjectileSO projectileSO, Vector3 position, Quaternion rotation)
        {
            Transform prefab = GetFromPool(projectileSO);
            prefab.SetParent(transform);
            prefab.SetPositionAndRotation(position, rotation);
            prefab.gameObject.SetActive(true);
            return prefab;
        }

        protected virtual Transform GetFromPool(ProjectileSO projectileSO)
        {
            foreach (Transform objectFromPool in transform)
            {
                if (!objectFromPool.gameObject.activeSelf && objectFromPool.name == projectileSO.name)
                {
                    return objectFromPool;
                }
            }

            Transform newPrefab = Instantiate(projectileSO.Prefab.transform);
            newPrefab.name = projectileSO.name;
            return newPrefab;
        }

        public virtual void Despawn(Transform obj)
        {
            obj.gameObject.SetActive(false);
            TrailRenderer trailRenderer = obj.GetComponent<TrailRenderer>();
            if( trailRenderer != null) trailRenderer.Clear();
        }

        public virtual void DespawnAllPool()
        {
            foreach (Transform trans in transform)
            {
                Despawn(trans);
            }
        }
    }
}
