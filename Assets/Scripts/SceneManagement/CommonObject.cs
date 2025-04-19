using Global.Audio;
using Global.SaveSystem;
using Global.Translator;
using UnityEngine;

namespace SceneManagement {
    public class CommonObject : MonoBehaviour {
        public SceneLoader SceneLoader;
        public AudioManager AudioManager;
        public SaveSystem SaveSystem;
        public TranslatorManager TranslatorManager;
    }
}