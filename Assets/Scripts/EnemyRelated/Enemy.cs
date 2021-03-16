using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    //parameters
    [SerializeField] int goldReward;
    [SerializeField] int goldPenalty;


    //cached references
    Bank bank;

    void Start() {
        bank = FindObjectOfType<Bank>();
    }

    public void DropGoldFromDeath() {

        if (!bank) {return;}
        bank.DepositGold(goldReward);
    }

    public void StealGold() {

        if (!bank) {return;}
        bank.WithdrawGold(goldReward);
    }

}
