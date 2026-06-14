# ⚙️ Core — UnityToolbox

![Unity](https://img.shields.io/badge/Unity-6000.0%2B-black?logo=unity)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-WIP-orange)

Core utilities for Unity 6 — ServiceLocator, EventBus, and SceneLoader.

---

## 📦 Installation

Add via Git URL in Package Manager:

```
https://github.com/oneblack74/UnityToolbox.git?path=Packages/com.oneblack74.unitytoolbox.core
```

**Requirements:**
- Unity `6000.0+`
- [UniTask](https://github.com/Cysharp/UniTask)

---

## 📖 Modules

| Module | Description |
|--------|-------------|
| [**ServiceLocator**](#servicelocator) | Simple dependency injection container |
| [**EventBus**](#eventbus) | Static global event system for decoupled communication |
| [**SceneLoader**](#sceneloader) | Async scene loading with events |
| [**Singleton\<T\>**](#singletont) | Generic MonoBehaviour singleton base class |

---

## ServiceLocator

### Overview

Static service registry — register services globally and retrieve them anywhere.

### Usage

```csharp
// Register a service
ServiceLocator.Register(new MyService());

// Retrieve a service
var service = ServiceLocator.Get<MyService>();

// Try retrieve (safe)
if (ServiceLocator.TryGet<MyService>(out var myService))
{
    Debug.Log($"Found: {myService}");
}

// Unregister
ServiceLocator.Unregister<MyService>();

// Clear all
ServiceLocator.Clear();
```

---

## EventBus

### Overview

Static event bus — subscribe and publish events without direct references between components.

### Usage

```csharp
// Subscribe to an event
EventBus.Subscribe<MyEvent>(OnMyEvent);

void OnMyEvent(MyEvent evt)
{
    Debug.Log($"Received: {evt.Data}");
}

// Unsubscribe
EventBus.Unsubscribe<MyEvent>(OnMyEvent);

// Publish an event
EventBus.Publish(new MyEvent { Data = "Hello" });

// Clear all subscriptions
EventBus.Clear();
```

---

## SceneLoader

### Overview

Singleton helper for async scene loading with progress control and built-in events.

### Usage

```csharp
// Get the SceneLoader (creates if needed)
var loader = SceneLoader.Instance;

// Load scene asynchronously
await loader.LoadSceneAsync("GameScene");

// SceneLoader automatically publishes:
// - SceneLoadStartedEvent on start
// - SceneLoadCompletedEvent on completion

// Subscribe to scene events
EventBus.Subscribe<SceneLoadStartedEvent>(evt =>
{
    Debug.Log($"Loading: {evt.SceneName}");
});
```

**Events:**

```csharp
public struct SceneLoadStartedEvent
{
    public string SceneName;
}

public struct SceneLoadCompletedEvent
{
    public string SceneName;
}
```

---

## Singleton\<T\>

### Overview

Generic MonoBehaviour singleton base class. Ensures a single instance exists across the application, with optional persistence between scenes.

### Usage

```csharp
// Define a singleton
public class AudioManager : Singleton<AudioManager>
{
    public void PlaySFX(AudioClip clip) { ... }
}

// Scene-scoped singleton (disable persist in Inspector)
public class UIManager : Singleton<UIManager>
{
    public void ShowPanel(string panelName) { ... }
}

// Access anywhere
AudioManager.Instance.PlaySFX(clip);
```

**Options:**

| Field | Default | Description |
|-------|---------|-------------|
| `_persistAcrossScenes` | `false` | Calls `DontDestroyOnLoad` on the instance |

**Behaviour:**
- If no instance exists in the scene, one is created automatically
- Duplicate instances are destroyed immediately
- Returns `null` safely after application quit

---

## 🛠️ TaskHelper

Utility helpers for async task operations built on top of UniTask.

### Methods

| Method | Description |
|--------|-------------|
| `WaitUntil(condition)` | Waits until the condition returns `true` |
| `WaitWhile(condition)` | Waits while the condition returns `true` |
| `RunWithTimeout(task, seconds)` | Cancels the task if it exceeds the timeout (default: 10s) |
| `Retry(task, maxAttempts, delay)` | Retries the task on failure (default: 3 attempts) |
| `SafeFireAndForget(task, onError)` | Runs a task without await with error handling |

### Usage

```csharp
// Wait for a condition
await TaskHelper.WaitUntil(() => _isReady);
await TaskHelper.WaitWhile(() => _isLoading);

// Timeout
await TaskHelper.RunWithTimeout(() => LoadDataAsync(), seconds: 5f);

// Retry with delay between attempts
await TaskHelper.Retry(() => FetchFromAPIAsync(), maxAttempts: 3, delayBetweenAttempts: 1f);

// Fire and forget with error logging
TaskHelper.SafeFireAndForget(PlayAnimationAsync());

// Fire and forget with custom error handler
TaskHelper.SafeFireAndForget(PlayAnimationAsync(), ex => 
    Debug.LogError($"Anim failed: {ex.Message}"));
    
```
- `WaitUntil` and `WaitWhile` are thin wrappers around UniTask for a unified API.
- `RunWithTimeout` throws a `TimeoutException` if the task exceeds the limit.
- `Retry` rethrows the last exception after all attempts are exhausted.

---

## 🗺️ Roadmap

- [x] ServiceLocator
- [x] EventBus
- [x] SceneLoader
- [x] Singleton<T>
- [x] TaskHelper
- [ ] TweenHelper
- [ ] Extensions
  - [ ] TransformExtensions
  - [ ] VectorExtensions
  - [ ] ColorExtensions
  - [ ] ListExtensions
  - [ ] StringExtensions

---

## 📄 License

MIT © 2026 Axel Brissy — [axel-brissy.fr](https://axel-brissy.fr)