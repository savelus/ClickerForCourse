using System.Collections.Generic;
using Game.Enemies;
using UnityEditor;
using UnityEngine;

namespace Game.Configs.EnemyConfigs {
    [CreateAssetMenu(menuName="Configs/EnemiesConfig", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject {
        public Enemy EnemyPrefab;
        public List<EnemyStaticData> Enemies;

        public EnemyStaticData GetEnemy(string id) {
            foreach (var enemyData in Enemies) {
                if (enemyData.Id == id) return enemyData;
            }
            
            Debug.LogError($"Not found enemy with id {id}");
            return default;
        }
        
#if UNITY_EDITOR
        public void AutoFillIdsFromSprites() {
            for (int i = 0; i < Enemies.Count; i++) {
                if (Enemies[i].Sprite != null) {
                    var data = Enemies[i];
                    data.Id = Enemies[i].Sprite.name;
                    Enemies[i] = data;
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif   
    }
}