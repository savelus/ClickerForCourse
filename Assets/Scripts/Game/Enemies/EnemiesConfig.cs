using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies {
    [CreateAssetMenu(menuName="Configs/EnemiesConfig", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject {
        public Enemy EnemyPrefab;
        public List<EnemyData> Enemies;
    }
}