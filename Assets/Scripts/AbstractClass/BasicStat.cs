using AbstractClass;
using UnityEngine;

public class BasicStat : AbsStat
{
    protected override void OnDead()
    {
        Debug.Log(transform.parent.name + "is Dead");
        transform.parent.gameObject.SetActive(false);
    }
}
