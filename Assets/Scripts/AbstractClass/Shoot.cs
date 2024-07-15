using UnityEngine;

namespace AbstractClass
{
    public abstract class Shoot : MonoBehaviour
    {
        [Header("Shoot")]
        [SerializeField] 
        protected float firingRate;
        
        [SerializeField] 
        protected bool isFiring;

        protected Coroutine firingCoroutine;
        
        public abstract void Fire(Vector3 direction);

        /// <summary>
        /// Get fire time by fire rate
        /// </summary>
        /// <param name="fireRate">Number of shots per second</param>
        /// <returns>Fire time</returns>
        public static float FireTime(float fireRate)
        {
            if (fireRate <= 0)
            {
                Debug.LogError("Fire Rate must be greater than 0.");
                return 0;
            }
            return 1.0f / fireRate;
        }
    }
}