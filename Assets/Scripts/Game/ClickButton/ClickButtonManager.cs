using System;
using Game.Configs.KNBConfig;
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

        public void SetAutoClickToButton(DamageType color, float interval) {
            switch (color) {
                case DamageType.Red:
                    _clickRedButton.InitAutoClickLoop(interval);
                    break;
                case DamageType.Green:
                    _clickGreenButton.InitAutoClickLoop(interval);
                    break;
                case DamageType.Blue:
                    _clickBlueButton.InitAutoClickLoop(interval);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        public void StopAutoClickToButton(DamageType color) {
            switch (color) {
                case DamageType.Red:
                    _clickRedButton.KillAutoClickLoop();
                    break;
                case DamageType.Green:
                    _clickGreenButton.KillAutoClickLoop();
                    break;
                case DamageType.Blue:
                    _clickBlueButton.KillAutoClickLoop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
    }
}