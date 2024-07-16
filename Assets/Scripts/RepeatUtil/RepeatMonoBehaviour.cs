using System.Collections.Generic;
using UnityEngine;

namespace RepeatUtil
{
    public class RepeatMonoBehaviour : MonoBehaviour
    {
        protected virtual void Reset()
        {
            this.LoadComponents();
            this.ResetValue();
        }
    
        protected virtual void Awake()
        {
            this.LoadComponents();
            this.ResetValue();
        }
    
        protected virtual void ResetValue()
        {
            // For override
        }

        protected virtual void LoadComponents()
        {
            // For override
        }

        public static T LoadComponentInChild<T>(ref T component, GameObject gameObj) where T : Component
        {
            if (component != null) return component;

            component = gameObj.GetComponentInChildren<T>();
            if(component != null) return component;

            Debug.LogError($"Component [{typeof(T).Name}] doesn't have in GameObject [{gameObj.name}] child");
            return null;
        }

        public static T LoadComponentInParent<T>(ref T component, GameObject gameObj) where T : Component
        {
            if (component != null) return component;

            Transform parent = gameObj.transform.parent;
            while (parent != null)
            {
                T foundComponent = parent.GetComponent<T>();
                if (foundComponent != null)
                {
                    component = foundComponent;
                    return component;
                }
                parent = parent.parent;
            }

            Debug.LogError($"Component [{typeof(T).Name}] not found in parents of GameObject [{gameObj.name}]");
            return null;
        }

        //Find component in all children(all children of children) of this transform 
        protected T FindComponentInChildren<T>() where T : Component
        {
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(transform);

            while (queue.Count > 0)
            {
                Transform current = queue.Dequeue();
                T foundComponent = current.GetComponent<T>();
                if (foundComponent != null) return foundComponent;

                int childCount = current.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = current.GetChild(i);
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
