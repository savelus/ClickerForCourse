using Game.Configs.EnemyConfigs;
using Game.Configs.KNBConfig;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies {
    public class EnemyManager : MonoBehaviour {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;
    
        private Enemy _currentEnemyMonoBehaviour;
        private HealthBar.HealthBar _healthBar;
        private Timer.Timer _timer;
        private LevelData _levelData;
        private int _currentEnemyIndex;
        private DamageType _currentEnemyDamageType;
        private LevelInfoBlock _levelInfoBlock;
        private int _maxLevelOnLocation;

        public event UnityAction<bool> OnLevelPassed;
    
        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer, LevelInfoBlock levelInfoBlock) {
            _levelInfoBlock = levelInfoBlock;
            _timer = timer;
            _healthBar = healthBar;
        }

        public void StartLevel(LevelData levelData, int maxLevelOnLocation) {
            _maxLevelOnLocation = maxLevelOnLocation;
            _levelData = levelData;
            
            _currentEnemyIndex = -1;
            
            if (!_currentEnemyMonoBehaviour) {
                _currentEnemyMonoBehaviour = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemyMonoBehaviour.OnDead += SpawnEnemy;
                _currentEnemyMonoBehaviour.OnDamaged += _healthBar.DecreaseValue;
            }
            
            SpawnEnemy();
        }
        
        private void SpawnEnemy() {
            _currentEnemyIndex++;
            _timer.Stop();
            
            if (_currentEnemyIndex >= _levelData.Enemies.Count) {
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }
            
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            _timer.SetActive(currentEnemy.IsBoss);
            if (currentEnemy.IsBoss) {
                _timer.SetValue(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            
            InitHpBar(currentEnemy.Hp);

            var enemyStaticData = _enemiesConfig.GetEnemy(currentEnemy.Id);
            _currentEnemyDamageType = enemyStaticData.DamageType;
            _currentEnemyMonoBehaviour.Initialize(enemyStaticData.Sprite, currentEnemy.Hp);

            if (currentEnemy.IsBoss) {
                _levelInfoBlock.ShowBossInfo();
            }
            else {
                _levelInfoBlock.ShowStandardInfo(_levelData.LevelNumber, 
                                         _maxLevelOnLocation,
                                                 _currentEnemyIndex + 1,
                                                 _levelData.Enemies.Count);
            }
        }

        private void InitHpBar(float health) {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);
        }

        public void DamageCurrentEnemy(float damage) {
            _currentEnemyMonoBehaviour.DoDamage(damage);
        }

        public DamageType GetCurrentEnemyDamageType() {
            return _currentEnemyDamageType;
        }
    }
}