using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace oneBlack74.UnityToolbox.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private int _poolSize = 10;

        private AudioPool _pool;
        private AudioSource _musicSource;
        private float _masterVolume = 1f;
        private float _sfxVolume = 1f;
        private float _musicVolume = 1f;
        private CancellationTokenSource _musicCts;

        private void Awake()
        {
            _pool = new AudioPool(transform, _poolSize);

            var musicGo = new GameObject("MusicSource");
            musicGo.transform.SetParent(transform);
            _musicSource = musicGo.AddComponent<AudioSource>();
        }

        // Play a sound effect at a position
        public void PlaySFX(SoundEffect soundEffect, Vector3 position = default)
        {
            if (soundEffect == null || soundEffect.Clip == null) return;

            AudioSource source = _pool.Get();
            ApplySFX(source, soundEffect, position);
            source.spatialBlend = soundEffect.SpatialBlend;
            source.Play();
            _pool.AutoReturn(source).Forget();
        }

        // Play a sound effect attached to a transform
        public void PlaySFXAttached(SoundEffect soundEffect, Transform target)
        {
            if (soundEffect == null || soundEffect.Clip == null) return;

            AudioSource source = _pool.Get();
            ApplySFX(source, soundEffect, target.position);
            source.transform.SetParent(target);
            source.spatialBlend = 1f;
            source.Play();
            ReturnAfterPlay(source, target).Forget();
        }

        // Stop all sound effects
        public void StopAllSFX() => _pool.ReturnAll();

        // Play a music track with fade in
        public async UniTask PlayMusic(MusicTrack track)
        {
            if (track == null || track.Clip == null) return;

            _musicCts?.Cancel();
            _musicCts = new CancellationTokenSource();
            var token = _musicCts.Token;

            // Fade out current music
            if (_musicSource.isPlaying)
                await FadeMusic(0f, track.FadeOutDuration, token);

            _musicSource.clip = track.Clip;
            _musicSource.loop = track.Loop;
            _musicSource.volume = 0f;
            _musicSource.Play();

            // Fade in new music
            await FadeMusic(track.Volume * _musicVolume * _masterVolume, track.FadeInDuration, token);
        }

        // Stop current music with fade out
        public async UniTask StopMusic()
        {
            _musicCts?.Cancel();
            _musicCts = new CancellationTokenSource();

            await FadeMusic(0f, 1f, _musicCts.Token);
            _musicSource.Stop();
        }

        public void SetMasterVolume(float volume)
        {
            _masterVolume = Mathf.Clamp01(volume);
            UpdateMusicVolume();
        }

        public void SetSFXVolume(float volume) => _sfxVolume = Mathf.Clamp01(volume);

        public void SetMusicVolume(float volume)
        {
            _musicVolume = Mathf.Clamp01(volume);
            UpdateMusicVolume();
        }

        // Apply sfx settings to a source
        private void ApplySFX(AudioSource source, SoundEffect sfx, Vector3 position)
        {
            source.transform.SetParent(transform);
            source.transform.position = position;
            source.clip = sfx.Clip;
            source.volume = sfx.Volume * _sfxVolume * _masterVolume;
            source.pitch = sfx.Pitch;
            source.loop = sfx.Loop;
        }

        // Update music source volume
        private void UpdateMusicVolume()
        {
            if (_musicSource.isPlaying)
                _musicSource.volume = _musicVolume * _masterVolume;
        }

        // Fade music volume over duration
        private async UniTask FadeMusic(float targetVolume, float duration, CancellationToken token)
        {
            float startVolume = _musicSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (token.IsCancellationRequested) return;
                elapsed += Time.deltaTime;
                _musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            _musicSource.volume = targetVolume;
        }

        // Return source after clip ends and reparent
        private async UniTaskVoid ReturnAfterPlay(AudioSource source, Transform target)
        {
            await UniTask.WaitWhile(() => source != null && source.isPlaying);
            source.transform.SetParent(transform);
            _pool.Return(source);
        }

        private void OnDestroy()
        {
            _musicCts?.Cancel();
            _musicCts?.Dispose();
        }
    }
}
