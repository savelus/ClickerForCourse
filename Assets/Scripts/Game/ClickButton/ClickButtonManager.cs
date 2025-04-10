using Game.Skills;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ClickButton {
    public class ClickButtonManager : MonoBehaviour {
        [SerializeField] private ClickButton _clickRedButton;
        [SerializeField] private ClickButton _clickGreenButton;
        [SerializeField] private ClickButton _clickBlueButton;

        [SerializeField] private ClickButtonConfig _buttonRedConfig;
        [SerializeField] private ClickButtonConfig _buttonGreenConfig;
        [SerializeField] private ClickButtonConfig _buttonBlueConfig;

        public void Initialize(SkillSystem skillSystem) {
            _clickRedButton.Initialize(_buttonRedConfig.DefaultSprite, _buttonRedConfig.ButtonColors);
            _clickGreenButton.Initialize(_buttonGreenConfig.DefaultSprite, _buttonGreenConfig.ButtonColors);
            _clickBlueButton.Initialize(_buttonBlueConfig.DefaultSprite, _buttonBlueConfig.ButtonColors);

            _clickRedButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnRed));
            _clickGreenButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnGreen));
            _clickBlueButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnBlue));
        }
    }
}