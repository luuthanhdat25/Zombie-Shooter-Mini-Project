using Manager;
using Player;
using ScriptableObjects;
using Sound;
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
        private SoundSO doorAcceptSoundSO;

        [SerializeField]
        private SoundSO doorRejectSoundSO;

        [SerializeField]
        private Transform doorTransform;

        [SerializeField]
        private BoxCollider doorTriggerBox;

        [SerializeField]
        private List<ZombieController> zombiesInZone;

        [SerializeField]
        private KeySO keySOAccept;

        [SerializeField]
        private KeySO keySONextDoor;

        private float doorMoveDistance = 3.0f;
        private const float MOVE_SPEED = 4.0f;  

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
                UIManager.Instance.InPlayingUI.MessageUI.ShowMessage("Player doesn't has " + keySOAccept.Name, Color.red);
                SoundPooling.Instance.CreateSound(doorRejectSoundSO, PlayerPublicInfor.Instance.Position, 0, 0);
            }
            else
            {
                SoundPooling.Instance.CreateSound(doorAcceptSoundSO, PlayerPublicInfor.Instance.Position, 0, 0);
                UIManager.Instance.InPlayingUI.MessageUI.ShowMessage("Open door", Color.green);

                PlayerPublicInfor.Instance.KeyCollector.RemoveKey(keySOAccept);
                doorTriggerBox.enabled = false;
                StartCoroutine(DoorMoveDown());

                if(zombiesInZone != null)
                {
                    foreach (var zombie in zombiesInZone)
                    {
                        zombie.ActiveBehaviourTree();
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
                doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, MOVE_SPEED * Time.deltaTime);
                yield return null;
            }

            doorTransform.position = targetPosition;
        }
    }
}
