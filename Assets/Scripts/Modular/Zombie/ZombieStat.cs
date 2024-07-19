using AbstractClass;
using ScriptableObjects;
using UnityEngine;

public class ZombieStat: AbsStat
{
    [SerializeField]
    private ZombieSO zombieSO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (zombieSO == null) Debug.Log(typeof(ZombieStat).Name + " does't have " + typeof(ZombieSO).Name);
        hpMax = zombieSO.Health;
        SetDamage(zombieSO.Damage);
    }

    private void Start()
    {
        OnDead += () => Destroy(transform.parent.gameObject);
    }

    public override float GetMoveSpeed() => zombieSO.MoveSpeed;
}
