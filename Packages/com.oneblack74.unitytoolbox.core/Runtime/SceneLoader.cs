using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace oneblack74.UnityToolbox.Core
{
    public struct SceneLoadStartedEvent
    {
        public string SceneName;
    }

    public struct SceneLoadCompletedEvent
    {
        public string SceneName;
    }

    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static async UniTask LoadSceneAsync(string sceneName)
        {
            EventBus.Publish(new SceneLoadStartedEvent { SceneName = sceneName });

            var operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            // Wait until almost done
            while (operation.progress < 0.9f)
            {
                await UniTask.Yield();
            }

            operation.allowSceneActivation = true;

            await UniTask.WaitUntil(() => operation.isDone);

            EventBus.Publish(new SceneLoadCompletedEvent { SceneName = sceneName });
        }
    }
}
