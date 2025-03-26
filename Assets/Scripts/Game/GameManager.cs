using Game.ClickButton;
using Game.Configs.LevelConfigs;
using Game.Enemies;
using SceneManagement;
using UnityEngine;

namespace Game {
    public class GameManager : EntryPoint {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private LevelsConfig _levelsConfig;

        [SerializeField] private Timer.Timer _timer;
        
        private GameEnterParams _gameEnterParams;

        private const string SCENE_LOADER_TAG = "SceneLoader";
        
        public override void Run(SceneEnterParams enterParams) {
            if (enterParams is not GameEnterParams gameEnterParams) {
                Debug.LogError("troubles with enter params into game");
                return;
            }

            _gameEnterParams = gameEnterParams;
            
            _clickButtonManager.Initialize();
            _enemyManager.Initialize(_healthBar, _timer);
            _endLevelWindow.Initialize();

            _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _enemyManager.OnLevelPassed += LevelPassed;

            StartLevel();
        }

        private void LevelPassed(bool isPassed) {
            if (isPassed) _endLevelWindow.ShowWinWindow();
            else _endLevelWindow.ShowLoseWindow();
        }

        private void StartLevel() {
            var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
            
            _enemyManager.StartLevel(levelData);
        }

        private void RestartLevel() {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene(_gameEnterParams);
        }
    }
}