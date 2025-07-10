using Game.Configs.KNBConfig;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants {
    [Preserve]
    public class BlueAutoClickSkill : Skill {
        private SkillDataByLevel _skillData;
        private SkillScope _scope;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData) {
            _skillData = skillData;
            _scope = scope;
        }

        public override void OnSkillRegistered() {
            _scope.ClickButtonManager.SetAutoClickToButton(DamageType.Blue, _skillData.Value);
        }
    }
}