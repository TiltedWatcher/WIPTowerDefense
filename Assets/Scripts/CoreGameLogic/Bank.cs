using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank: MonoBehaviour {

    //parameters
    [SerializeField] int startingBalance = 100;
    [SerializeField] int taxEarnings = 10;
    [SerializeField] [Min(0f)] float secondsBetweenTaxPayments = 10f;
    [SerializeField] bool subjectsAreBeingTaxed = true;
    
    
    //states
    [SerializeField] int currentBalance;

    public int CurrentBalance {
        get => currentBalance;
    }

    void Awake() {
        currentBalance = startingBalance;
    }

    private void Start() {
        StartCoroutine(Taxes());
    }

    public void DepositGold(int amount) {

        currentBalance += Mathf.Abs(amount);
    }

    public void WithdrawGold(int amount) {

        currentBalance -= Mathf.Abs(amount);
    }

    IEnumerator Taxes() {
        yield return new WaitForSeconds(secondsBetweenTaxPayments);
        while (subjectsAreBeingTaxed) {
            currentBalance += taxEarnings;
            yield return new WaitForSeconds(secondsBetweenTaxPayments);
        }
    }
}
