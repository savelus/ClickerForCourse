using System;
using UnityEngine;

namespace Global.Translator {
    public class TranslatorManager {
        private readonly LanguageMapConfig _currentConfig;

        public TranslatorManager(string lang) { 
            _currentConfig = Resources.Load<LanguageMapConfig>("Languages/" + lang);
        }

        public string Translate(string key) {
            try {
                return typeof(LanguageMapConfig)
                    .GetField(key)
                    .GetValue(_currentConfig)
                    .ToString();
            }
            catch (Exception e) {
                return "Exception";
            }
            
        }
    }
}