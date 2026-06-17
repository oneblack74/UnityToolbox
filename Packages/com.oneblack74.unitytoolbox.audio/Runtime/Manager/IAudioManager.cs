using UnityEngine;
using Cysharp.Threading.Tasks;

namespace oneBlack74.UnityToolbox.Audio
{
    public interface IAudioManager
    {
        // Play a sound effect at a position
        void PlaySFX(SoundEffect soundEffect, Vector3 position = default);

        // Play a sound effect attached to a transform
        void PlaySFXAttached(SoundEffect soundEffect, Transform target);

        // Stop all sound effects
        void StopAllSFX();

        // Play a music track with fade in
        UniTask PlayMusic(MusicTrack track);

        // Stop current music with fade out
        UniTask StopMusic();

        // Set global volume
        void SetMasterVolume(float volume);

        // Set SFX volume
        void SetSFXVolume(float volume);

        // Set music volume
        void SetMusicVolume(float volume);
    }
}
