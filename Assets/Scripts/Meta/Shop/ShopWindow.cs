using System.Collections.Generic;
using Game.Configs.SkillsConfig;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Shop {
    public class ShopWindow : MonoBehaviour {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [SerializeField] private List<GameObject> _blocks;

        [SerializeField] private List<ShopItem> _items;

        private Dictionary<string, ShopItem> _itemsMap;
        private int _currentBlock = 0;
        private Wallet _wallet;
        private OpenedSkills _openedSkills;
        private SkillsConfig _skillsConfig;
        private SaveSystem _saveSystem;

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig) {
            _saveSystem = saveSystem;
            _skillsConfig = skillsConfig;
            _openedSkills = (OpenedSkills) saveSystem.GetData(SavableObjectType.OpenedSkills);
            _wallet = (Wallet) saveSystem.GetData(SavableObjectType.Wallet);
            InitializeItemMap();
            
            InitializeBlockSwitching();
            ShowShopItems();
        }

        public void SetActive(bool isActive) {
            gameObject.SetActive(isActive);
        }

        private void ShowShopItems() {
            foreach (var skillData in _skillsConfig.Skills) {
                var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillData.SkillId);
                var skillDataByLevel = skillData.GetSkillDataByLevel(skillWithLevel.Level);
                
                if(!_itemsMap.ContainsKey(skillData.SkillId)) continue;

                _itemsMap[skillData.SkillId].Initialize(skillId => SkillUpgrade(skillId, skillDataByLevel.Cost),
                    skillData.SkillId,
                    "",
                    skillDataByLevel.Cost,
                    _wallet.Coins >= skillDataByLevel.Cost,
                    skillData.IsMaxLevel(skillWithLevel.Level));
            }
        }

        private void InitializeItemMap() {
            _itemsMap = new();
            foreach (var shopItem in _items) {
                _itemsMap[shopItem.SkillId] = shopItem;
            }
        }

        private void SkillUpgrade(string skillId, int cost) {
            var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillId);
            skillWithLevel.Level++;
            _wallet.ChangeCoins(-cost);
            
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            ShowShopItems();
        }

        private void InitializeBlockSwitching() {
            _previousButton.onClick.AddListener(() => ShowBlock(_currentBlock - 1));
            _nextButton.onClick.AddListener(() => ShowBlock(_currentBlock + 1));
            ShowBlock(_currentBlock);
        }

        private void ShowBlock(int index) {
            for (var i = 0; i < _blocks.Count; i++) {
                _currentBlock = (index + _blocks.Count) % _blocks.Count;
                _blocks[i].SetActive(i == _currentBlock);
            }
        }
    }
}