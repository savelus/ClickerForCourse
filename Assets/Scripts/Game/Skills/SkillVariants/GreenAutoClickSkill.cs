using Game.Configs.KNBConfig;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants {
    [Preserve]
    public class GreenAutoClickSkill : Skill {
        private SkillDataByLevel _skillData;
        private SkillScope _scope;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData) {
            _scope = scope;
            _skillData = skillData;
        }

        public override void OnSkillRegistered() {
            _scope.ClickButtonManager.SetAutoClickToButton(DamageType.Green, _skillData.Value);
        }
    }
}