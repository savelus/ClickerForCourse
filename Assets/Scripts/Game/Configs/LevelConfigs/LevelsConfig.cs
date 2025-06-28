using System.Collections.Generic;
using System.Linq;
using Extensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Configs.LevelConfigs {
    [CreateAssetMenu(menuName="Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject {
        [SerializeField] private List<LevelData> _levels;
        [SerializeField] private List<LocationData> _locations;

        private Dictionary<int, Dictionary<int, LevelData>> _levelsMap;
        private Dictionary<int, LocationData> _locationsMap;
        
        public LevelData GetLevel(int location, int level) {
            if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
            return _levelsMap[location][level];
        }

        public int GetMaxLevelOnLocation(int location) {
            if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
            var maxLevel = 0;
            foreach (var levelNumber in _levelsMap[location].Keys) {
                if (levelNumber <= maxLevel) continue;
                maxLevel = levelNumber;
            }

            return maxLevel;
        }

        public Vector2Int GetMaxLocationAndLevel() {
            if(_levelsMap.IsNullOrEmpty()) FillLevelMap();
            var locationAndLevel = new Vector2Int();
            foreach (var locationNumber in _levelsMap.Keys) {
                if (locationNumber <= locationAndLevel.x) continue;
                locationAndLevel.x = locationNumber;
            }
            
            foreach (var levelNumber in _levelsMap[locationAndLevel.x].Keys) {
                if (levelNumber <= locationAndLevel.y) continue;
                locationAndLevel.y = levelNumber;
            }

            return locationAndLevel;
        }

        public Sprite GetLocationBg(int location) {
            if (_locationsMap.IsNullOrEmpty()) FillLocationMap();

            return _locationsMap[location].Background;
        }

        private void FillLocationMap() {
            _locationsMap = _locations.ToDictionary(x => x.Number);
        }

        private void FillLevelMap() {
            _levelsMap = new();

            foreach (var levelData in _levels) {
                var locationMap = _levelsMap.GetOrCreate(levelData.Location);
                locationMap[levelData.LevelNumber] = levelData; 
            }
        }

        public string GetLocationName(int currentLocation) {
            if (_locationsMap.IsNullOrEmpty()) FillLocationMap();
            
            return _locationsMap[currentLocation].LocationName;
        }
    }
}
    
