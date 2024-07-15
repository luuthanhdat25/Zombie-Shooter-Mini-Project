using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStraightMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody.velocity = Vector3.forward * 10;
    }

    
}
