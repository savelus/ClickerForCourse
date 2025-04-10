using System;

namespace Game.Configs.KNBConfig {
    [Serializable]
    public struct KNBData {
        public DamageType From;
        public DamageType To;
        public float Multiplier;
    }
}