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
        private EndLevelSystem _endLevelSystem;
        private SceneLoader _sceneLoader;

        private const string COMMON_OBJECT_TAG = "CommonObject";
        
        public override void Run(SceneEnterParams enterParams) {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            
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
            _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);
            
            _clickButtonManager.OnClicked += () => {
                _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
            };
            
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _endLevelWindow.OnMetaClicked += GoToMeta;
            _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;

            _audioManager.PlayClip(AudioNames.BackgroundGameMusic);
            StartLevel();
        }

        private void StartLevel() {
            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            var location = _gameEnterParams.Location;
            var level = _gameEnterParams.Level;
            if(location > maxLocationAndLevel.x ||
               (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y)) {
                location = maxLocationAndLevel.x;
                level = maxLocationAndLevel.y;
            }
            var levelData = _levelsConfig.GetLevel(location, level);
            
            _enemyManager.StartLevel(levelData);
        }

        private void RestartLevel() {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }

        private void GoToMeta() {
            _sceneLoader.LoadMetaScene();
        }
    }
}