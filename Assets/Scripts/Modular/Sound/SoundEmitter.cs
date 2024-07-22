using Manager;
using RepeatUtil;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : RepeatMonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        private Coroutine playingCoroutine;
        public LinkedListNode<SoundEmitter> SoundEmitterLinkedListNode { get; set; }

        protected override void Awake()
        {
            base.Awake();
            LoadComponent(ref audioSource, gameObject);
        }

        public void Initialize(SoundSO soundSO)
        {
            audioSource.volume = soundSO.Volume;
            audioSource.clip = soundSO.AudioClip;
            audioSource.outputAudioMixerGroup = soundSO.AudioMixerGroup;
            audioSource.loop = soundSO.Loop;
            audioSource.playOnAwake = soundSO.PlayOnAwake;
        }

        public void Play()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
            }

            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            Stop();
        }

        public void Stop()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }

            audioSource.Stop();
            SoundManager.Instance.ReturnToPool(this);
        }

        public void SetRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            audioSource.pitch += Random.Range(min, max);
        }
    }
}
