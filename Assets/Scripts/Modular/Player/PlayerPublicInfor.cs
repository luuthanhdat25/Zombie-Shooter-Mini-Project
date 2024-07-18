using RepeatUtil.DesignPattern.SingletonPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerPublicInfor : Singleton<PlayerPublicInfor>
    {
        public Vector3 Position => transform.position;
    }

}
