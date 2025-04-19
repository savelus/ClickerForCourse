using System;
using Game.Configs.KNBConfig;
using UnityEngine;

namespace Game.Configs.EnemyConfigs {
    [Serializable]
    public struct EnemyStaticData {
        public string Id;
        public Sprite Sprite;
        public DamageType DamageType;
    }
}