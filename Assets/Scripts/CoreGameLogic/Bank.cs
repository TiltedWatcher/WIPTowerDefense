using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank: MonoBehaviour {

    //parameters
    [SerializeField] int startingBalance = 100;
    [SerializeField] int taxEarnings = 10;
    [SerializeField] int lossThreshhold = 0;
    [SerializeField] [Min(0f)] float secondsBetweenTaxPayments = 10f;
    [SerializeField] bool subjectsAreBeingTaxed = true;

    //cached
    GoldDisplay goldDisplay;
    
    //states
    [SerializeField] int currentBalance;

    public int CurrentBalance {
        get => currentBalance;
    }

    void Awake() {
        currentBalance = startingBalance;
        goldDisplay = FindObjectOfType<GoldDisplay>();
        goldDisplay.UpdateGoldDisplay();
    }

    private void Start() {
        StartCoroutine(Taxes());
    }

    public void DepositGold(int amount) {

        currentBalance += Mathf.Abs(amount);
        goldDisplay.UpdateGoldDisplay();
    }

    public void WithdrawGold(int amount) {

        currentBalance -= Mathf.Abs(amount);
        goldDisplay.UpdateGoldDisplay();

        if (currentBalance < lossThreshhold) {
            ProcessLoss();
        }

    }

    IEnumerator Taxes() {
        yield return new WaitForSeconds(secondsBetweenTaxPayments);
        while (subjectsAreBeingTaxed) {
            currentBalance += taxEarnings;
            goldDisplay.UpdateGoldDisplay();
            yield return new WaitForSeconds(secondsBetweenTaxPayments);
        }
    }

    void ProcessLoss() {
        var sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.reloadScene(1f);
    }
}
