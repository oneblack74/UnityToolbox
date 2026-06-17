# 🔊 Audio — UnityToolbox

![Unity](https://img.shields.io/badge/Unity-6000.0%2B-black?logo=unity)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-WIP-orange)

Audio utilities for Unity 6 — Music, SFX, pooling and volume control.

---

## 📦 Installation

Add via Git URL in Package Manager:

```
https://github.com/oneblack74/UnityToolbox.git?path=Packages/com.oneblack74.unitytoolbox.audio
```

**Requirements:**
- Unity `6000.0+`
- [UniTask](https://github.com/Cysharp/UniTask)
- [Toolbox - Core](https://github.com/oneblack74/UnityToolbox.git?path=Packages/com.oneblack74.unitytoolbox.core)

---

## 📖 Modules

| Module | Description |
|--------|-------------|
| [**AudioManager**](#audiomanager) | Central audio controller for music and SFX |
| [**MusicTrack**](#musictrack) | ScriptableObject for music configuration |
| [**SoundEffect**](#soundeffect) | ScriptableObject for SFX configuration |

---

## AudioManager

### Overview

MonoBehaviour controller — manages music playback with crossfade and a pooled SFX system.

Register it with the `ServiceLocator` for global access.

### Setup

Add `AudioManager` to a GameObject in your scene, then register it:

```csharp
ServiceLocator.Register<IAudioManager>(GetComponent<AudioManager>());
```

### Usage

```csharp
var audio = ServiceLocator.Get<IAudioManager>();

// Play a sound effect at a world position
audio.PlaySFX(explosionSFX, transform.position);

// Play a sound effect following a transform
audio.PlaySFXAttached(footstepSFX, playerTransform);

// Stop all active SFX
audio.StopAllSFX();

// Play a music track (with fade in)
await audio.PlayMusic(menuMusic);

// Stop current music (with fade out)
await audio.StopMusic();

// Volume control
audio.SetMasterVolume(0.8f);
audio.SetMusicVolume(0.5f);
audio.SetSFXVolume(1f);
```

---

## MusicTrack

### Overview

ScriptableObject that holds all configuration for a music track.

**Create:** `Right-click → UnityToolbox/Audio/Music Track`

### Fields

| Field | Default | Description |
|-------|---------|-------------|
| `Clip` | — | The AudioClip to play |
| `Volume` | `1.0` | Track volume (0–1) |
| `Loop` | `true` | Loop the track |
| `FadeInDuration` | `1.0` | Fade in duration in seconds |
| `FadeOutDuration` | `1.0` | Fade out duration in seconds |

---

## SoundEffect

### Overview

ScriptableObject that holds all configuration for a sound effect.

**Create:** `Right-click → UnityToolbox/Audio/Sound Effect`

### Fields

| Field | Default | Description |
|-------|---------|-------------|
| `Clip` | — | The AudioClip to play |
| `Volume` | `1.0` | SFX volume (0–1) |
| `Pitch` | `1.0` | Pitch (-3 to 3) |
| `SpatialBlend` | `0.0` | 2D (0) to 3D (1) blend |
| `Loop` | `false` | Loop the effect |

---

## 🗺️ Roadmap

- [x] AudioManager
- [x] MusicTrack
- [x] SoundEffect
- [x] Audio pooling
- [ ] AudioMixer integration
- [ ] Random pitch variation on SoundEffect

---

## 📄 License

MIT © 2026 Axel Brissy — [axel-brissy.fr](https://axel-brissy.fr)
