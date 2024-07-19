using Player;
using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zombie;

namespace Door{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField]
        private Transform doorTransform;

        [SerializeField]
        private BoxCollider doorTriggerBox;

        [SerializeField]
        private float moveSpeed = 1.0f;  

        [SerializeField]
        private List<ZombieController> zombiesInZone;

        [SerializeField]
        private KeySO keySOAccept;

        [SerializeField]
        private KeySO keySONextDoor;

        private float doorMoveDistance = 3.0f;

        private void Start()
        {
            GiveKeyToRandomZombie();
        }

        private void GiveKeyToRandomZombie()
        {
            if (zombiesInZone == null || zombiesInZone.Count <= 0) return;
            int randomIndex = UnityEngine.Random.Range(0, zombiesInZone.Count);
            zombiesInZone[randomIndex].KeyCollector.Add(keySONextDoor);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (other.GetComponent<PlayerPublicInfor>() == null) return;
            
            if (!PlayerPublicInfor.Instance.KeyCollector.IsHasKey(keySOAccept))
            {
                MessageUI.Instance.ShowMessage("Player doesn't has " + keySOAccept.Name, Color.red);
            }
            else
            {
                PlayerPublicInfor.Instance.KeyCollector.RemoveKey(keySOAccept);
                doorTriggerBox.enabled = false;
                StartCoroutine(DoorMoveDown());

                if(zombiesInZone != null)
                {
                    foreach (var zombie in zombiesInZone)
                    {
                        zombie.ActiveBehaviourTree(true);
                    }
                }
            }
        }

        private IEnumerator DoorMoveDown()
        {
            Vector3 initialPosition = doorTransform.position;
            Vector3 targetPosition = initialPosition - new Vector3(0, doorMoveDistance, 0);

            while (Vector3.Distance(doorTransform.position, targetPosition) > 0.01f)
            {
                doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            doorTransform.position = targetPosition;
        }
    }
}
