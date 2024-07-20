using RepeatUtil;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for animator components. Provides common animation control methods.
    /// </summary>
    public abstract class AbsAnimator : RepeatMonoBehaviour
    {
        public virtual void PlayAnimation(string animationName, bool loop)
        {
            return;
        }

        public virtual void SetTimeScale(float timeScale)
        {
            return;
        }

        public virtual void SetBool(string parraName, bool value)
        {
            return;
        }

        public virtual float GetAnimationDuration(string animationName, int layerIndex = 0)
        {
            return 0;
        }
    }
}
