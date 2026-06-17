using UnityEngine;

namespace oneBlack74.UnityToolbox.Audio
{
    [CreateAssetMenu(fileName = "MusicTrack", menuName = "UnityToolbox/Audio/Music Track")]
    public class MusicTrack : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
        [SerializeField] private bool _loop = true;
        [SerializeField] private float _fadeInDuration = 1f;
        [SerializeField] private float _fadeOutDuration = 1f;

        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public bool Loop => _loop;
        public float FadeInDuration => _fadeInDuration;
        public float FadeOutDuration => _fadeOutDuration;
    }
}
