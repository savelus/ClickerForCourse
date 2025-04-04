using Game.Enemies;
using Game.Skills.Data;

namespace Game.Skills.SkillVariants {
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