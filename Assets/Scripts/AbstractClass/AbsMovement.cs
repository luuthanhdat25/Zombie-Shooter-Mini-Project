using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsMovement : RepeatMonoBehaviour
    {
        public abstract void Move(Vector3 moveDirectionOrDestination, float speed);
        public abstract void Rotate(Vector3 rotateDirection);
    }
}

