using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsAnimator : RepeatMonoBehaviour
    {
        public virtual void PlayAnimation(string animationName, bool loop)
        {
            //For override
        }

        public virtual void SetTimeScale(float timeScale)
        {
            //For override
        }

        public virtual void SetBool(string parraName, bool value)
        {
            //For override
        }

        public virtual float GetAnimationDuration(string animationName, int layerIndex = 0)
        {
            //For override
            return 0;
        }
    } 
}
