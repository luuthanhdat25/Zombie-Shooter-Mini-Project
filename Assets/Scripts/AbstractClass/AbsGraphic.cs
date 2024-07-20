using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for graphic components. Provides methods for modifying visual appearance.
    /// </summary>
    public abstract class AbsGraphic : RepeatMonoBehaviour
    {
        public virtual void ActiveHurtEffect()
        {
            return;
        }
    }
}


