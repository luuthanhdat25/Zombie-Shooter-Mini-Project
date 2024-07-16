using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsMovement : RepeatMonoBehaviour
    {
        [Range(0.5f, 10)]
        [SerializeField]
        protected float moveSpeed = 2.5f;

        protected abstract void Move(Vector3 moveDirection);
        protected abstract void Rotate(Vector3 rotateDirection);
    }
}

