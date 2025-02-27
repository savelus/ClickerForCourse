using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonController : MonoBehaviour {
   [SerializeField] private Button _button;
   [SerializeField] private Image _image;

   public void Initialize() {
      //Инициализация палитры кнопки
      //Визуальное изменение кнопки при клике
   }
   
   public void SubscribeOnClick(UnityAction action) {
      _button.onClick.AddListener(action);
   }

   public void UnsubscribeOnClick(UnityAction action) {
      _button.onClick.RemoveListener(action);
   }
}
