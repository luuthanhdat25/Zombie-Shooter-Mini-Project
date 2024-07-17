using AbstractClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectileDamageSender : AbsDamageSender
{
    public override void GotHit()
    {
        //Despawn
        Destroy(gameObject);
    }
}
