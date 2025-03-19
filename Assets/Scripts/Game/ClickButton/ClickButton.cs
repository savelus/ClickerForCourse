using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButton {
   public class ClickButton : MonoBehaviour {
      [SerializeField] private Button _button;
      [SerializeField] private Image _image;
   
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
   }
}
