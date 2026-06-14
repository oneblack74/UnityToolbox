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

## 🗺️ Roadmap

- [x] ServiceLocator
- [x] EventBus
- [x] SceneLoader
- [ ] Singleton<T>
- [ ] TaskHelper
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