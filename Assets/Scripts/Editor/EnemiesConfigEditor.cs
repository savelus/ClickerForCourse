using Game.Configs.EnemyConfigs;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(EnemiesConfig))]
    public class EnemiesConfigEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var config = (EnemiesConfig)target;

            if (GUILayout.Button("Fill id's")) {
                config.AutoFillIdsFromSprites();
            }
        }
    }
}