using AbstractClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectileDamageReciever : AbsDamageReciver
{
    [SerializeField]
    private GameObject explosionEffect;

    [SerializeField]
    private float radiusExplosion = 4f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boom");
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplosion);

        foreach (Collider objCollider in colliders)
        {
            Rigidbody rigidbody = objCollider.GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                rigidbody.AddExplosionForce(100, transform.position, radiusExplosion);
            }
        }

        Destroy(gameObject);
    }
}
