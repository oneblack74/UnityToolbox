using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace oneBlack74.UnityToolbox.Audio
{
    internal class AudioPool
    {
        private readonly Transform _parent;
        private readonly Queue<AudioSource> _available = new();
        private readonly List<AudioSource> _inUse = new();

        internal AudioPool(Transform parent, int initialSize)
        {
            _parent = parent;
            for (int i = 0; i < initialSize; i++)
                _available.Enqueue(CreateSource());
        }

        // Get a source from the pool
        internal AudioSource Get()
        {
            AudioSource source = _available.Count > 0
                ? _available.Dequeue()
                : CreateSource();

            _inUse.Add(source);
            return source;
        }

        // Return a source to the pool
        internal void Return(AudioSource source)
        {
            source.Stop();
            source.clip = null;
            _inUse.Remove(source);
            _available.Enqueue(source);
        }

        // Auto-return source when clip is done
        internal async UniTaskVoid AutoReturn(AudioSource source)
        {
            await UniTask.WaitWhile(() => source.isPlaying);
            Return(source);
        }

        private AudioSource CreateSource()
        {
            var go = new GameObject("AudioSource");
            go.transform.SetParent(_parent);
            return go.AddComponent<AudioSource>();
        }

        // Return all active sources to the pool
        internal void ReturnAll()
        {
            foreach (var source in _inUse.ToArray())
                Return(source);
        }

    }
}
