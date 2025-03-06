using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    [SerializeField] private Timer _timer;

    private void Awake() {
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();

        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
        _endLevelWindow.OnRestartClicked += StartLevel;
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
}