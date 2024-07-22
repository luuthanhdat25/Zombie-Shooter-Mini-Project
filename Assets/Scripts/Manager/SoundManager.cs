using RepeatUtil.DesignPattern.SingletonPattern;
using ScriptableObjects;
using Sound;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        private IObjectPool<SoundEmitter> soundEmitterPool;
        private List<SoundEmitter> activeSoundEmitters = new ();
        public readonly LinkedList<SoundEmitter> FrequentSoundEmitters = new();

        [SerializeField] 
        private SoundEmitter soundEmitterPrefab;
        
        [SerializeField] 
        private bool collectionCheck = true;
        
        [SerializeField] 
        private int defaultCapacity = 10;
        
        [SerializeField] 
        private int maxPoolSize = 100;
        
        [SerializeField] 
        private int maxSoundInstances = 30;

        private void Start() => InitializePool();

        private void InitializePool()
        {
            soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
        }

        public SoundBuilder CreateSound() => new SoundBuilder(this);

        private SoundEmitter CreateSoundEmitter()
        {
            var soundEmitter = Instantiate(soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }

        private void OnTakeFromPool(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(true);
            activeSoundEmitters.Add(soundEmitter);
        }

        private void OnReturnedToPool(SoundEmitter soundEmitter)
        {
            if (soundEmitter.SoundEmitterLinkedListNode != null)
            {
                FrequentSoundEmitters.Remove(soundEmitter.SoundEmitterLinkedListNode);
                soundEmitter.SoundEmitterLinkedListNode = null;
            }
            soundEmitter.gameObject.SetActive(false);
            activeSoundEmitters.Remove(soundEmitter);
        }

        private void OnDestroyPoolObject(SoundEmitter soundEmitter) => Destroy(soundEmitter.gameObject);

        public bool CanPlaySound(SoundSO soundSO)
        {
            if (!soundSO.FrequentSound) return true;

            if (FrequentSoundEmitters.Count < maxSoundInstances) return true;
            try
            {
                FrequentSoundEmitters.First.Value.Stop();
                return true;
            }
            catch
            {
                Debug.Log("SoundEmitter is already released");
            }
            return false;
        }

        public SoundEmitter Get() => soundEmitterPool.Get();

        public void ReturnToPool(SoundEmitter soundEmitter) => soundEmitterPool.Release(soundEmitter);

        public void StopAll()
        {
            activeSoundEmitters.ForEach(soundEmitter => soundEmitter.Stop());
            FrequentSoundEmitters.Clear();
        }
    }
}
