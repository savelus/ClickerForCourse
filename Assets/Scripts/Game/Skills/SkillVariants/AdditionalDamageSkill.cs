using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants {
    [Preserve]
    public class AdditionalDamageSkill : Skill {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skillData;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData) {
            _skillData = skillData;
            _enemyManager = scope.EnemyManager;
        }

        public override void SkillProcess() {
            _enemyManager.DamageCurrentEnemy(_skillData.Value);
        }
    }
}