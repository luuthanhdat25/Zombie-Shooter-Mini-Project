using AbstractClass;
using ScriptableObjects;
using Sound;
using UnityEngine;

public class ZombieDamageReciever : AbsDamageReciver
{
    [SerializeField]
    private SoundSO hurtSoundSO;

    public override void GotHit()
    {
        absController.AbsGraphic.ActiveHurtEffect();
        SoundPooling.Instance.CreateSound(hurtSoundSO, Player.PlayerPublicInfor.Instance.Position, -0.05f, 0.05f);
    }
}
