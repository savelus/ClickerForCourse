using Game.ClickButton;
using Game.Configs.KNBConfig;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.Enemies;
using Game.Skills;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class GameEntryPoint : EntryPoint {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private LevelInfoBlock _levelInfoBlock;
        [SerializeField] private GameHeader _gameHeader;
        [SerializeField] private Image _background;


        [Space] [Header("Configs")]
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private KNBConfig _knbConfig;

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
            
            _enemyManager.Initialize(_healthBar, _timer, _levelInfoBlock);
            _endLevelWindow.Initialize();
            
            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new(openedSkills, _skillsConfig, _enemyManager, _knbConfig, _clickButtonManager);
            _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);
            _clickButtonManager.Initialize(_skillSystem);
            
            _gameHeader.SetLocationNameText(_levelsConfig.GetLocationName(_gameEnterParams.Location));
            
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _endLevelWindow.OnMetaClicked += GoToMeta;
            _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;

            _audioManager.PlayClip(AudioNames.BackgroundGameMusic);

            _background.sprite = _levelsConfig.GetLocationBg(_gameEnterParams.Location);

            InitCoins();
            
            StartLevel();
        }

        private void InitCoins() {
            var wallet = (Wallet) _saveSystem.GetData(SavableObjectType.Wallet);
            _gameHeader.ChangeCoinsCount(wallet.Coins);
            wallet.OnChanged += _gameHeader.ChangeCoinsCount;
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
            
            _enemyManager.StartLevel(levelData, maxLocationAndLevel.y);
        }

        private void RestartLevel() {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }

        private void GoToMeta() {
            _sceneLoader.LoadMetaScene();
        }

        private void OnDestroy() {
            var wallet = (Wallet) _saveSystem.GetData(SavableObjectType.Wallet);
            wallet.OnChanged -= _gameHeader.ChangeCoinsCount;
        }
    }
}