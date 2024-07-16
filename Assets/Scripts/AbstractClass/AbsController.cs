using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsController : RepeatMonoBehaviour
    {
        [SerializeField]
        protected AbsGraphic absGraphic;
        public AbsGraphic AbsGraphic => absGraphic;

        [SerializeField]
        protected AbsAnimator absAnimator;
        public AbsAnimator AbsAnimator => absAnimator;

        [SerializeField]
        protected AbsTag absTag;
        public AbsTag AbsTag => absTag;

        [SerializeField]
        protected AbsMovement absMovement;
        public AbsMovement AbsMovement => absMovement;

        [SerializeField]
        protected AbsVisionSensor absVisionSensor;
        public AbsVisionSensor AbsVisionSensor => absVisionSensor;

        [SerializeField]
        protected AbsHearingSensor absHearingSensor;
        public AbsHearingSensor AbsHearingSensor => absHearingSensor;

        [SerializeField]
        protected AbsStat absStat;
        public AbsStat AbsStat => absStat;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInChild<AbsGraphic>(ref absGraphic, gameObject);
            LoadComponentInChild<AbsAnimator>(ref absAnimator, gameObject);
            LoadComponentInChild<AbsTag>(ref absTag, gameObject);
            LoadComponentInChild<AbsMovement>(ref absMovement, gameObject);
            LoadComponentInChild<AbsVisionSensor>(ref absVisionSensor, gameObject);
            LoadComponentInChild<AbsHearingSensor>(ref absHearingSensor, gameObject);
            LoadComponentInChild<AbsStat>(ref absStat, gameObject);
        }
    }
}

