using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.HUD {
    public class TabsView : MonoBehaviour {
        [SerializeField] private Button _shopWindowButton;
        [SerializeField] private Button _locationButton;
        [SerializeField] private Button _rewardedButton;

        public void Initialize(UnityAction onShop, UnityAction onLocation) {
            SubscribeOnShopButton(onShop);
            SubscribeOnLocationButton(onLocation);
        }

        public void ShowRewardedButton(UnityAction callback) {
            _rewardedButton.onClick.RemoveAllListeners();
            _rewardedButton.onClick.AddListener(() => callback?.Invoke());
        }
        
        public void HideRewardedButton() {
            _rewardedButton.onClick.RemoveAllListeners();
        }
        
        private void SubscribeOnShopButton(UnityAction callback) {
            _shopWindowButton.onClick.RemoveAllListeners();
            _shopWindowButton.onClick.AddListener(()=> {
                callback?.Invoke();
                _shopWindowButton.image.color = new Color(0, 0, 0, 210f / 255);
                _locationButton.image.color = new Color(0, 0, 0, 170f / 255);
            });
        }
        
        private void SubscribeOnLocationButton(UnityAction callback) {
            _locationButton.onClick.RemoveAllListeners();
            _locationButton.onClick.AddListener(()=> {
                callback?.Invoke();
                _shopWindowButton.image.color = new Color(0, 0, 0, 170f / 255);
                _locationButton.image.color = new Color(0, 0, 0, 210f / 255);
            });
        }
    }
}