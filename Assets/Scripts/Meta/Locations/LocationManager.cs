using System;
using System.Collections.Generic;
using Game.Configs.LevelConfigs;
using Global.SaveSystem.SavableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations {
    public class LocationManager : MonoBehaviour {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _locationNameText;

        [SerializeField] private List<Location> _locations;
        [SerializeField] private LevelsConfig _levelsConfig;

        private int _currentLocation;

        public void Initialize(Progress progress, UnityAction<int, int> startLevelCallback) {
            _currentLocation = progress.CurrentLocation;

            ChangeLocationName();
            
            InitLocations(progress, startLevelCallback);
            InitializeMoveLocationButtons();
        }

        private void InitializeMoveLocationButtons() {
            _previousButton.onClick.AddListener(ShowPreviousLocation);
            _nextButton.onClick.AddListener(ShowNextLocation);

            if (_currentLocation == _locations.Count) {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 1) {
                _previousButton.gameObject.SetActive(false);
            }
        }

        private void ChangeLocationName() {
            _locationNameText.text = _levelsConfig.GetLocationName(_currentLocation);
        }

        private void ShowNextLocation() {
            _locations[_currentLocation - 1].SetActive(false);
            _currentLocation++;
            _locations[_currentLocation - 1].SetActive(true);

            if (_currentLocation == _locations.Count) {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 2) {
                _previousButton.gameObject.SetActive(true);
            }

            ChangeLocationName();
        }

        private void ShowPreviousLocation() {
            _locations[_currentLocation - 1].SetActive(false);
            _currentLocation--;
            _locations[_currentLocation - 1].SetActive(true);

            if (_currentLocation == _locations.Count - 1) {
                _nextButton.gameObject.SetActive(true);
            }

            if (_currentLocation == 1) {
                _previousButton.gameObject.SetActive(false);
            }
            
            ChangeLocationName();
        }

        private void InitLocations(Progress progress, UnityAction<int, int> startLevelCallback) {
            for (var i = 0; i < _locations.Count; i++) {
                var locationNumber = i + 1;

                var isLocationPassed = progress.CurrentLocation > locationNumber
                    ? ProgressState.Passed
                    : progress.CurrentLocation == locationNumber
                        ? ProgressState.Current
                        : ProgressState.Closed;

                var currentLevel = progress.CurrentLevel;

                var startAbsoluteLevel = _levelsConfig.GetAbsoluteStartLevelOnLocation(locationNumber);
                _locations[i].Initialize(isLocationPassed, currentLevel, startAbsoluteLevel,
                    level => startLevelCallback?.Invoke(locationNumber, level));
                _locations[i].SetActive(progress.CurrentLocation == locationNumber);
            }
        }
    }
}