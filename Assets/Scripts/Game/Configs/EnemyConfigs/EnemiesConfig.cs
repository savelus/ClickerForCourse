﻿using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Configs.EnemyConfigs {
    [CreateAssetMenu(menuName="Configs/EnemiesConfig", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject {
        public Enemy EnemyPrefab;
        public List<EnemyViewData> Enemies;

        public EnemyViewData GetEnemy(string id) {
            foreach (var enemyData in Enemies) {
                if (enemyData.Id == id) return enemyData;
            }
            
            Debug.LogError($"Not found enemy with id {id}");
            return default;
        }
    }
}