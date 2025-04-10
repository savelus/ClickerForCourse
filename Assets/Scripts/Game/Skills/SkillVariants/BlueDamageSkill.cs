using Game.Configs.KNBConfig;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants {
    [Preserve]
    public class BlueDamageSkill : Skill {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skillData;
        private KNBConfig _knbConfig;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData) {
            _skillData = skillData;
            _enemyManager = scope.EnemyManager;
            _knbConfig = scope.KnbConfig;
        }

        public override void SkillProcess() {
            var toDamageType = _enemyManager.GetCurrentEnemyDamageType();
            var calculatedDamage = _knbConfig.CalculateDamage(DamageType.Blue, toDamageType, _skillData.Value);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
}