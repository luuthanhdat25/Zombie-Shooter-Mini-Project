using Manager;
using ScriptableObjects;
using UnityEngine;

namespace Sound
{
    public class SoundBuilder
    {
        private readonly SoundManager soundManager;
        private Vector3 position = Vector3.zero;
        private bool randomPitch;

        public SoundBuilder(SoundManager soundManager) {
            this.soundManager = soundManager;
        }

        public SoundBuilder SetPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        public SoundBuilder SetRandomPitch()
        {
            this.randomPitch = true;
            return this;
        }

        public void Play(SoundSO soundSO)
        {
            if (soundSO == null)
            {
                Debug.LogError("SoundSO is null");
                return;
            }

            if(SoundManager.Instance == null)
            {
                Debug.LogError("SoundManager is null");
                return;
            }

            if (!soundManager.CanPlaySound(soundSO)) return;

            SoundEmitter soundEmitter = soundManager.Get();
            soundEmitter.Initialize(soundSO);
            soundEmitter.transform.position = this.position;
            soundEmitter.transform.parent = soundManager.transform;

            if (randomPitch)
            {
                soundEmitter.SetRandomPitch();
            }

            if (soundSO.FrequentSound)
            {
                soundEmitter.SoundEmitterLinkedListNode = soundManager.FrequentSoundEmitters.AddLast(soundEmitter);
            }

            soundEmitter.Play();
        }
    }
}