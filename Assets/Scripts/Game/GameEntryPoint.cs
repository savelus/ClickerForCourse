using Game.ClickButton;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.Enemies;
using Game.Skills;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEditor;
using UnityEngine;
using Progress = Global.SaveSystem.SavableObjects.Progress;

namespace Game {
    public class GameEntryPoint : EntryPoint {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private SkillsConfig _skillsConfig;

        [SerializeField] private Timer.Timer _timer;
        
        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SkillSystem _skillSystem;

        private const string SCENE_LOADER_TAG = "SceneLoader";
        
        public override void Run(SceneEnterParams enterParams) {
            _saveSystem = FindFirstObjectByType<SaveSystem>();
            _audioManager = FindFirstObjectByType<AudioManager>();
            
            if (enterParams is not GameEnterParams gameEnterParams) {
                Debug.LogError("troubles with enter params into game");
                return;
            }

            _gameEnterParams = gameEnterParams;
            
            _clickButtonManager.Initialize();
            _enemyManager.Initialize(_healthBar, _timer);
            _endLevelWindow.Initialize();
            
            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new(openedSkills, _skillsConfig, _enemyManager);

            _clickButtonManager.OnClicked += () => {
                _enemyManager.DamageCurrentEnemy(1f);
                _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
            };
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _enemyManager.OnLevelPassed += LevelPassed;

            _audioManager.PlayClip(AudioNames.BackgroundGameMusic);
            StartLevel();
        }

        private void LevelPassed(bool isPassed) {
            if (isPassed) {
                TrySaveProgress();
                _endLevelWindow.ShowWinWindow();
            }
            else {
                _endLevelWindow.ShowLoseWindow();
            }
        }

        private void TrySaveProgress() {
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation ||
                _gameEnterParams.Level != progress.CurrentLevel) return;
            
            var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);
            if (progress.CurrentLevel >= maxLevel) {
                progress.CurrentLevel = 1;
                progress.CurrentLocation++;
            }
            else {
                progress.CurrentLevel++;
            }
            
            _saveSystem.SaveData(SavableObjectType.Progress);
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