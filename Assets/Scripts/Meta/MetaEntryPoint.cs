using Game.Configs.SkillsConfig;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Global.Translator;
using Meta.HUD;
using Meta.Locations;
using Meta.RewardedAd;
using Meta.Shop;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Meta {
    public class MetaEntryPoint : EntryPoint {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private TabsView _tabsView;
        [SerializeField] private RewardedAdManager _rewardedAdManager;
        
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;
        private TranslatorManager _translatorManager;

        private const string COMMON_OBJECT_TAG = "CommonObject";

        public override void Run(SceneEnterParams enterParams) {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            _translatorManager = commonObject.TranslatorManager;
            
            var progress = (Progress) _saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig, _translatorManager);
            _tabsView.Initialize(OpenShop, OpenLocation);
            
            _rewardedAdManager.Initialize(_saveSystem, 
                callback => _tabsView.ShowRewardedButton(callback),
                () => _tabsView.HideRewardedButton());
            
            _audioManager.PlayClip(AudioNames.BackgroundMetaMusic);
        }

        private void StartLevel(int location, int level) {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
        
        private void OpenShop() { 
            YG2.InterstitialAdvShow();
            _shopWindow.SetActive(true);
        }

        private void OpenLocation() {
            _shopWindow.SetActive(false);
        }
    }
}
