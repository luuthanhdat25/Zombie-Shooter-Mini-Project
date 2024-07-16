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

        [SerializeField]
        protected List<TagType> collisionTags;

        public List<TagType> TagList => tags;

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

        public bool IsHasTag(TagType tag)
        {
            if(tags == null) return false;
            return tags.Contains(tag);
        }

        public bool IsCollision(List<TagType> tagList)
        {
            foreach (var collisionTag in this.collisionTags)
            {
                if (tagList.Contains(collisionTag)) return true;
            }
            return false;
        }
    }
}

