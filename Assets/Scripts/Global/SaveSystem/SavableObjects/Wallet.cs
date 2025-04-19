using System;
using UnityEngine.Events;

namespace Global.SaveSystem.SavableObjects {
    public class Wallet : ISavable {
        public int Coins = 50;
        public event UnityAction<int> OnChanged;

        public void ChangeCoins(int coins) {
            Coins += coins;
            OnChanged?.Invoke(Coins);
        }
    }
}