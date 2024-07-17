using AbstractClass;

public class NullDamageSender : AbsDamageSender
{
    public override void GotHit()
    {
        return;
    }
}
