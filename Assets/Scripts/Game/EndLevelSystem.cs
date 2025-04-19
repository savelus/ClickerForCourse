using Game.Configs.LevelConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;

namespace Game {
    public class EndLevelSystem {
        private readonly EndLevelWindow.EndLevelWindow _endLevelWindow;
        private readonly SaveSystem _saveSystem;
        private readonly GameEnterParams _gameEnterParams;
        private readonly LevelsConfig _levelsConfig;

        public EndLevelSystem(EndLevelWindow.EndLevelWindow endLevelWindow,
                              SaveSystem saveSystem,
                              GameEnterParams gameEnterParams,
                              LevelsConfig levelsConfig) {
            _levelsConfig = levelsConfig;
            _gameEnterParams = gameEnterParams;
            _saveSystem = saveSystem;
            _endLevelWindow = endLevelWindow;
        }

        public void LevelPassed(bool isPassed) {
            if (isPassed) {
                TrySaveProgress();
                _endLevelWindow.ShowWinWindow();
            }
            else {
                _endLevelWindow.ShowLoseWindow();
            }
        }
        
        private void TrySaveProgress() {
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            wallet.ChangeCoins(_levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level).Reward);
            _saveSystem.SaveData(SavableObjectType.Wallet);
            
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation ||
                _gameEnterParams.Level != progress.CurrentLevel) return;

            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            if(progress.CurrentLocation > maxLocationAndLevel.x || 
               (progress.CurrentLocation == maxLocationAndLevel.x 
                    && progress.CurrentLevel > maxLocationAndLevel.y)) return;
            
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
    }
}