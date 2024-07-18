using AbstractClass;

public class MeleeZombieDamageReciever : AbsDamageReciver
{
    public override void GotHit()
    {
        absController.AbsGraphic.ActiveHurtEffect();
    }
}
