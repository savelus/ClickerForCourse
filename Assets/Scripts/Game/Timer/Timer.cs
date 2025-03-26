using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Timer {
    public class Timer : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _timerText;

        private float _maxTime;
        private float _currentTime;
        private bool _isPlaying;
    
        public event UnityAction OnTimerEnd;
    
        public void SetValue(float maxTime) {
            _maxTime = maxTime;
            _currentTime = maxTime;
            Play();
        }
    
        public void Play() {
            _isPlaying = true;
        }

        public void Stop() {
            _isPlaying = false;
            OnTimerEnd = null;
        }
    
        public void Pause() {
            _isPlaying = false;
        }

        public void Resume() {
            _isPlaying = true;
        }

        public void SetActive(bool isActive) {
            gameObject.SetActive(isActive);
        }

        private void FixedUpdate() {
            if(!_isPlaying) return;

            var deltaTime = Time.fixedDeltaTime;
            _currentTime -= deltaTime;
        
            if (_currentTime <= 0) {
                OnTimerEnd?.Invoke();
                Stop();
                return;
            }

            _currentTime -= deltaTime;
            _timerText.text = _currentTime.ToString("00:00");
        }
    }
}