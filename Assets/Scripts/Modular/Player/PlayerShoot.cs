using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public void Fire(Vector3 direction)
        {
            //isFiring = direction != Vector3.zero;

            //if (isFiring && firingCoroutine == null)
            //{
            //    firingCoroutine = StartCoroutine(FireContinously());
            //}
        }

        //private IEnumerator FireContinously()
        //{
        //    // Spawn Projectile at gun Position
        //    Debug.Log("Shoot");
        //    yield return new WaitForSeconds(Shoot.FireTime(firingRate));
        //    firingCoroutine = null;
        //}
    }
}

