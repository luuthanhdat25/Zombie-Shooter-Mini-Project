using AbstractClass;

public class ZombieDamageReciever : AbsDamageReciver
{
    public override void GotHit()
    {
        absController.AbsGraphic.ActiveHurtEffect();
    }
}
