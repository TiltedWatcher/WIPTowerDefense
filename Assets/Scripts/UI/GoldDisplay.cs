using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour{


    //cached
    Bank bank;
    TextMeshProUGUI goldDisplay;

    private void Awake() {
        bank = FindObjectOfType<Bank>();
        goldDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
    }

    public void UpdateGoldDisplay() {
        goldDisplay.text = $"Gold: {bank.CurrentBalance}";
    }

}
