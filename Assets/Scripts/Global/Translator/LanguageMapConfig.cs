using UnityEngine;

namespace Global.Translator {
    [CreateAssetMenu(menuName = "Configs/LanguageMap", fileName = "LanguageMap")]
    public class LanguageMapConfig : ScriptableObject {
        public string BlueDamageSkillLabel;
        public string BlueDamageSkillDescription;
    }
}