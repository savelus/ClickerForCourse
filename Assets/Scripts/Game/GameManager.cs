using Game.ClickButton;
using Game.Enemies;
using SceneManagement;
using UnityEngine;

namespace Game {
    public class GameManager : EntryPoint {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;

        [SerializeField] private Timer.Timer _timer;

        private const string SCENE_LOADER_TAG = "SceneLoader";
        
        public override void Run(SceneEnterParams enterParams) {
            _clickButtonManager.Initialize();
            _enemyManager.Initialize(_healthBar);
            _endLevelWindow.Initialize();

            _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _enemyManager.OnLevelPassed += LevelPassed;

            StartLevel();
        }

        private void LevelPassed() {
            _endLevelWindow.ShowWinWindow();
            _timer.Stop();
        }

        private void StartLevel() {
            _timer.Initialize(10f);
            _enemyManager.SpawnEnemy();

            _timer.OnTimerEnd += _endLevelWindow.ShowLoseWindow;
        }

        private void RestartLevel() {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene();
        }
    }
}