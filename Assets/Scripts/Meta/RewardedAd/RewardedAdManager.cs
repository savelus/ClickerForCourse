using System;
using System.Collections;
using DG.Tweening;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Meta.RewardedAd {
    public class RewardedAdManager : MonoBehaviour {
        [SerializeField] private ConfirmationWindow _confirmWindow;
        
        private UnityAction<UnityAction> _showRewardButton;
        private UnityAction _hideRewardButton;
        private SaveSystem _saveSystem;
        private Wallet _wallet;
        private Sequence _sequence;

        public void Initialize(SaveSystem saveSystem, UnityAction<UnityAction> showRewardButton, UnityAction hideRewardButton) {
            _saveSystem = saveSystem;
            _wallet = (Wallet) _saveSystem.GetData(SavableObjectType.Wallet);
            
            _hideRewardButton = hideRewardButton;
            _showRewardButton = showRewardButton;
            showRewardButton?.Invoke(OnRewardClicked);
        }

        private void OnRewardClicked() {
            _confirmWindow.ShowWindowInfo(ShowAdvertisement, "Посмотрите рекламу и получите 50 монет");
        }

        private void ShowAdvertisement() {
            YG2.RewardedAdvShow("metaButton", GetReward);
            _hideRewardButton?.Invoke();
            _sequence = DOTween
                .Sequence()
                .AppendInterval(120f)
                .OnComplete(() => _showRewardButton?.Invoke(OnRewardClicked));
        }

        private void GetReward() {
            _wallet.ChangeCoins(50);
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }

        private void OnDestroy() {
            _sequence.Kill();
        }
    }
}