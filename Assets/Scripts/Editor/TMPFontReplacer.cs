using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor {
    public class TMPFontReplacer : EditorWindow
    {
        private TMP_FontAsset newFont;

        [MenuItem("Tools/Replace TMP Font")]
        public static void ShowWindow()
        {
            GetWindow<TMPFontReplacer>("Replace TMP Font");
        }

        void OnGUI()
        {
            GUILayout.Label("Выберите новый TMP Font Asset", EditorStyles.boldLabel);
            newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", newFont, typeof(TMP_FontAsset), false);

            if (GUILayout.Button("Заменить во всех сценах"))
            {
                ReplaceFontInAllScenes();
            }
        }

        void ReplaceFontInAllScenes()
        {
            string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
            foreach (string guid in scenePaths)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var scene = EditorSceneManager.OpenScene(path);
                var texts = GameObject.FindObjectsOfType<TextMeshProUGUI>(true);

                foreach (var tmp in texts)
                {
                    Undo.RecordObject(tmp, "Change TMP Font");
                    tmp.font = newFont;
                    EditorUtility.SetDirty(tmp);
                }

                EditorSceneManager.SaveScene(scene);
            }

            Debug.Log("✅ Замена шрифта завершена во всех сценах.");
        }
    }
}
