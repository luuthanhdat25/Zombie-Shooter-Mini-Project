using Enum;
using RepeatUtil;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsTag : RepeatMonoBehaviour
    {
        [SerializeField]
        protected List<TagType> tags;

        public virtual void AddTag(TagType tag)
        {
            if (tags == null) return;
            if (tags.Contains(tag)) return;
            tags.Add(tag);
        }

        public virtual void RemoveTag(TagType tag)
        {
            if (tags == null) return;
            if (!tags.Contains(tag)) return;
            tags.Remove(tag);
        }
    }
}

