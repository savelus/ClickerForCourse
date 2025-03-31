using System.Collections;
using Global.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] private GameObject _loadingScreen;
        
        private AudioManager _audioManager;

        public void Initialize(AudioManager audioManager) {
            _audioManager = audioManager;
        }
        
        public void LoadMetaScene(SceneEnterParams enterParams = null) {
            StartCoroutine(LoadAndStartMeta(enterParams));
        }
        
        public void LoadGameplayScene(SceneEnterParams enterParams = null) {
            StartCoroutine(LoadAndStartGameplay(enterParams));
        }
        
        private IEnumerator LoadAndStartMeta(SceneEnterParams enterParams = null) {
            _loadingScreen.SetActive(true);

            yield return LoadScene(Scenes.Loader);
            yield return LoadScene(Scenes.MetaScene);

            var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
            sceneEntryPoint.Run(enterParams);

            _loadingScreen.SetActive(false);
        }
        
        private IEnumerator LoadAndStartGameplay(SceneEnterParams enterParams) {
            _loadingScreen.SetActive(true);

            yield return LoadScene(Scenes.Loader);
            yield return LoadScene(Scenes.LevelScene);

            var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
            sceneEntryPoint.Run(enterParams);

            _loadingScreen.SetActive(false);
        }
        
        
        private IEnumerator LoadScene(string sceneName) {
            _audioManager.Load(sceneName);
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}