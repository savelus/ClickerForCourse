using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButton {
   public class ClickButton : MonoBehaviour {
      [SerializeField] private Button _button;
      [SerializeField] private Image _image;
      [SerializeField] private Image _proressBar;
      private Sequence _loopTween;

      public void Initialize(Sprite sprite, ColorBlock colorBlock) {
         _image.sprite = sprite;
         _button.colors = colorBlock;
      }

      public void SubscribeOnClick(UnityAction action) {
         _button.onClick.AddListener(action);
      }

      public void UnsubscribeOnClick(UnityAction action) {
         _button.onClick.RemoveListener(action);
      }

      public void InitAutoClickLoop(float intervalSeconds) {
         _proressBar.fillAmount = 1;
         _loopTween?.Kill();

         _loopTween = DOTween.Sequence()
            .AppendCallback(() => _proressBar.fillAmount = 1f)
            .Append(_proressBar.DOFillAmount(0f, intervalSeconds).SetEase(Ease.Linear))
            .AppendCallback(() => _button.onClick?.Invoke())
            .SetLoops(-1);
      }

      public void KillAutoClickLoop() {
         _loopTween?.Kill();
         _proressBar.fillAmount = 1;
      }

      private void OnDestroy() {
         KillAutoClickLoop();
      }
   }
}
