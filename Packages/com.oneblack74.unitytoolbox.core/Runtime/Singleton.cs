using UnityEngine;

namespace oneBlack74.UnityToolbox.Core
{
    /// <summary>
    /// Generic MonoBehaviour singleton base class.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new();
        private static bool _applicationIsQuitting;

        [SerializeField] private bool _persistAcrossScenes = false;

        /// <summary>
        /// Returns the singleton instance. Creates one if it doesn't exist.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance of {typeof(T)} requested after application quit.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance != null) return _instance;

                    _instance = FindAnyObjectByType<T>();

                    if (_instance != null) return _instance;

                    var go = new GameObject($"[Singleton] {typeof(T).Name}");
                    _instance = go.AddComponent<T>();

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T).Name} destroyed.");
                Destroy(gameObject);
                return;
            }

            _instance = this as T;

            if (_persistAcrossScenes)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
