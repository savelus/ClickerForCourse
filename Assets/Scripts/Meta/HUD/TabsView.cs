using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.HUD {
    public class TabsView : MonoBehaviour {
        [SerializeField] private Button _shopWindowButton;
        [SerializeField] private Button _locationButton;
        [SerializeField] private Button _rewardedButton;
        [SerializeField] private TextMeshProUGUI _locationNameText;
        [SerializeField] private TextMeshProUGUI _coinsCountText;

        public void Initialize(UnityAction onShop, UnityAction onLocation) {
            SubscribeOnShopButton(onShop);
            SubscribeOnLocationButton(onLocation);
        }

        public void ShowRewardedButton(UnityAction callback) {
            _rewardedButton.onClick.RemoveAllListeners();
            _rewardedButton.onClick.AddListener(() => callback?.Invoke());
        }

        public void ChangeLocationName(string locationName) {
            _locationNameText.text = locationName;
        }

        public void HideRewardedButton() {
            _rewardedButton.onClick.RemoveAllListeners();
        }

        public void ChangeCoinsCount(int count) {
            _coinsCountText.text = count.ToString();
        }

        private void SubscribeOnShopButton(UnityAction callback) {
            _shopWindowButton.onClick.RemoveAllListeners();
            _shopWindowButton.onClick.AddListener(() => {
                callback?.Invoke();
                _shopWindowButton.image.color = new Color(0, 0, 0, 190f / 255);
                _locationButton.image.color = new Color(0, 0, 0, 130f / 255);
            });
        }

        private void SubscribeOnLocationButton(UnityAction callback) {
            _locationButton.onClick.RemoveAllListeners();
            _locationButton.onClick.AddListener(() => {
                callback?.Invoke();
                _shopWindowButton.image.color = new Color(0, 0, 0, 130f / 255);
                _locationButton.image.color = new Color(0, 0, 0, 190f / 255);
            });
        }
    }
}