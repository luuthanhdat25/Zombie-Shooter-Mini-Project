using UnityEngine;

namespace Player
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] FootstepAudioClips;

        [Range(0, 1)]
        [SerializeField]
        private float FootstepAudioVolume = 0.5f;

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
                }
            }
        }
    }
}
