using TMPro;
using UnityEngine;

namespace Game {
    public class LevelInfoBlock : MonoBehaviour {
        [SerializeField] private GameObject _bossBlock;
        [SerializeField] private GameObject _standardBlock;
        
        [SerializeField] private TextMeshProUGUI _levelInfoText;
        [SerializeField] private TextMeshProUGUI _waveInfotext;

        public void ShowBossInfo() {
            _bossBlock.SetActive(true);
            _standardBlock.SetActive(false);
        }

        public void ShowStandardInfo(int levelNumber, int allLevels, int waveNumber, int allWaves) {
            _bossBlock.SetActive(false);
            _standardBlock.SetActive(true);
            
            _levelInfoText.text = $"Уровень {levelNumber}/{allLevels}";
            _waveInfotext.text = $"Волна {waveNumber}/{allWaves}";
        }

        public void SetLevelInfo(int levelNumber, int allLevels) {
            _levelInfoText.text = $"Уровень {levelNumber}/{allLevels}";
        }

        public void SetWaveInfo(int waveNumber, int allWaves) {
            _waveInfotext.text = $"Волна {waveNumber}/{allWaves}";
        }
    }
}
