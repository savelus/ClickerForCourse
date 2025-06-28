using TMPro;
using UnityEngine;

namespace Game {
    public class GameHeader : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _coinsCountText;
        [SerializeField] private TextMeshProUGUI _locationNameText;

        public void ChangeCoinsCount(int coinsCount) {
            _coinsCountText.text = coinsCount.ToString();
        }
        
        public void SetLocationNameText(string locationName) {
            _locationNameText.text = locationName;
        }
    }
}