using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.EndLevelWindow {
    public class EndLevelWindow : MonoBehaviour {
        [SerializeField] private GameObject _loseLevelWindow;
        [SerializeField] private GameObject _winLevelWindow;

        [SerializeField] private Button _loseRestartButton;
        [SerializeField] private Button _winRestartButton;

        [SerializeField] private Button _goMetaButton;

        public event UnityAction OnRestartClicked;
        public event UnityAction OnMetaClicked;

        public void Initialize() {
            _loseRestartButton.onClick.AddListener(Restart);
            _winRestartButton.onClick.AddListener(Restart);
            _goMetaButton.onClick.AddListener(() => OnMetaClicked?.Invoke());
        }

        public void ShowLoseWindow() {
            _loseLevelWindow.SetActive(true);
            _winLevelWindow.SetActive(false);
            gameObject.SetActive(true);
        }

        public void ShowWinWindow() {
            _loseLevelWindow.SetActive(false);
            _winLevelWindow.SetActive(true);
            gameObject.SetActive(true);
        }

        private void Restart() {
            OnRestartClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}