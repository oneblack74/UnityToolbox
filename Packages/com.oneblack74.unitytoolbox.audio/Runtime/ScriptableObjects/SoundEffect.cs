using UnityEngine;

namespace oneBlack74.UnityToolbox.Audio
{
    [CreateAssetMenu(fileName = "SoundEffect", menuName = "UnityToolbox/Audio/Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
        [SerializeField] [Range(-3f, 3f)] private float _pitch = 1f;
        [SerializeField] [Range(0f, 1f)] private float _spatialBlend = 0f;
        [SerializeField] private bool _loop = false;

        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public float SpatialBlend => _spatialBlend;
        public bool Loop => _loop;
    }
}
